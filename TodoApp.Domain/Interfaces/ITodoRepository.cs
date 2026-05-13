using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Interfaces
{
    public interface ITodoRepository
    {
        //Sync
        //IEnumerable<TodoItem> GetAll();
        //TodoItem? GetById(Guid id);
        //TodoItem Create(TodoItem todoItem);
        //TodoItem? Update(TodoItem todoItem);
        //void Delete(Guid id); IEnumerable<TodoItem> GetAll();
        //TodoItem? GetById(Guid id);
        //TodoItem Create(TodoItem todoItem);
        //TodoItem? Update(TodoItem todoItem);
        //void Delete(Guid id);
        
        //Async
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(Guid id);
        Task<TodoItem> CreateAsync(TodoItem todoItem);
        Task<TodoItem?> UpdateAsync(TodoItem todoItem);
        Task DeleteAsync(Guid id);
    }
}
