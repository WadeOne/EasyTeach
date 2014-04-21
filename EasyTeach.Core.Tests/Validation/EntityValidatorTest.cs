using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;

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
        public void ValidateEntity_ValidEntityWithoutAdditionalValidation_ResultIsValid()
        {
            var result =
                _entityValidator.ValidateEntity(_validEntity);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateEntity_InvalidEntityWithoutAdditionalValidation_ResultIsNotValid()
        {
            var result =
                _entityValidator.ValidateEntity(_invalidEntity);

            Assert.False(result.IsValid);
            Assert.True(result.ValidationResults.Any(x => x.MemberNames.First() == "RequiredStringProperty"));
            Assert.True(result.ValidationResults.All(x => x.MemberNames.First() != "NotRequiredStringProperty"));
            Assert.True(result.ValidationResults.All(x => x.MemberNames.First() != "PositiveIntegerProperty"));
        }
    }
}
