using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;
using EasyTeach.Data.Repostitories;

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
        public void SaveUser_NotNullUser_UserAdd()
        {
            IDbSet<UserDto> users = A.Fake<IDbSet<UserDto>>();
            A.CallTo(() => _context.Users).Returns(users);

            _userRepository.SaveUser(new UserDto());

            A.CallTo(() => users.Add(A<UserDto>.Ignored)).MustHaveHappened();
            A.CallTo(() => _context.SaveChanges()).MustHaveHappened();
        }

        [Fact]
        public void GetUserByEmail_ExistingEmail_User()
        {
            var data = new List<UserDto>
            {
                new UserDto { Email = "test" },
                new UserDto { Email = "john.doe@example.com" }
            };

            IDbSet<UserDto> users = GetFakeDbSet(data.AsQueryable());
            A.CallTo(() => _context.Users).Returns(users);

            IUserDto user = _userRepository.GetUserByEmail("john.doe@example.com");

            Assert.Equal("john.doe@example.com", user.Email);
        }

        [Fact]
        public void GetUserByEmail_NonExistingEmail_Null()
        {
            var data = new List<UserDto> { new UserDto { Email = "test" } };
            IDbSet<UserDto> users = GetFakeDbSet(data.AsQueryable());
            A.CallTo(() => _context.Users).Returns(users);

            IUserDto user = _userRepository.GetUserByEmail("john.doe@example.com");

            Assert.Null(user);
        }

        private IDbSet<T> GetFakeDbSet<T>(IQueryable<T> data) where T : class 
        {
            IDbSet<T> dataSet = A.Fake<IDbSet<T>>();
            A.CallTo(() => dataSet.Provider).Returns(data.Provider);
            A.CallTo(() => dataSet.Expression).Returns(data.Expression);
            A.CallTo(() => dataSet.ElementType).Returns(data.ElementType);
            A.CallTo(() => dataSet.GetEnumerator()).Returns(data.GetEnumerator());
            return dataSet;
        }
    }
}