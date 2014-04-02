using System.Data.Entity;
using EasyTeach.Core.Entities;
using EasyTeach.Data.Context;
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
            IDbSet<User> users = A.Fake<IDbSet<User>>();
            A.CallTo(() => _context.Users).Returns(users);

            _userRepository.SaveUser(new User());

            A.CallTo(() => users.Add(A<User>.Ignored)).MustHaveHappened();
            A.CallTo(() => _context.SaveChanges()).MustHaveHappened();
        }
    }
}