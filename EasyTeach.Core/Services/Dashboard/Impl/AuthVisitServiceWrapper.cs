using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Security;

namespace EasyTeach.Core.Services.Dashboard.Impl
{
    public sealed class AuthVisitServiceWrapper : IVisitService
    {
        private readonly IVisitService _visitService;
        private readonly ClaimsPrincipal _principal;
        private readonly IUserStore<IUserDto, int> _userStore;
        private readonly ClaimsAuthorizationManager _authorizationManager;

        public AuthVisitServiceWrapper(
            IVisitService visitService, 
            ClaimsPrincipal principal, 
            IUserStore<IUserDto, int> userStore,
            ClaimsAuthorizationManager authorizationManager)
        {
            if (visitService == null)
            {
                throw new ArgumentNullException("visitService");
            }
            _visitService = visitService;

            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }
            _principal = principal;

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

        public IQueryable<IVisitModel> GetGroupVisits(int groupId)
        {
            if (_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Visit", "Get")))
            {
                IUserDto user = _userStore.FindByNameAsync(_principal.Identity.Name).Result;
                if (user.GroupId != groupId && user.GroupId.HasValue)
                {
                    throw new SecurityException("User doesn't have enough permission for getting visit");
                }
            }
            return _visitService.GetGroupVisits(groupId);
        }

        public IQueryable<IVisitModel> GetGroupVisitsAvailableForStudent(IPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }

            IUserDto user = _userStore.FindByNameAsync(principal.Identity.Name).Result;
            if (_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Visit", "Get")))
            {
                throw new SecurityException("User doesn't have enough permission for getting visit");
            }
            return _visitService.GetGroupVisits(user.Group.GroupId);
        }

        public Task UpdateVisitAsync(IVisitModel visit)
        {
            if (visit == null)
            {
                throw new ArgumentNullException("visit");
            }

            IUserDto user = _userStore.FindByNameAsync(_principal.Identity.Name).Result;
            if (_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Visit", "Update")))
            {
                if (user.GroupId == null)
                {
                    throw new SecurityException("User doesn't have enough permission for update visit");
                }
            }

            return  _visitService.UpdateVisitAsync(visit);
        }
    }
}
