using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Services.Base.Exceptions;

namespace EasyTeach.Core.Services.Dashboard.Exceptions
{
    public sealed class InvalidScoreException : ModelValidationException
    {
        public InvalidScoreException(IEnumerable<ValidationResult> validationResults)
            : base(validationResults)
        {
        }

        public InvalidScoreException()
        {
        }
    }
}
