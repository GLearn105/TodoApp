using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoResponseDto>> GetAllAsync();
        Task<TodoResponseDto?> GetByIdAsync(Guid id);
        Task<TodoResponseDto> CreateAsync(CreateTodoDto dto);
        Task<TodoResponseDto> UpdateAsync(Guid id, UpdateTodoDto dto);
        Task DeleteAsync(Guid id);
    }
}
