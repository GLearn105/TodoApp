using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Interfaces
{
    public interface ITodoService
    {
       //Async
        Task<IEnumerable<TodoResponseDto>> GetAllAsync();
        Task<TodoResponseDto?> GetByIdAsync(Guid id);
        Task<TodoResponseDto> CreateAsync(CreateTodoDto dto);
        Task<TodoResponseDto> UpdateAsync(Guid id, UpdateTodoDto dto);
        Task DeleteAsync(Guid id);

        //Sync
        //IEnumerable<TodoItemDto> GetAll();
        //TodoItemDto? GetById(Guid id);
        //TodoItemDto Create(CreateTodoDto dto);
        //TodoItemDto? Update(Guid id, UpdateTodoDto dto);
        //bool Delete(Guid id);
    }
}
