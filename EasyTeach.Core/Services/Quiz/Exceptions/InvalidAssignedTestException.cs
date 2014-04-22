using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Services.Base.Exceptions;

namespace EasyTeach.Core.Services.Quiz.Exceptions
{
    public class InvalidAssignedTestException : ModelValidationException
    {
        public InvalidAssignedTestException(IEnumerable<ValidationResult> validationResults) : base(validationResults)
        {
        }

        public InvalidAssignedTestException()
        {
        }
    }
}
