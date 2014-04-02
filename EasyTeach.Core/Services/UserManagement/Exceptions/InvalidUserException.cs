using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Services.UserManagement.Exceptions
{
    public class InvalidUserException : Exception
    {
        public ICollection<ValidationResult> ValidationResults { get; private set; }

        public InvalidUserException(ICollection<ValidationResult> validationResults)
        {
            ValidationResults = validationResults;
        }

        public InvalidUserException()
        {
        }
    }
}
