using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Services.Base.Exceptions;

namespace EasyTeach.Core.Services.UserManagement.Exceptions
{
    public sealed class InvalidUserDataException : ModelValidationException
    {
        public InvalidUserDataException(IList<ValidationResult> validationResults)
            : base(validationResults)
        {
        }

        public InvalidUserDataException()
            : this(new List<ValidationResult>())
        {
        }
    }
}
