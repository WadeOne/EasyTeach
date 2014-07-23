using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Web.Models.ViewModels.Groups;

namespace EasyTeach.Web.Controllers
{
    public sealed class GroupsController : ODataController
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            if (groupService == null)
            {
                throw new ArgumentNullException("groupService");
            }

            _groupService = groupService;
        }

        [EnableQuery]
        [HttpGet]
        public IQueryable<GroupViewModel> Get()
        {
            return _groupService.GetAll().Select(g => new GroupViewModel
            {
                GroupId = g.GroupId,
                GroupNumber = g.GroupNumber,
                Year = g.Year,
                ContactEmail = g.ContactEmail,
                ContactName = g.ContactName,
                ContactPhone = g.ContactPhone
            });
        }

        [EnableQuery]
        [HttpPost]
        public async Task<IHttpActionResult> Post(CreateGroupViewModel group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            await _groupService.CreateGroupAsync(group.ToGroup());
            return Ok();
        }

        [EnableQuery]
        [HttpPut]
        public async Task<IHttpActionResult> Put(GroupViewModel group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            await _groupService.UpdateGroupAsync(group.ToGroup());
            return Ok();
        }

        [EnableQuery]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int groupId)
        {
            await _groupService.DeleteGroupAsync(groupId);
            return Ok();
        }
    }
}