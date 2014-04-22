using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyTeach.Core.Validation.Attributes;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Core.Tests.Validation.Attributes
{
    public class ComparisonAttributeTest
    {
        private readonly ComparisonAttribute _attribute;
        private readonly TestEntity _validEntity;
        private readonly TestEntity _invalidEntity;
        private const string _errorMessage = "FirstProperty and SecondProperty should be equal";

        private class TestEntity
        {
            public int FirstProperty { get; set; }

            public int SecondProperty { get; set; }
        }

        public ComparisonAttributeTest()
        {
            _validEntity = new TestEntity { FirstProperty = 10, SecondProperty = 10 };
            _invalidEntity = new TestEntity {FirstProperty = 10, SecondProperty = 20};
            _attribute = new ComparisonAttribute("SecondProperty", ComparisonAttribute.ComparisonCondition.Equal);
            _attribute.ErrorMessage = _errorMessage;
        }

        [Fact]
        public void GetValidationResult_PropertiesEqual_NoValidationResult()
        {
            var validationContext = new ValidationContext(_validEntity);
            ValidationResult validationResult = _attribute.GetValidationResult(_validEntity.FirstProperty, validationContext);

            Assert.Null(validationResult);
        }

        [Fact]
        public void GetValidationResult_PropertiesNotEqual_ReturnedValidationResult()
        {
            var validationContext = new ValidationContext(_invalidEntity);
            ValidationResult validationResult = _attribute.GetValidationResult(_invalidEntity.FirstProperty, validationContext);

            Assert.NotNull(validationResult);
            Assert.Equal(validationResult.ErrorMessage, _errorMessage);
        }
    }
}
