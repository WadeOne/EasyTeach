using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Validation.EntityValidator
{
    public sealed class EntityValidationResult
    {
        public EntityValidationResult(bool isValid, IEnumerable<ValidationResult> validationResults = null)
        {
            ValidationResults = validationResults ?? new List<ValidationResult>();
            IsValid = isValid;
        }

        public readonly bool IsValid;

        public readonly IEnumerable<ValidationResult> ValidationResults;
    }
}
