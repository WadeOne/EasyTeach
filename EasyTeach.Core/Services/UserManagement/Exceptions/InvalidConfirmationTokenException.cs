using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EasyTeach.Core.Services.UserManagement.Exceptions
{
    public sealed class InvalidConfirmationTokenException : Exception
    {
        public IEnumerable<ValidationResult> ValidationResults { get; private set; }

        public InvalidConfirmationTokenException(IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults == null)
            {
                throw new ArgumentNullException("validationResults");
            }

            ValidationResults = validationResults;
        }

        public InvalidConfirmationTokenException() : this(Enumerable.Empty<ValidationResult>())
        {
        }

        public InvalidConfirmationTokenException(ValidationResult validationResult) : this(new[]{ validationResult })
        {
            if (validationResult == null)
            {
                throw new ArgumentNullException("validationResult");
            }
        }
    }
}