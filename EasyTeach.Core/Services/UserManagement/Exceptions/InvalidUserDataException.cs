using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EasyTeach.Core.Services.UserManagement.Exceptions
{
    public sealed class InvalidUserDataException : Exception
    {
        public IEnumerable<ValidationResult> ValidationResults { get; private set; }

        public InvalidUserDataException(IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults == null)
            {
                throw new ArgumentNullException("validationResults");
            }

            ValidationResults = validationResults;
        }

        public InvalidUserDataException()
            : this(Enumerable.Empty<ValidationResult>())
        {
        }
    }
}
