using System;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using EasyTeach.Core.Repositories;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupReository)
        {
            if (groupReository == null)
            {
                throw new ArgumentNullException("groupReository");
            }
            _groupRepository = groupReository;
        }

        public IQueryable<IGroupModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task CreateGroupAsync(IGroupModel groupModel)
        {
            if (groupModel == null)
            {
                throw new ArgumentNullException("groupModel");
            }

            throw new NotImplementedException();
        }

        public Task UpdateGroupAsync(IGroupModel groupModel)
        {
            if (groupModel == null)
            {
                throw new ArgumentNullException("groupModel");
            }

            throw new NotImplementedException();
        }

        public Task DeleteGroupAsync(int groupId)
        {
            if (_groupRepository.GetGroupById(groupId) == null)
            {
                throw new EntityNotFoundException("group", groupId);
            }
            _groupRepository.RemoveGroup(groupId);

            return Task.FromResult<int>(0);
        }
    }
}