using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Services.Base.Exceptions
{
    public class ModelValidationException: Exception
    {
        public virtual IEnumerable<ValidationResult> ValidationResults { get; private set; }

        public ModelValidationException(IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults == null)
            {
                throw new ArgumentNullException("validationResults");
            }

            ValidationResults = validationResults;
        }

        public ModelValidationException()
        {
        }
    }
}
