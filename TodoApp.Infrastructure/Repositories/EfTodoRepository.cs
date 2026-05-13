using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repositories
{
    public class EfTodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public EfTodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(Guid id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<TodoItem> CreateAsync(TodoItem todoItem)
        {
            todoItem.CreatedAt = DateTime.UtcNow;
            _context.Todos.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem?> UpdateAsync(TodoItem todoItem)
        {
            var existing = await _context.Todos.FindAsync(todoItem.Id);
            if (existing is null) return null;

            existing.Title = todoItem.Title;
            existing.IsCompleted = todoItem.IsCompleted;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo is not null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
//using System;
//using System.Collections.Generic;
//using System.Text;
//using TodoApp.Domain.Entities;
//using TodoApp.Domain.Interfaces;

//namespace TodoApp.Infrastructure.Repositories
//{
//    public class TodoRepository : ITodoRepository
//    {
//        private readonly List<TodoItem> _todos = new();

//        public Task<IEnumerable<TodoItem>> GetAllAsync()
//        {
//            return Task.FromResult<IEnumerable<TodoItem>>(_todos);
//        }

//        public Task<TodoItem?> GetByIdAsync(Guid id)
//        {
//            var todo = _todos.FirstOrDefault(t => t.Id == id);
//            return Task.FromResult(todo);
//        }

//        public Task<TodoItem> CreateAsync(TodoItem todoItem)
//        {
//            todoItem.Id = Guid.NewGuid();
//            todoItem.CreatedAt = DateTime.UtcNow;
//            _todos.Add(todoItem);
//            return Task.FromResult(todoItem);
//        }

//        public Task<TodoItem?> UpdateAsync(TodoItem todoItem)
//        {
//            var existing = _todos.FirstOrDefault(t => t.Id == todoItem.Id);
//            if (existing is null) return Task.FromResult<TodoItem?>(null);

//            existing.Title = todoItem.Title;
//            existing.IsCompleted = todoItem.IsCompleted;
//            return Task.FromResult<TodoItem?>(existing);
//        }

//        public Task DeleteAsync(Guid id)
//        {
//            var todo = _todos.FirstOrDefault(t => t.Id == id);
//            if (todo is not null) _todos.Remove(todo);
//            return Task.CompletedTask;
//        }
//    }
//}
