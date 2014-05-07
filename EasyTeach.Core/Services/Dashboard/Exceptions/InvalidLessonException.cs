using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Services.Base.Exceptions;

namespace EasyTeach.Core.Services.Dashboard.Exceptions
{
    public sealed class InvalidLessonException : ModelValidationException
    {
        public InvalidLessonException(IEnumerable<ValidationResult> validationResults)
            : base(validationResults)
        {
        }

        public InvalidLessonException()
        {
        }
    }
}