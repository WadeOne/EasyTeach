using System;
using EasyTeach.Core.Entities.Data.Group;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers.UserManagement;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers
{
    public sealed class GroupDtoMapper : IGroupDtoMapper
    {
        public IGroupDto Map(IGroupModel group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            return new GroupDto
            {
                ContactEmail = group.ContactEmail,
                ContactName = group.ContactName,
                ContactPhone = group.ContactPhone,
                GroupId = group.GroupId,
                GroupNumber = group.GroupNumber,
                Year = group.Year
            };
        }
    }
}