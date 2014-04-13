using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Validation;
using Xunit;

namespace EasyTeach.Core.Tests.Validation
{
    public class EntityValidatorTest
    {
        private class TestEntityWithDataAnnotations
        {
            [Required]
            public string RequiredStringProperty { get; set; }

            public string NotRequiredStringProperty { get; set; }

            public int PositiveIntegerProperty { get; set; }
        }

        private readonly EntityValidator _entityValidator;

        private readonly TestEntityWithDataAnnotations _validEntity;
        private readonly TestEntityWithDataAnnotations _invalidEntity;

        public EntityValidatorTest()
        {
            _entityValidator = new EntityValidator();
            _validEntity = new TestEntityWithDataAnnotations
            {
                RequiredStringProperty = "property",
                PositiveIntegerProperty = 10
            };

            _invalidEntity = new TestEntityWithDataAnnotations
            {
                PositiveIntegerProperty = -1
            };
        }

        [Fact]
        public void ValidateEntity_ValidEntityWithoutAdditionalValidation_ReturnedNull()
        {
            var resultException =
                _entityValidator.ValidateEntity<TestEntityWithDataAnnotations, ModelValidationException>(_validEntity);

            Assert.Null(resultException);
        }

        [Fact]
        public void ValidateEntity_ValidEntityWithAdditionalValidation_ReturnedNull()
        {
            var additionalValidation = new Dictionary<Func<TestEntityWithDataAnnotations, bool>, ValidationResult>
            {
                {
                    x => x.PositiveIntegerProperty <= 0,
                    new ValidationResult("Should be positive", new[] {"PositiveIntegerProperty"})
                }
            };
            var resultException =
                _entityValidator.ValidateEntity<TestEntityWithDataAnnotations, ModelValidationException>(_validEntity, additionalValidation);

            Assert.Null(resultException);
        }

        [Fact]
        public void ValidateEntity_InvalidEntityWithoutAdditionalValidation_ReturnedModelValidationException()
        {
            var resultException =
                _entityValidator.ValidateEntity<TestEntityWithDataAnnotations, ModelValidationException>(_invalidEntity);

            Assert.NotNull(resultException);
            Assert.IsAssignableFrom<ModelValidationException>(resultException);
            Assert.True(((ModelValidationException)resultException).ValidationResults.Any(x => x.MemberNames.First() == "RequiredStringProperty"));
            Assert.True(((ModelValidationException)resultException).ValidationResults.All(x => x.MemberNames.First() != "NotRequiredStringProperty"));
            Assert.True(((ModelValidationException)resultException).ValidationResults.All(x => x.MemberNames.First() != "PositiveIntegerProperty"));
        }

        [Fact]
        public void ValidateEntity_InvalidEntityWithAdditionalValidation_ReturnedModelValidationException()
        {
            var additionalValidation = new Dictionary<Func<TestEntityWithDataAnnotations, bool>, ValidationResult>
            {
                {
                    x => x.PositiveIntegerProperty <= 0,
                    new ValidationResult("Should be positive", new[] {"PositiveIntegerProperty"})
                }
            };

            var resultException =
                _entityValidator.ValidateEntity<TestEntityWithDataAnnotations, ModelValidationException>(_invalidEntity, additionalValidation);
            Assert.NotNull(resultException);
            Assert.IsAssignableFrom<ModelValidationException>(resultException);
            Assert.True(((ModelValidationException)resultException).ValidationResults.Any(x => x.MemberNames.First() == "RequiredStringProperty"));
            Assert.True(((ModelValidationException)resultException).ValidationResults.Any(x => x.MemberNames.First() == "PositiveIntegerProperty"));
            Assert.True(((ModelValidationException)resultException).ValidationResults.All(x => x.MemberNames.First() != "NotRequiredStringProperty"));
        }
    }
}
