using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Services.Claim.Impl;
using FakeItEasy;
using Microsoft.AspNet.Identity;
using Xunit;

namespace EasyTeach.Core.Tests.Services.Claim.Impl
{
    using System.Security.Principal;
    using System.Security.Claims;

    public sealed class ClaimServiceTest
    {
        private readonly ClaimService _claimService;
        private readonly IUserStore<IUserDto, int> _userStore;
        private readonly IUserClaimStore<IUserDto, int> _userClaimStore;

        public ClaimServiceTest()
        {
            _userClaimStore = A.Fake<IUserClaimStore<IUserDto, int>>();
            _userStore = A.Fake<IUserStore<IUserDto, int>>();
            _claimService = new ClaimService(_userStore, _userClaimStore);
        }

        [Fact]
        public void GetUserClaims_ExistingUser_UserClaims()
        {
            var identity = A.Fake<IIdentity>();
            A.CallTo(() => identity.Name).Returns("John Doe");

            var userDto = A.Fake<IUserDto>();

            A.CallTo(() => _userStore.FindByNameAsync("John Doe")).Returns(Task.FromResult(userDto));
            A.CallTo(() => _userClaimStore.GetClaimsAsync(userDto)).Returns(new List<Claim>
            {
                new Claim("type", "value", "valueType")
            });

            IEnumerable<Claim> claims = _claimService.GetUserClaims(identity);

            Assert.Equal("value", claims.Single().Value);
            Assert.Equal("type", claims.Single().Type);
            Assert.Equal("valueType", claims.Single().ValueType);
        }

        [Fact]
        public void GetUserClaims_NotExistingUser_ThrowException()
        {
            var identity = A.Fake<IIdentity>();
            A.CallTo(() => identity.Name).Returns("John Doe");

            A.CallTo(() => _userStore.FindByNameAsync("John Doe")).Returns(Task.FromResult<IUserDto>(null));

            Assert.Throws<InvalidOperationException>(() => _claimService.GetUserClaims(identity));
        }
    }
}