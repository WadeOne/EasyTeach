using System.Security.Claims;
using Xunit;
using Xunit.Extensions;
using ClaimsAuthorizationManager = EasyTeach.Core.Security.ClaimsAuthorizationManager;

namespace EasyTeach.Core.Tests.Security
{
    public sealed class ClaimsAuthorizationManagerTest
    {
        private readonly ClaimsAuthorizationManager _claimsAuthorizationManager = new ClaimsAuthorizationManager();

        [Theory]
        [InlineData("Teacher", "User", "Register")]
        public void CheckAccess_ValidOperationResourceRoleClaim_True(string role, string resource, string operation)
        {
            var context = new AuthorizationContext(new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, role, ClaimValueTypes.String)
            })), resource, operation);
            Assert.True(_claimsAuthorizationManager.CheckAccess(context));
        }

        [Theory]
        [InlineData("", "User", "Register")]
        [InlineData("Teacher", "User", "ConfirmEmail")]
        [InlineData("Teacher", "User", "SetPassword")]
        [InlineData("Teacher", " ", "Register")]
        public void CheckAccess_InvalidOperationResourceRoleClaim_False(string role, string resource, string operation)
        {
            var context = new AuthorizationContext(new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, role, ClaimValueTypes.String)
            })), resource, operation);
            Assert.False(_claimsAuthorizationManager.CheckAccess(context));
        }
    }
}