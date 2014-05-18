using System;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Web.Models.ViewModels.Groups;

namespace EasyTeach.Web.Controllers
{
    [RoutePrefix("api/Visit")]
    public sealed class GroupController : ApiControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            if (groupService == null)
            {
                throw new ArgumentNullException("groupService");
            }

            _groupService = groupService;
        }


        [Route("")]
        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "GetAll", Resource = "Group")]
        public IQueryable<GroupViewModel> Get()
        {
            var groups = _groupService.GetAll().Select(g => new GroupViewModel
            {
                GroupId = g.GroupId,
                GroupNumber = g.GroupNumber,
                Year = g.Year,
                ContactEmail = g.ContactEmail,
                ContactName = g.ContactName,
                ContactPhone = g.ContactPhone
            });

            return groups;
        }


        [Route("")]
        [HttpPost]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "Create", Resource = "Group")]
        public async Task<IHttpActionResult> Post(CreateGroupViewModel group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            await _groupService.CreateGroupAsync(group.ToGroup());
            return Ok();
        }


        [Route("")]
        [HttpPut]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "Update", Resource = "Group")]
        public async Task<IHttpActionResult> Put(GroupViewModel group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            await _groupService.UpdateGroupAsync(group.ToGroup());
            return Ok();
        }

        [Route("")]
        [HttpPut]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "Delete", Resource = "Group")]
        public async Task<IHttpActionResult> Delete(int groupId)
        {
            await _groupService.DeleteGroupAsync(groupId);
            return Ok();
        }
    }
}