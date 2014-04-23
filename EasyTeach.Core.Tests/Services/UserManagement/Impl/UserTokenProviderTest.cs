using System;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Services.UserManagement.Impl;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Core.Tests.Services.UserManagement.Impl
{
    public sealed class UserTokenProviderTest
    {
        private readonly UserTokenProvider _userTokenProvider;
        private readonly IUserTokenRepository _userTokenRepository;

        public UserTokenProviderTest()
        {
            _userTokenRepository = A.Fake<IUserTokenRepository>();
            _userTokenProvider = new UserTokenProvider(_userTokenRepository);
        }

        [Fact]
        public void NotifyAsync_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => _userTokenProvider.NotifyAsync(null, null, null));
        }

        [Fact]
        public void GenerateAsync_CreateAsyncCalled()
        {
            var user = A.Fake<IUserDto>();

             _userTokenProvider.GenerateAsync("purpose", null, user).Wait();

            A.CallTo(() => _userTokenRepository.CreateAsync("purpose", A<string>.Ignored, A<int>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void GenerateAsync_NotEmptyToken()
        {
            var user = A.Fake<IUserDto>();

            string token = _userTokenProvider.GenerateAsync("purpose", null, user).Result;

            Assert.NotEmpty(token);
        }

        [Fact]
        public void IsValidProviderForUserAsync_AlwaysTrue()
        {
            bool valid = _userTokenProvider.IsValidProviderForUserAsync(null, null).Result;
            Assert.True(valid);
        }

        [Fact]
        public void ValidateAsync_NotExsitingToken_False()
        {
            var user = A.Fake<IUserDto>();
            A.CallTo(() => _userTokenRepository.GetUserTokenAsync("purpose", "token", A<int>.Ignored)).Returns((IUserTokenDto)null);

            bool valid = _userTokenProvider.ValidateAsync("purpose", "token", null, user).Result;

            Assert.False(valid);
        }

        [Fact]
        public void ValidateAsync_ExsitingTokenNonExpired_True()
        {
            var token = A.Fake<IUserTokenDto>();
            A.CallTo(() => token.Created).Returns(DateTime.UtcNow.AddDays(-13));

            A.CallTo(() => _userTokenRepository.GetUserTokenAsync("purpose", "token", A<int>.Ignored)).Returns(token);

            bool valid = _userTokenProvider.ValidateAsync("purpose", "token", null, A.Fake<IUserDto>()).Result;

            Assert.True(valid);
        }

        [Fact]
        public void ValidateAsync_ExsitingTokenExpired_False()
        {
            var token = A.Fake<IUserTokenDto>();
            A.CallTo(() => token.Created).Returns(DateTime.UtcNow.AddDays(-15));

            A.CallTo(() => _userTokenRepository.GetUserTokenAsync("purpose", "token", A<int>.Ignored)).Returns(token);

            bool valid = _userTokenProvider.ValidateAsync("purpose", "token", null, A.Fake<IUserDto>()).Result;

            Assert.False(valid);
        }
    }
}