using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Validation.Attributes
{
    public class NotEmptyCollectionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var collection = value as ICollection;
            if (collection == null)
            {
                return new ValidationResult("Field must be collection");
            }

            return collection.Count > 0 ? null : new ValidationResult("Collection is empty");
        }
    }
}
