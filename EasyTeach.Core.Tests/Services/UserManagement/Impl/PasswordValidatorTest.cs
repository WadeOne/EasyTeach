using Microsoft.AspNet.Identity;
using Xunit;
using PasswordValidator = EasyTeach.Core.Services.UserManagement.Impl.PasswordValidator;

namespace EasyTeach.Core.Tests.Services.UserManagement.Impl
{
    public sealed class PasswordValidatorTest
    {
        private readonly PasswordValidator _passwordValidator = new PasswordValidator();

        [Fact]
        public void ValidateAsync_NullPassword_Failed()
        {
            IdentityResult identityResult = _passwordValidator.ValidateAsync(null).Result;
            Assert.False(identityResult.Succeeded);
        }

        [Fact]
        public void ValidateAsync_ShortPassword_Failed()
        {
            IdentityResult identityResult = _passwordValidator.ValidateAsync("1234567").Result;
            Assert.False(identityResult.Succeeded);
        }

        [Fact]
        public void ValidateAsync_AlphaNumbericPassword_Failed()
        {
            IdentityResult identityResult = _passwordValidator.ValidateAsync("qwerty1234567").Result;
            Assert.False(identityResult.Succeeded);
        }

        [Fact]
        public void ValidateAsync_StrongPassword_Passed()
        {
            IdentityResult identityResult = _passwordValidator.ValidateAsync("p@ssow14eg_shake_easy_teach").Result;
            Assert.True(identityResult.Succeeded);
        }
    }
}