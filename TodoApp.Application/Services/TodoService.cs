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

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoResponseDto>> GetAllAsync()
        {
            var todos = await _repository.GetAllAsync();
            return todos.Select(MapToResponse);
        }

        public async Task<TodoResponseDto?> GetByIdAsync(Guid id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) return null;
            return MapToResponse(todo);
        }

        public async Task<TodoResponseDto> CreateAsync(CreateTodoDto dto)
        {
            var todo = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(todo);
            return MapToResponse(created);
        }

        public async Task<TodoResponseDto> UpdateAsync(Guid id, UpdateTodoDto dto)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) throw new Exception($"Todo dengan id {id} tidak ditemukan");

            todo.Title = dto.Title;
            todo.Description = dto.Description;
            todo.IsCompleted = dto.IsCompleted;

            //var updated = await _repository.UpdateAsync(todo);
            //return MapToResponse(updated);
            var updated = await _repository.UpdateAsync(todo);
            if (updated is null) throw new Exception("Gagal update todo");
            return MapToResponse(updated);
        }

        public async Task DeleteAsync(Guid id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) throw new Exception($"Todo dengan id {id} tidak ditemukan");

            await _repository.DeleteAsync(id);
        }

        // Helper method untuk mapping Entity → DTO
        private static TodoResponseDto MapToResponse(TodoItem todo)
        {
            return new TodoResponseDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt
            };
        }
    }
}
