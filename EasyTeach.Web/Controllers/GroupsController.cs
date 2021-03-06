﻿using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

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
        public IQueryable<GroupViewModel> Get(ODataQueryOptions<GroupViewModel> queryOptions)
        {
            var result = _groupService.GetAll().Select(g => new GroupViewModel
            {
                GroupId = g.GroupId,
                GroupNumber = g.GroupNumber,
                Year = g.Year,
                ContactEmail = g.ContactEmail,
                ContactName = g.ContactName,
                ContactPhone = g.ContactPhone,
                DisplayName = g.GroupNumber + " (" + g.Year + ")"
            });
            var filteredResult = ((IQueryable<GroupViewModel>)queryOptions.ApplyTo(result));
            return filteredResult;
        }

        [HttpPost]
        public IHttpActionResult Post(GroupViewModel groupView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var create = groupView.ToGroup();
            _groupService.CreateGroupAsync(create);

            return Created(MapGroupToView(create));
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, GroupViewModel groupView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_groupService.GetAll().Any(g => g.GroupId == key))
            {
                return BadRequest();
            }

            var update = groupView.ToGroup();
            _groupService.UpdateGroupAsync(update);

            return Updated(MapGroupToView(update));
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            IGroupModel group = _groupService.GetAll().FirstOrDefault(g => g.GroupId == key);

            if (group == null)
            {
                return NotFound();
            }

            _groupService.DeleteGroupAsync(key);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private static GroupViewModel MapGroupToView(IGroupModel g)
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
    }
}