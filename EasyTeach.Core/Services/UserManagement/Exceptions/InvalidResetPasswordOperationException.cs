using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyTeach.Core.Services.Base.Exceptions;

namespace EasyTeach.Core.Services.UserManagement.Exceptions
{
    public sealed class InvalidResetPasswordOperationException : ModelValidationException
    {
        public InvalidResetPasswordOperationException(IEnumerable<ValidationResult> validationResults)
            : base(validationResults.ToList())
        {
        }

        public InvalidResetPasswordOperationException(ValidationResult validationResult)
            : this(new List<ValidationResult> { validationResult })
        {
        }
    }
}