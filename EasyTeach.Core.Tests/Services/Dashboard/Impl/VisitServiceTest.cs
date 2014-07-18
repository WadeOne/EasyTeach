using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Core.Validation.EntityValidator;
using EasyTeach.Core.Services.Dashboard.Impl;
using EasyTeach.Data.Entities;
using FakeItEasy;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using EasyTeach.Core.Entities;

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

            _visitService = new VisitService(_visitRepository, _lessonRepository, _userRepository, _entityValidator, _visitDtoMapper);
        }

        [Fact]
        public void GetGroupVisits_AlgorithmWorksCorrectly()
        {
            var userDto1 = new UserDto { UserId = 1, GroupId = 1 };
            var userDto2 = new UserDto { UserId = 2, GroupId = 1 };
            var userDto3 = new UserDto { UserId = 3, GroupId = 1 };
            var userDto4 = new UserDto { UserId = 3, GroupId = 2 };

            var visitDto1 = new VisitDto { UserId = 1, LessonId = 1 };
            var visitDto2 = new VisitDto { UserId = 2, LessonId = 2, Status = VisitStatus.SkippedWithExcuse };
            var visitDto3 = new VisitDto { UserId = 3, LessonId = 3 };

            var lessonDto = new LessonDto { GroupId = 1, LessonId = 1 };
            var lessonDto2 = new LessonDto { GroupId = 1, LessonId = 2 };
            var lessonDto3 = new LessonDto { GroupId = 3, LessonId = 3 };

            A.CallTo(() => _userRepository.GetUsers()).Returns((new[] { userDto1, userDto2, userDto3, userDto4 }).AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessons()).Returns((new[] { lessonDto, lessonDto2, lessonDto3 }).AsQueryable());
            A.CallTo(() => _visitRepository.GetAll()).Returns((new[] { visitDto1, visitDto2, visitDto3 }).AsQueryable());

            IQueryable<IVisitModel> vistis = _visitService.GetGroupVisits(1);

            Assert.Equal(6, vistis.Count());
            
            Assert.Contains(new Visit
            {
                Status = VisitStatus.SkippedWithExcuse,
                Visitor = new User { UserId = 2 },
                Lesson = new Lesson { LessonId = 2 }
            }, vistis, new VisitComparer());

            Assert.Contains(new Visit
            {
                Status = VisitStatus.Vistited,
                Visitor = new User { UserId = 1 },
                Lesson = new Lesson { LessonId = 2 }
            }, vistis, new VisitComparer());
        }

        private sealed class VisitComparer : IEqualityComparer<IVisitModel>
        {
            public bool Equals(IVisitModel x, IVisitModel y)
            {
                if (x.Lesson == null || x.Visitor == null || y.Lesson == null || y.Visitor == null)
                {
                    return false;
                }

                return x.Lesson.LessonId == y.Lesson.LessonId && x.Visitor.UserId == y.Visitor.UserId &&
                       x.Status == y.Status;
            }

            public int GetHashCode(IVisitModel obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
