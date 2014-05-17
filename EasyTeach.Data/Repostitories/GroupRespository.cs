using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateGroupAsync(IGroupDto group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            _context.Groups.Add((GroupDto)group);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveGroupAsync(int groupId)
        {
            GroupDto group = await _context.Groups.SingleAsync(g => g.GroupId == groupId);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroupAsync(IGroupDto group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }

            GroupDto oldGroup = await _context.Groups.SingleAsync(g => g.GroupId == group.GroupId);
            oldGroup.ContactEmail = group.ContactEmail;
            oldGroup.ContactName = group.ContactName;
            oldGroup.ContactPhone = group.ContactPhone;
            oldGroup.GroupNumber = group.GroupNumber;
            oldGroup.Year = group.Year;
            await _context.SaveChangesAsync();
        }

        public IQueryable<IGroupDto> GetGroups()
        {
            return _context.Groups;
        }

        public async Task<IGroupDto> GetGroupByIdAsync(int groupId)
        {
            return await _context.Groups.SingleOrDefaultAsync(g => g.GroupId == groupId);
        }
    }
}