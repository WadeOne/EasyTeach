using System;
using System.Linq;
using EasyTeach.Core.Entities.Data.Group;
using EasyTeach.Core.Repositories;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories
{
    public sealed class GroupRespository : IGroupRepository
    {
        private readonly EasyTeachContext _context;

        public GroupRespository(EasyTeachContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public void CreateGroup(IGroupDto group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            _context.Groups.Add((GroupDto)group);
            _context.SaveChanges();
        }

        public void RemoveGroup(int groupId)
        {
            GroupDto group = _context.Groups.Single(g => g.GroupId == groupId);
            _context.Groups.Remove(group);
            _context.SaveChanges();
        }

        public void UpdateGroup(IGroupDto group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            GroupDto oldGroup = _context.Groups.Single(g => g.GroupId == group.GroupId);
            oldGroup.ContactEmail = group.ContactEmail;
            oldGroup.ContactName = group.ContactName;
            oldGroup.ContactPhone = group.ContactPhone;
            oldGroup.GroupNumber = group.GroupNumber;
            oldGroup.Year = group.Year;
            _context.SaveChanges();
        }

        public IQueryable<IGroupDto> GetGroups()
        {
            return _context.Groups;
        }

        public IGroupDto GetGroupById(int groupId)
        {
            return _context.Groups.SingleOrDefault(g => g.GroupId == groupId);
        }
    }
}