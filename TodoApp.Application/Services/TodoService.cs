using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IValidator<CreateTodoDto> _createValidator;
        private readonly IValidator<UpdateTodoDto> _updateValidator;

        public TodoService(
            ITodoRepository repository,
            IValidator<CreateTodoDto> createValidator,
            IValidator<UpdateTodoDto> updateValidator)
        {
            _repository = repository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<TodoResponseDto>> GetAllAsync()
        {
            var todos = await _repository.GetAllAsync();
            return todos.Select(MapToDto);
        }

        public async Task<TodoResponseDto?> GetByIdAsync(Guid id)
        {
            var todo = await _repository.GetByIdAsync(id);
            return todo == null ? null : MapToDto(todo);
        }

        public async Task<TodoResponseDto> CreateAsync(CreateTodoDto dto)
        {
            var result = await _createValidator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            var todo = new TodoItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false
            };

            var created = await _repository.CreateAsync(todo);
            return MapToDto(created);
        }

        public async Task<TodoResponseDto> UpdateAsync(Guid id, UpdateTodoDto dto)
        {
            var result = await _updateValidator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) throw new KeyNotFoundException($"Todo dengan id {id} tidak ditemukan.");

            todo.Title = dto.Title;
            todo.Description = dto.Description;
            todo.IsCompleted = dto.IsCompleted;

            var updated = await _repository.UpdateAsync(todo);
            return MapToDto(updated);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        private static TodoResponseDto MapToDto(TodoItem todo) => new()
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt
        };
    }
}
