using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Validation.Attributes;
using Xunit;

namespace EasyTeach.Core.Tests.Validation.Attributes
{
    public class NotEmptyCollectionAttributeTest
    {
        private readonly NotEmptyCollectionAttribute _attribute;
        private readonly ValidationContext _validationContext;

        public NotEmptyCollectionAttributeTest()
        {
            _attribute = new NotEmptyCollectionAttribute();
            _validationContext = new ValidationContext(new object());

        }

        [Fact]
        public void GetValidationResult_NotEmptyCollection_ReturnedNull()
        {
            var notEmptyCollection = new List<int> {1, 2, 3};
            ValidationResult validationResult = _attribute.GetValidationResult(notEmptyCollection, _validationContext);

            Assert.Null(validationResult);
        }

        [Fact]
        public void GetValidationResult_EmptyCollection_ReturnedValidationResult()
        {
            var emptyCollection = new List<int>();
            ValidationResult validationResult = _attribute.GetValidationResult(emptyCollection, _validationContext);

            Assert.NotNull(validationResult);
            Assert.Equal(validationResult.ErrorMessage, "Collection is empty");
        }

        [Fact]
        public void GetValidationResult_NotCollection_ReturnedValidationResult()
        {
            var notCollectionObject = "Sample string";
            ValidationResult validationResult = _attribute.GetValidationResult(notCollectionObject, _validationContext);

            Assert.NotNull(validationResult);
            Assert.Equal(validationResult.ErrorMessage, "Field must be collection");
        }
    }
}
