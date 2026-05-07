using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Application.DTOs
{
    public class CreateTodoDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
