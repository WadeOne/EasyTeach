using System.Data.Entity;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;
using EasyTeach.Data.Repostitories;
using EasyTeach.Data.Tests.Context;
using FakeItEasy;

using Xunit;

namespace EasyTeach.Data.Tests.Repostitories
{
    public sealed class UserRepositoryTest
    {
        private readonly UserRepository _userRepository;
        private readonly EasyTeachContext _context;

        public UserRepositoryTest()
        {
            _context = A.Fake<EasyTeachContext>();
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public void CreateUserAsync_NotNullUser_UserAdd()
        {
            IDbSet<UserDto> users = A.Fake<IDbSet<UserDto>>();
            A.CallTo(() => _context.Users).Returns(users);

            _userRepository.CreateAsync(new UserDto()).Wait();

            A.CallTo(() => users.Add(A<UserDto>.Ignored)).MustHaveHappened();
            A.CallTo(() => _context.SaveChangesAsync()).MustHaveHappened();
        }

        [Fact]
        public void GetUserByEmail_ExistingEmail_User()
        {
            var data = new[]
            {
                new UserDto { Email = "test" },
                new UserDto { Email = "john.doe@example.com" }
            };

            IDbSet<UserDto> users = new FakeDbSet<UserDto>(data);
            A.CallTo(() => _context.Users).Returns(users);

            IUserDto user = _userRepository.GetUserByEmail("john.doe@example.com").Result;

            Assert.Equal("john.doe@example.com", user.Email);
        }

        [Fact]
        public void GetUserByEmail_NonExistingEmail_Null()
        {
            IDbSet<UserDto> users = new FakeDbSet<UserDto>(new[] { new UserDto { Email = "test" } });
            A.CallTo(() => _context.Users).Returns(users);

            IUserDto user = _userRepository.GetUserByEmail("john.doe@example.com").Result;

            Assert.Null(user);
        }
    }
}