using System;
using System.Data.Entity;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;
using EasyTeach.Data.Repostitories;
using EasyTeach.Data.Tests.Context;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Data.Tests.Repostitories
{
    public sealed class UserTokenRepositoryTests
    {
        private readonly UserTokenRepository _repository;
        private readonly EasyTeachContext _context;

        public UserTokenRepositoryTests()
        {
            _context = A.Fake<EasyTeachContext>();
            _repository = new UserTokenRepository(_context);
        }

        [Fact]
        public void CreateAsync_ValidArguments_TokenAdd()
        {
            var tokens = A.Fake<IDbSet<UserTokenDto>>();
            A.CallTo(() => _context.UserTokens).Returns(tokens);

            _repository.CreateAsync("Test", "Test", 1).Wait();

            A.CallTo(() => tokens.Add(A<UserTokenDto>.That.Matches(t => t.Created > DateTime.UtcNow.AddSeconds(-5)))).MustHaveHappened();
            A.CallTo(() => _context.SaveChangesAsync()).MustHaveHappened();
        }

        [Fact]
        public void GetUserTokenAsync_ValidArguments_Token()
        {
            var existingToken = new UserTokenDto
            {
                Puprose = "purpose",
                Token = "token",
                UserId = 42
            };

            IDbSet<UserTokenDto> tokens = new FakeDbSet<UserTokenDto>(new[] { existingToken });
            A.CallTo(() => _context.UserTokens).Returns(tokens);

            IUserTokenDto token = _repository.GetUserTokenAsync("purpose", "token", 42).Result;

            Assert.Same(existingToken, token);
        }

        [Fact]
        public void GetUserTokenAsync_InvalidArguments_Null()
        {
            IDbSet<UserTokenDto> tokens = new FakeDbSet<UserTokenDto>();
            A.CallTo(() => _context.UserTokens).Returns(tokens);

            IUserTokenDto token = _repository.GetUserTokenAsync("purpose", "token", 42).Result;
            Assert.Null(token);
        }

    }
}