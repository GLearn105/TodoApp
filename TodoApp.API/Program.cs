using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Application.Validators;
using TodoApp.Domain.Interfaces;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories & Services
builder.Services.AddScoped<ITodoRepository, EfTodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

// ↓ Tambahkan ini: Daftarkan Validators
builder.Services.AddScoped<IValidator<CreateTodoDto>, CreateTodoDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateTodoDto>, UpdateTodoDtoValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();