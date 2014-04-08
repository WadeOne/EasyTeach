using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Services.UserManagement.Impl;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Core.Tests.Services.UserManagement.Impl
{
    public sealed class UserStoreTest
    {
        private readonly UserStore _userStore;
        private readonly IUserRepository _userRepository;

        public UserStoreTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _userStore = new UserStore(_userRepository);
        }

        [Fact]
        public void FindByNameAsync_ValidName_FindByEmailCalled()
        {
            IUserDto user = _userStore.FindByNameAsync("email@example.org").Result;
            A.CallTo(() => _userRepository.GetUserByEmail("email@example.org")).MustHaveHappened();
        }
    }
}