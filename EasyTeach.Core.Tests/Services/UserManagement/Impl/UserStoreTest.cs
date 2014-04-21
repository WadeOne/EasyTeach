using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.UserManagement.Impl;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Core.Tests.Services.UserManagement.Impl
{
    public sealed class UserStoreTest
    {
        private readonly UserStore _userStore;
        private readonly IUserRepository _userRepository;
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly IUserClaimDtoMapper _userClaimDtoMapper;

        public UserStoreTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _userClaimRepository = A.Fake<IUserClaimRepository>();
            _userClaimDtoMapper = A.Fake<IUserClaimDtoMapper>();
            _userStore = new UserStore(_userRepository, _userClaimRepository, _userClaimDtoMapper);
        }

        [Fact]
        public void FindByNameAsync_ValidName_FindByEmailCalled()
        {
            IUserDto user = _userStore.FindByNameAsync("email@example.org").Result;
            Assert.Equal("email@example.org", user.Email);
            A.CallTo(() => _userRepository.GetUserByEmail("email@example.org")).MustHaveHappened();
        }
    }
}