using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName, object key)
            : base($"{resourceName} dengan id '{key}' tidak ditemukan.")
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
