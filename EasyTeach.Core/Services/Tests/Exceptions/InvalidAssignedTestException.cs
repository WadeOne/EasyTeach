using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using EasyTeach.Core.Services.Base.Exceptions;

namespace EasyTeach.Core.Services.Tests.Exceptions
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
