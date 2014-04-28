using System.Collections.Generic;
using System.Linq;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.UserManagement.Impl;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Core.Tests.Services.UserManagement.Impl
{
    using System.Security.Claims;

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
            _userStore.FindByNameAsync("email@example.org").Wait();
            A.CallTo(() => _userRepository.GetUserByEmail("email@example.org")).MustHaveHappened();
        }

        [Fact]
        public void GetClaimsAsync_ExistingUserId_UserClaimCollection()
        {
            var userClaim = A.Fake<IUserClaimDto>();
            A.CallTo(() => userClaim.ValueType).Returns("string");
            A.CallTo(() => userClaim.Type).Returns("role");
            A.CallTo(() => userClaim.Value).Returns("big boss");

            var user = A.Fake<IUserDto>();
            A.CallTo(() => user.UserId).Returns(42);

            A.CallTo(() => _userClaimRepository.GetUserClaims(42))
                .Returns(new[] { userClaim }.AsQueryable());

            IList<Claim> claims = _userStore.GetClaimsAsync(user).Result;
            Assert.Equal(1, claims.Count);
            Assert.Equal("role", claims[0].Type);
            Assert.Equal("big boss", claims[0].Value);
            Assert.Equal("string", claims[0].ValueType);
        }

        [Fact]
        public void AddClaimAsync_ValidUserAndClaim_MapUserClaimAndAddClaimAsyncCalled()
        {
            _userStore.AddClaimAsync(A.Dummy<IUserDto>(), A.Dummy<Claim>());
            A.CallTo(() => _userClaimRepository.AddClaimAsync(A<IUserClaimDto>.Ignored)).MustHaveHappened();
            A.CallTo(() => _userClaimDtoMapper.Map(A<IUserDto>.Ignored, A<Claim>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void RemoveClaimAsync_ValidUserAndClaim_MapUserClaimAndRemoveClaimAsyncCalled()
        {
            _userStore.RemoveClaimAsync(A.Dummy<IUserDto>(), A.Dummy<Claim>());
            A.CallTo(() => _userClaimRepository.RemoveClaimAsync(A<IUserClaimDto>.Ignored)).MustHaveHappened();
            A.CallTo(() => _userClaimDtoMapper.Map(A<IUserDto>.Ignored, A<Claim>.Ignored)).MustHaveHappened();
        }
    }
}