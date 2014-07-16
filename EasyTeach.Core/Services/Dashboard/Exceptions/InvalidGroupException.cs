using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Services.Base.Exceptions;

namespace EasyTeach.Core.Services.Dashboard.Exceptions
{
    public sealed class InvalidGroupException : ModelValidationException
    {
        public InvalidGroupException(IEnumerable<ValidationResult> validationResults)
            : base(validationResults)
        {
        }

        public InvalidGroupException()
        {
        }
    }
}
