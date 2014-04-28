using System.Security.Claims;
using Xunit;
using Xunit.Extensions;
using ClaimsAuthorizationManager = EasyTeach.Core.Security.ClaimsAuthorizationManager;

namespace EasyTeach.Core.Tests.Security
{
    public sealed class ClaimsAuthorizationManagerTest
    {
        private readonly ClaimsAuthorizationManager _claimsAuthorizationManager = new ClaimsAuthorizationManager();

        private readonly static Claim[] Claims = new Claim[]
        {
            new Claim("User", "Register"),
            new Claim("User", "Confirm"),
            new Claim("Quiz", "Create"),
            new Claim("Quiz", "Update"),
            new Claim("Quiz", "Delete"),
        };

        [Theory]
        [InlineData("User", "Register")]
        [InlineData("Quiz", "Update")]
        public void CheckAccess_ValidOperationResourceClaim_True(string resource, string operation)
        {
            var context = new AuthorizationContext(new ClaimsPrincipal(new ClaimsIdentity(Claims)), resource, operation);
            Assert.True(_claimsAuthorizationManager.CheckAccess(context));
        }

        [Theory]
        [InlineData("User", "Quiz")]
        [InlineData("User", "ConfirmEmail")]
        [InlineData("Quiz", "SetPassword")]
        [InlineData(" ", "Register")]
        public void CheckAccess_InvalidOperationResourceClaim_False(string resource, string operation)
        {
            var context = new AuthorizationContext(new ClaimsPrincipal(new ClaimsIdentity(Claims)), resource, operation);
            Assert.False(_claimsAuthorizationManager.CheckAccess(context));
        }
    }
}