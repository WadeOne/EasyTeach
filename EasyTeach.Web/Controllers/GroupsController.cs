using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

using Microsoft.Data.OData;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
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

        [HttpGet]
        public IQueryable<GroupViewModel> Get()
        {
            var result = _groupService.GetAll().Select(g => new GroupViewModel
            {
                GroupId = g.GroupId,
                GroupNumber = g.GroupNumber,
                Year = g.Year,
                ContactEmail = g.ContactEmail,
                ContactName = g.ContactName,
                ContactPhone = g.ContactPhone
            });
            return result;
        }
        /*
        [HttpPost]
        public IHttpActionResult Post(GroupViewModel groupView)
        {

            IGroupModel g = groupView.ToGroup();
            _groupService.CreateGroupAsync(g);

            GroupViewModel gr = new GroupViewModel
            {
                GroupId = g.GroupId,
                GroupNumber = g.GroupNumber,
                Year = g.Year,
                ContactEmail = g.ContactEmail,
                ContactName = g.ContactName,
                ContactPhone = g.ContactPhone
            };

            return (GroupViewModel)gr;
        }
        */
        private static GroupViewModel MapProductToDto(IGroupModel g)
        {
            return new GroupViewModel
            {
                GroupId = g.GroupId,
                GroupNumber = g.GroupNumber,
                Year = g.Year,
                ContactEmail = g.ContactEmail,
                ContactName = g.ContactName,
                ContactPhone = g.ContactPhone
            };
        }

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

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int groupId)
        {
            await _groupService.DeleteGroupAsync(groupId);
            return Ok();
        }
    }
}