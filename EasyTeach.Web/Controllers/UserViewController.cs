using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Web.Models.ViewModels.UserManagement;

namespace EasyTeach.Web.Controllers
{
    public sealed class UserViewController : ODataController
    {
        private readonly IUserService _userService;

        public UserViewController(IUserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            _userService = userService;
        }

        [HttpGet]
        public IQueryable<UserViewModel> Get(ODataQueryOptions<UserViewModel> queryOptions)
        {
            var result = _userService.GetUsers().Select(s => new UserViewModel
            {
                UserId = s.UserId,
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                DisplayName = s.FirstName + " " + s.LastName,
                GroupId = s.Group == null ? (int?)null : s.Group.GroupId
            });
            var filteredResult = ((IQueryable<UserViewModel>)queryOptions.ApplyTo(result));
            return filteredResult;
        }
    }
}
