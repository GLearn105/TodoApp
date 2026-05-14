using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Validators
{
    public class CreateTodoDtoValidator : AbstractValidator<CreateTodoDto>
    {
        public CreateTodoDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title tidak boleh kosong.")
                .MaximumLength(200).WithMessage("Title maksimal 200 karakter.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description maksimal 1000 karakter.");
        }
    }
}
