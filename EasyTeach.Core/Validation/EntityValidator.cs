using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Validation
{
    public class EntityValidator
    {
        public virtual T2 ValidateEntity<T1, T2>(T1 entity, Dictionary<Func<T1, bool>, ValidationResult> additionalValidation = null) where T2 : Exception, IErrorsAddable, new()
        {
            bool entityIsValid;
            var validationResults = ValidateEntityWithAnnotations(entity, out entityIsValid, additionalValidation);
            if (entityIsValid == false)
            {
                var exception = new T2();
                exception.AddErrors(validationResults);
                return exception;
            }

            return null;
        }

        private IEnumerable<ValidationResult> ValidateEntityWithAnnotations<T>(T entity, out bool entityIsValid, Dictionary<Func<T, bool>, ValidationResult> additionalValidation = null)
        {
            var validationResults = new List<ValidationResult>();
            entityIsValid = Validator.TryValidateObject(entity, new ValidationContext(entity, null, null),
                validationResults, true);

            if (additionalValidation != null)
            {
                foreach (var predicateWithResult in additionalValidation)
                {
                    if (predicateWithResult.Key(entity))
                    {
                        validationResults.Add(predicateWithResult.Value);
                        entityIsValid = false;
                    }
                }
            }

            return validationResults;
        }
    }
}
