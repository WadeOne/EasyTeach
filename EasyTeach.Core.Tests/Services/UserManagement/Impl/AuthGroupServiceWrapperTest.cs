using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Dashboard.Impl;
using EasyTeach.Core.Validation.EntityValidator;
using FakeItEasy;
using Microsoft.AspNet.Identity;
using Xunit;
using System.Threading.Tasks;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Impl;
using System.Security;

namespace EasyTeach.Core.Tests.Services.UserManagement.Impl
{
    public sealed class AuthGroupServiceWrapperTest
    {
        private readonly IGroupService _groupService;
        private readonly ClaimsPrincipal _principal;
        private readonly IIdentity _identity;
        private readonly EntityValidator _entityValidator;
        private readonly IUserStore<IUserDto, int> _userStore;
        private readonly ClaimsAuthorizationManager _authorizationManager;
        private readonly AuthGroupServiceWrapper _authGroupServiceWrapper;

        public AuthGroupServiceWrapperTest()
        {
            _groupService = A.Fake<IGroupService>();
            _principal = A.Fake<ClaimsPrincipal>();
            _identity = A.Fake<IIdentity>();

            A.CallTo(() => _principal.Identity).Returns(_identity);
            A.CallTo(() => _identity.IsAuthenticated).Returns(true);

            _entityValidator = A.Fake<EntityValidator>();
            _userStore = A.Fake<IUserStore<IUserDto, int>>();
            _authorizationManager = new ClaimsAuthorizationManager();

            _authGroupServiceWrapper = new AuthGroupServiceWrapper(
                _groupService,
                _principal,
                _entityValidator,
                _userStore,
                _authorizationManager);
        }

        [Fact]
        public void CreateGroup_GrantAccess_CreateGroupCalled()
        {
            var group = A.Fake<IGroupModel>();
            A.CallTo(() => _entityValidator.ValidateEntity(group)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _principal.HasClaim("Group", "Create")).Returns(true);
            _authGroupServiceWrapper.CreateGroupAsync(group);
            A.CallTo(() => _groupService.CreateGroupAsync(group)).MustHaveHappened();
        }

        [Fact]
        public void CreateGroup_DenyAccess_ThrowSecurityException()
        {
            var group = A.Fake<IGroupModel>();
            A.CallTo(() => _entityValidator.ValidateEntity(group)).Returns(new EntityValidationResult(true));
            A.CallTo(() => A.Fake<ClaimsAuthorizationManager>().CheckAccess(new AuthorizationContext(_principal, "Group", "Create"))).Returns(false);
            Assert.Throws<SecurityException>(() => _authGroupServiceWrapper.CreateGroupAsync(group));
            A.CallTo(() => _groupService.CreateGroupAsync(group)).MustNotHaveHappened();
        }
    }
}
