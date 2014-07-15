using System;
using System.Linq;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.UserManagement.Impl;
using EasyTeach.Core.Entities.Data.Group;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Core.Tests.Services.UserManagement.Impl
{
    public sealed class GroupServiceTest
    {
        private readonly GroupService _groupService;
        private readonly IGroupRepository _groupRepository;

        public GroupServiceTest()
        {
            _groupRepository = A.Fake<IGroupRepository>();
            _groupService = new GroupService(_groupRepository);
        }

        [Fact]
        public void RemoveGroup_NotExistingId_ThrowEntityNotFoundException()
        {
            A.CallTo(() => _groupRepository.GetGroups()).Returns(Enumerable.Empty<IGroupDto>().AsQueryable());
            A.CallTo(() => _groupRepository.GetGroupById(2)).Returns(null);

            Assert.Throws<EntityNotFoundException>(() => _groupService.DeleteGroupAsync(2));

            A.CallTo(() => _groupRepository.RemoveGroup(A<int>.Ignored)).MustNotHaveHappened();
        }
        /*
        [Fact]
        public void RemoveGroup_ExistingId_Removed()
        {
            A.CallTo(() => _groupRepository.GetGroups()).Returns(Enumerable.Empty<IGroupDto>().AsQueryable());
            A.CallTo(() => _groupRepository.GetGroupById(2)).Returns(A.Dummy<IGroupDto>());

            _groupService.DeleteGroupAsync(2);

            A.CallTo(() => _groupService.DeleteGroupAsync(2)).MustHaveHappened();
        }
         * */
    }
}
