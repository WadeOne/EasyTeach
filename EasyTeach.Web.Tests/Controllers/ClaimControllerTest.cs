using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using EasyTeach.Core.Services.Claim;
using EasyTeach.Web.Controllers;
using EasyTeach.Web.Models.ViewModels.Claims;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Web.Tests.Controllers
{
    public sealed class ClaimControllerTest
    {
        private readonly ClaimController _claimController;
        private readonly IClaimService _claimService;

        public ClaimControllerTest()
        {
            _claimService = A.Fake<IClaimService>();
            _claimController = new ClaimController(_claimService);
        }

        [Fact]
        public void Get_CurrentUser_AllClaims()
        {
            var identity = A.Fake<IIdentity>();
            A.CallTo(() => identity.Name).Returns("John Doe");
            _claimController.User = new ClaimsPrincipal(identity);

            A.CallTo(() => _claimService.GetUserClaims(A<IIdentity>.Ignored)).Returns(new[]
            {
                new Claim("type", "value", "valueType")
            });

            IEnumerable<ClaimViewModel> claims = _claimController.Get();

            Assert.Equal(new[]
            {
                new ClaimViewModel
                {
                    Operation = "value",
                    Resource = "type"
                }
            }, claims.ToArray());
        }
    }
}