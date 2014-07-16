using System;
using System.Linq;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.UserManagement.Impl;
using EasyTeach.Core.Entities.Data.Group;
using EasyTeach.Core.Validation.EntityValidator;
using EasyTeach.Core.Repositories.Mappers.UserManagement;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Core.Tests.Services.UserManagement.Impl
{
    public sealed class GroupServiceTest
    {
        private readonly GroupService _groupService;
        private readonly IGroupRepository _groupRepository;
        private readonly EntityValidator _entityValidator;
        private readonly IGroupDtoMapper _groupDtoMapper;

        public GroupServiceTest()
        {
            _entityValidator = A.Fake<EntityValidator>();
            _groupRepository = A.Fake<IGroupRepository>();
            _groupDtoMapper = A.Fake<IGroupDtoMapper>();
            _groupService = new GroupService(_entityValidator, _groupRepository, _groupDtoMapper);
        }

        [Fact]
        public void CreateGroup_ValidModel_CreateGroupCalled()
        {
            IGroupModel group = new Group();
            A.CallTo(() => _entityValidator.ValidateEntity(group)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _groupRepository.GetGroups()).Returns(Enumerable.Empty<IGroupDto>().AsQueryable());
            A.CallTo(() => _groupDtoMapper.Map(group)).Returns(A.Dummy<IGroupDto>());
            _groupService.CreateGroupAsync(group);

            A.CallTo(() => _groupRepository.CreateGroup(A<IGroupDto>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void CreateGroup_InValidModel_InvalidGroupException()
        {
            IGroupModel group = new Group();
            A.CallTo(() => _entityValidator.ValidateEntity(group)).Returns(new EntityValidationResult(false));
            Assert.Throws<InvalidGroupException>(() => _groupService.CreateGroupAsync(group));
            A.CallTo(() => _groupRepository.CreateGroup(A<IGroupDto>.Ignored)).MustNotHaveHappened();
            
        }

        [Fact]
        public void RemoveGroup_NotExistingId_ThrowEntityNotFoundException()
        {
            A.CallTo(() => _groupRepository.GetGroups()).Returns(Enumerable.Empty<IGroupDto>().AsQueryable());
            A.CallTo(() => _groupRepository.GetGroupById(2)).Returns(null);

            Assert.Throws<EntityNotFoundException>(() => _groupService.DeleteGroupAsync(2));

            A.CallTo(() => _groupRepository.RemoveGroup(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void RemoveGroup_ExistingId_Removed()
        {
            A.CallTo(() => _groupRepository.GetGroups()).Returns(Enumerable.Empty<IGroupDto>().AsQueryable());
            A.CallTo(() => _groupRepository.GetGroupById(2)).Returns(A.Dummy<IGroupDto>());

            _groupService.DeleteGroupAsync(2);

            A.CallTo(() => _groupRepository.RemoveGroup(2)).MustHaveHappened();
        }
    }
}
