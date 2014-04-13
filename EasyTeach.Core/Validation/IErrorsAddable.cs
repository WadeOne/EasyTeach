using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Validation
{
    public interface IErrorsAddable
    {
        void AddErrors(IEnumerable<ValidationResult> additionalErrors);
    }
}