using System;
using System.Data.Entity;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Repositories;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;
using EasyTeach.Data.Repostitories;
using EasyTeach.Data.Tests.Context;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Data.Tests.Repostitories
{
    public sealed class UserClaimRepositoryTest
    {
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly EasyTeachContext _context;

        public UserClaimRepositoryTest()
        {
            _context = A.Fake<EasyTeachContext>();
            _userClaimRepository = new UserClaimRepository(_context);
        }

        [Fact]
        public void GetUserClaims_ExistingUserId_NotEmpty()
        {
            var userClaims = new FakeDbSet<UserClaimDto>(new[]
            {
                new UserClaimDto { User = new UserDto {UserId = 42}}
            });
            A.CallTo(() => _context.UserClaims).Returns(userClaims);

            Assert.NotEmpty(_userClaimRepository.GetUserClaims(42));
        }

        [Fact]
        public void AddClaimAsync_UserClaimDto_UserClaimAdd()
        {
            IDbSet<UserClaimDto> userClaims = A.Fake<IDbSet<UserClaimDto>>();
            A.CallTo(() => _context.UserClaims).Returns(userClaims);

            _userClaimRepository.AddClaimAsync(new UserClaimDto()).Wait();

            A.CallTo(() => userClaims.Add(A<UserClaimDto>.Ignored)).MustHaveHappened();
            A.CallTo(() => _context.SaveChangesAsync()).MustHaveHappened();
        }

        [Fact]
        public void AddClaimAsync_NotUserClaimDto_ThrowInvalidCastException()
        {
            Assert.Throws<InvalidCastException>(() => _userClaimRepository.AddClaimAsync(A.Fake<IUserClaimDto>()).Wait());
        }
    }
}