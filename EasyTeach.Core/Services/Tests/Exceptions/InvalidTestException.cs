using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Services.Base.Exceptions;

namespace EasyTeach.Core.Services.Tests.Exceptions
{
    public class InvalidTestException : ModelValidationException
    {
        public InvalidTestException(ICollection<ValidationResult> validationResults) : base(validationResults)
        {
        }

        public InvalidTestException()
        {
        }
    }
}
