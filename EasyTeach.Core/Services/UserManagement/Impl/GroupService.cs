using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Entities.Data.Group;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using EasyTeach.Core.Repositories.Mappers.UserManagement;
using EasyTeach.Core.Repositories;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class GroupService : IGroupService
    {
        private const int MinRequiredYear = 1900;
        private readonly EntityValidator _entityValidator;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupDtoMapper _groupnDtoMapper;

        public GroupService(EntityValidator entityValidator, IGroupRepository groupReository, IGroupDtoMapper groupDtoMapper)
        {
            if (groupReository == null)
            {
                throw new ArgumentNullException("groupReository");
            }
            _groupRepository = groupReository;

            if (entityValidator == null)
            {
                throw new ArgumentNullException("entityValidator");
            }
            _entityValidator = entityValidator;

            if (groupDtoMapper == null)
            {
                throw new ArgumentNullException("lessonDtoMapper");
            }
            _groupnDtoMapper = groupDtoMapper;
        }

        public IQueryable<IGroupModel> GetAll()
        {
            return _groupRepository.GetGroups().Select(Map).AsQueryable();
        }

        public Task CreateGroupAsync(IGroupModel groupModel)
        {
            if (groupModel == null)
            {
                throw new ArgumentNullException("groupModel");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(groupModel);
            if (result.IsValid == false)
            {
                throw new InvalidGroupException(result.ValidationResults);
            }
            if (_groupRepository.GetGroups().Any(g => g.GroupNumber == groupModel.GroupNumber && g.GroupId == groupModel.GroupId))
            {
                throw new GroupNotFoundException();
            }
            _groupRepository.CreateGroup(_groupnDtoMapper.Map(groupModel));

            return Task.FromResult<int>(0);
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

        private Group Map(IGroupDto group)
        {
            return new Group
            {
                GroupId = group.GroupId,
                GroupNumber = group.GroupNumber,
                Year = group.Year,
                ContactEmail = group.ContactEmail,
                ContactPhone = group.ContactPhone,
                ContactName = group.ContactName
            };
        }
    }
}