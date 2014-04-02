using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Exceptions
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
