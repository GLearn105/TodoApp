using FluentValidation;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;
using TodoApp.Domain.Exceptions;

using DomainValidationException = TodoApp.Domain.Exceptions.ValidationException;

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

            if (todo is null)
                throw new NotFoundException(nameof(TodoItem), id);

            return MapToDto(todo);
        }

        public async Task<TodoResponseDto> CreateAsync(CreateTodoDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                throw new DomainValidationException(errors);
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
            var validationResult = await _updateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                throw new DomainValidationException(errors);
            }

            var todo = await _repository.GetByIdAsync(id);

            if (todo is null)
                throw new NotFoundException(nameof(TodoItem), id);

            todo.Title = dto.Title;
            todo.IsCompleted = dto.IsCompleted;
            todo.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(todo);
            return MapToDto(todo);
        }

        public async Task DeleteAsync(Guid id)
        {
            var todo = await _repository.GetByIdAsync(id);

            if (todo is null)
                throw new NotFoundException(nameof(TodoItem), id);

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