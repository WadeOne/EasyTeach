using System;
using System.Collections.Generic;
using System.Security.Claims;
using EasyTeach.Core.Validation.EntityValidator;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;
using Microsoft.AspNet.Identity;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using System.Security;
using NLog;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class AuthGroupServiceWrapper : IGroupService
    {
        private readonly IGroupService _groupService;
        private readonly ClaimsPrincipal _principal;
        private readonly EntityValidator _entityValidator;
        private readonly IUserStore<IUserDto, int> _userStore;
        private readonly ClaimsAuthorizationManager _authorizationManager;

        public AuthGroupServiceWrapper(
            IGroupService groupService, 
            ClaimsPrincipal principal, 
            EntityValidator entityValidator,
            IUserStore<IUserDto, int> userStore,
            ClaimsAuthorizationManager authorizationManager)
        {
            if (groupService == null)
            {
                throw new ArgumentNullException("groupService");
            }
            _groupService = groupService;

            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }
            _principal = principal;

            if (entityValidator == null)
            {
                throw new ArgumentNullException("entityValidator");
            }
            _entityValidator = entityValidator;

            if (userStore == null)
            {
                throw new ArgumentNullException("userStore");
            }
            _userStore = userStore;

            if (authorizationManager == null)
            {
                throw new ArgumentNullException("authorizationManager");
            }
            _authorizationManager = authorizationManager;
        }

        public IQueryable<IGroupModel> GetAll()
        {
            return _groupService.GetAll();
        }

        public Task CreateGroupAsync(IGroupModel groupModel)
        {
            if (groupModel == null)
            {
                throw new ArgumentNullException("group");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(groupModel);
            if (result.IsValid == false)
            {
                throw new InvalidGroupException(result.ValidationResults);
            }

            if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Group", "Create")))
            {
                throw new SecurityException("User doesn't have enough permission for creating group");
            }

            return _groupService.CreateGroupAsync(groupModel);
        }

        public Task UpdateGroupAsync(IGroupModel groupModel)
        {
            if (groupModel == null)
            {
                throw new ArgumentNullException("group");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(groupModel);
            if (result.IsValid == false)
            {
                throw new InvalidGroupException(result.ValidationResults);
            }

            if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Group", "Update")))
            {
                throw new SecurityException("User doesn't have enough permission for update group");
            }

            return _groupService.UpdateGroupAsync(groupModel);
        }

        public Task DeleteGroupAsync(int groupId)
        {
            if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Group", "Delete")))
            {
                throw new SecurityException("User doesn't have enough permission for delete group");
            }
            return _groupService.DeleteGroupAsync(groupId);
        }
    }
}
