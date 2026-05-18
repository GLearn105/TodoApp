using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("Terjadi satu atau lebih error validasi.")
        {
            Errors = errors;
        }
    }
}
