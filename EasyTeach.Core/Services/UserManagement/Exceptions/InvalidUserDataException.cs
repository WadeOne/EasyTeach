using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Services.UserManagement.Exceptions
{
    public sealed class InvalidUserDataException : Exception
    {
        public ICollection<ValidationResult> ValidationResults { get; private set; }

        public InvalidUserDataException(ICollection<ValidationResult> validationResults)
        {
            if (validationResults == null)
            {
                throw new ArgumentNullException("validationResults");
            }

            ValidationResults = validationResults;
        }

        public InvalidUserDataException()
        {
        }
    }
}
