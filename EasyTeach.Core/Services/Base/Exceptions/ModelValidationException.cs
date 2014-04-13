using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Validation;

namespace EasyTeach.Core.Services.Base.Exceptions
{
    public class ModelValidationException: Exception, IErrorsAddable
    {
        public virtual IList<ValidationResult> ValidationResults { get; private set; }

        public virtual void AddErrors(IEnumerable<ValidationResult> additionalResults)
        {
            if (ValidationResults == null)
            {
                ValidationResults = new List<ValidationResult>();
            }

            foreach (var additionalResult in additionalResults)
            {
                ValidationResults.Add(additionalResult);
            }
        }

        public ModelValidationException(IList<ValidationResult> validationResults)
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
