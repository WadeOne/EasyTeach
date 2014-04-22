using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Validation.EntityValidator
{
    public class EntityValidator
    {
        public virtual EntityValidationResult ValidateEntity<T1>(T1 entity)
        {
            return ValidateEntity(entity, new ValidationContext(entity, null, null));
        }
        public virtual EntityValidationResult ValidateEntity<T1>(T1 entity, ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            bool entityIsValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);
            if (entityIsValid == false)
            {
                return new EntityValidationResult(false, validationResults);
            }

            return new EntityValidationResult(true);
        }
    }
}
