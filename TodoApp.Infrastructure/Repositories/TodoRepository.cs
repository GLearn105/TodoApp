using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> _todos = new();

        public Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TodoItem>>(_todos);
        }

        public Task<TodoItem?> GetByIdAsync(Guid id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(todo);
        }

        public Task<TodoItem> CreateAsync(TodoItem todoItem)
        {
            todoItem.Id = Guid.NewGuid();
            todoItem.CreatedAt = DateTime.UtcNow;
            _todos.Add(todoItem);
            return Task.FromResult(todoItem);
        }

        public Task<TodoItem?> UpdateAsync(TodoItem todoItem)
        {
            var existing = _todos.FirstOrDefault(t => t.Id == todoItem.Id);
            if (existing is null) return Task.FromResult<TodoItem?>(null);

            existing.Title = todoItem.Title;
            existing.IsCompleted = todoItem.IsCompleted;
            return Task.FromResult<TodoItem?>(existing);
        }

        public Task DeleteAsync(Guid id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo is not null) _todos.Remove(todo);
            return Task.CompletedTask;
        }
    }
}
