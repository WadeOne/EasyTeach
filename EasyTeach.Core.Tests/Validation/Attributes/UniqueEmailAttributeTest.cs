using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Validation.Attributes;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Core.Tests.Validation.Attributes
{
    public class UniqueEmailAttributeTest
    {
        private readonly UniqueEmailAttribute _attribute;
        private readonly IUserRepository _repository;

        private readonly ValidationContext _validationContext;

        private const string Email = "Email@Email.Email";

        public UniqueEmailAttributeTest()
        {
            _attribute = new UniqueEmailAttribute();
            _repository = A.Fake<IUserRepository>();
            _validationContext = new ValidationContext(new object());
            _validationContext.ServiceContainer.AddService(typeof(IUserRepository), _repository);
        }

        [Fact]
        public void GetValidationResult_EmailIsUnique_ReturnedNull()
        {
            A.CallTo(() => _repository.GetUserByEmail(Email)).Returns((IUserDto)null);

            var validationResult = _attribute.GetValidationResult(Email, _validationContext);

            Assert.Null(validationResult);
        }

        [Fact]
        public void GetValidationResult_EmailIsNotUnique_ReturnedValidationResult()
        {
            A.CallTo(() => _repository.GetUserByEmail(Email)).Returns(A.Fake<IUserDto>());

            var validationResult = _attribute.GetValidationResult(Email, _validationContext);

            Assert.NotNull(validationResult);
        }

        [Fact]
        public void GetValidationResult_CannotFindRepository()
        {
            _validationContext.ServiceContainer.RemoveService(typeof(IUserRepository));

            Assert.Throws<InvalidOperationException>(() => _attribute.GetValidationResult(Email, _validationContext));
        }
    }
}
