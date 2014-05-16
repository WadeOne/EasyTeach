using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Services.Base.Exceptions;

namespace EasyTeach.Core.Services.Dashboard.Exceptions
{
    public sealed class InvalidVisitException : ModelValidationException
    {
        public InvalidVisitException(IEnumerable<ValidationResult> validationResults)
            : base(validationResults)
        {
        }

        public InvalidVisitException()
        {
        }
    }
}