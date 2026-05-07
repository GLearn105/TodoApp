using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Application.DTOs
{
    public class UpdateTodoDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
