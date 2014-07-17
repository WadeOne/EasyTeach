using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Core.Validation.EntityValidator;
using EasyTeach.Core.Services.Dashboard.Impl;
using FakeItEasy;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data.Group;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.User;

namespace EasyTeach.Core.Tests.Services.Dashboard.Impl
{
    public sealed class VisitServiceTest
    {
        private readonly VisitService _visitService;
        private readonly EntityValidator _entityValidator;
        private readonly IVisitRepository _visitRepository;
        private readonly IVisitDtoMapper _visitDtoMapper;
        private readonly ILessonRepository _lessonRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;


        public VisitServiceTest()
        {
            _entityValidator = A.Fake<EntityValidator>();
            _visitRepository = A.Fake<IVisitRepository>();
            _visitDtoMapper = A.Fake<IVisitDtoMapper>();
            _lessonRepository = A.Fake<ILessonRepository>();
            _userRepository = A.Fake<IUserRepository>();

            _visitService = new VisitService(_visitRepository, _lessonRepository, _userRepository,  _entityValidator, _visitDtoMapper);
        }

        [Fact]
        public void GetGroupVisits_AlgorithmWorksCorrectly()
        {
            var groupDto = A.Fake<IGroupDto>();

            var userDto1 = A.Fake<IUserDto>();
            A.CallTo(() => userDto1.UserId).Returns(1);
            var userDto2 = A.Fake<IUserDto>();
            A.CallTo(() => userDto1.UserId).Returns(2);
            var userDto3 = A.Fake<IUserDto>();
            A.CallTo(() => userDto1.UserId).Returns(3);

            var visitDto1 = A.Fake<IVisitDto>();
            A.CallTo(() => userDto1.UserId).Returns(1);
            var visitDto2 = A.Fake<IVisitDto>();
            A.CallTo(() => userDto1.UserId).Returns(2);
            var visitDto3 = A.Fake<IVisitDto>();
            A.CallTo(() => userDto1.UserId).Returns(3);

            var lessonDto = A.Fake<ILessonDto>();

            A.CallTo(() => _userRepository.GetUsers()).Returns((new[] { userDto1, userDto2, userDto3 }).AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessons()).Returns((new[] { lessonDto }).AsQueryable());
            A.CallTo(() => _visitRepository.GetAll()).Returns((new[] { visitDto1, visitDto2, visitDto3 }).AsQueryable());


            A.CallTo(() => _entityValidator.ValidateEntity(groupDto)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _entityValidator.ValidateEntity(userDto1)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _entityValidator.ValidateEntity(userDto2)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _entityValidator.ValidateEntity(userDto3)).Returns(new EntityValidationResult(true));
            
            A.CallTo(() => groupDto.GroupId).Returns(1);
            A.CallTo(() => lessonDto.GroupId).Returns(1);
            
            A.CallTo(() => userDto1.Group.GroupId).Returns(1);
            A.CallTo(() => userDto2.Group.GroupId).Returns(1);
            A.CallTo(() => userDto3.Group.GroupId).Returns(1);

            _visitService.GetGroupVisits(1);
        }
    }
}
