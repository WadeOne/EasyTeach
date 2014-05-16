using System;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using EasyTeach.Core.Services.Dashboard.Impl;
using EasyTeach.Core.Validation.EntityValidator;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Core.Tests.Services.Dashboard.Impl
{
    public sealed class LessonServiceTest
    {
        private readonly LessonService _lessonService;
        private readonly EntityValidator _entityValidator;
        private readonly ILessonRepository _lessonRepository;
        private readonly ILessonDtoMapper _lessonDtoMapper;

        public LessonServiceTest()
        {
            _entityValidator = A.Fake<EntityValidator>();
            _lessonRepository = A.Fake<ILessonRepository>();
            _lessonDtoMapper = A.Fake<ILessonDtoMapper>();

            _lessonService = new LessonService(
                _entityValidator,
                _lessonRepository,
                _lessonDtoMapper);
        }

        [Fact]
        public void CreateLessonAsync_ValidNonOverlappingModel_Created()
        {
            ILessonModel lesson = new Lesson();
            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(Enumerable.Empty<ILessonDto>().AsQueryable());

            _lessonService.CreateLessonAsync(lesson).Wait();

            A.CallTo(() => _lessonRepository.CreateLessonAsync(A<ILessonDto>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void CreateLessonAsync_ValidOverlappingModel_ThrowsOverlappingException()
        {
            var group = A.Fake<IGroupModel>();
            A.CallTo(() => group.GroupId).Returns(2);

            ILessonModel lesson = new Lesson
            {
                Date = new DateTime(2014, 1, 1),
                Group = group
            };

            var lessonDto = A.Fake<ILessonDto>();
            A.CallTo(() => lessonDto.GroupId).Returns(2);
            A.CallTo(() => lessonDto.Date).Returns(new DateTime(2014, 1, 1));

            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(new[] { lessonDto }.AsQueryable());

            var aggregateException = Assert.Throws<AggregateException>(() => _lessonService.CreateLessonAsync(lesson).Wait());

            Assert.IsAssignableFrom<LessonDateOverlappingException>(aggregateException.GetBaseException());
            A.CallTo(() => _lessonRepository.CreateLessonAsync(A<ILessonDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void CreateLessonAsync_InvalidModel_ThrowsInvalidLessonException()
        {
            ILessonModel lesson = new Lesson();
            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(false));

            var aggregateException = Assert.Throws<AggregateException>(() => _lessonService.CreateLessonAsync(lesson).Wait());

            Assert.IsAssignableFrom<InvalidLessonException>(aggregateException.GetBaseException());
            A.CallTo(() => _lessonRepository.CreateLessonAsync(A<ILessonDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void UpdateLessonAsync_ExistingAndValidNonOverlappingModel_Updated()
        {
            ILessonModel lesson = new Lesson
            {
                LessonId = 42
            };

            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(Enumerable.Empty<ILessonDto>().AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessonByIdAsync(42)).Returns(A.Dummy<ILessonDto>());

            _lessonService.UpdateLessonAsync(lesson).Wait();

            A.CallTo(() => _lessonRepository.UpdateLessonAsync(A<ILessonDto>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void UpdateLessonAsync_InvalidModel_ThrowsInvalidLessonException()
        {
            ILessonModel lesson = new Lesson();
            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(false));

            var aggregateException = Assert.Throws<AggregateException>(() => _lessonService.UpdateLessonAsync(lesson).Wait());

            Assert.IsAssignableFrom<InvalidLessonException>(aggregateException.GetBaseException());
            A.CallTo(() => _lessonRepository.UpdateLessonAsync(A<ILessonDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void UpdateLessonAsync_NotExistingModel_ThrowEntityNotFoundException()
        {
            ILessonModel lesson = new Lesson
            {
                LessonId = 42
            };

            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(Enumerable.Empty<ILessonDto>().AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessonByIdAsync(42)).Returns(Task.FromResult((ILessonDto)null));

            var aggregateException = Assert.Throws<AggregateException>(() => _lessonService.UpdateLessonAsync(lesson).Wait());

            Assert.IsAssignableFrom<EntityNotFoundException>(aggregateException.GetBaseException());
            A.CallTo(() => _lessonRepository.UpdateLessonAsync(A<ILessonDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void UpdateLessonAsync_ValidOverlappingModel_ThrowsOverlappingException()
        {
            var group = A.Fake<IGroupModel>();
            A.CallTo(() => group.GroupId).Returns(2);

            ILessonModel lesson = new Lesson
            {
                LessonId = 42,
                Date = new DateTime(2014, 1, 1),
                Group = group
            };

            var lessonDto = A.Fake<ILessonDto>();
            A.CallTo(() => lessonDto.GroupId).Returns(2);
            A.CallTo(() => lessonDto.LessonId).Returns(1);
            A.CallTo(() => lessonDto.Date).Returns(new DateTime(2014, 1, 1));

            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(new[] { lessonDto }.AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessonByIdAsync(42)).Returns(A.Dummy<ILessonDto>());

            var aggregateException = Assert.Throws<AggregateException>(() => _lessonService.UpdateLessonAsync(lesson).Wait());

            Assert.IsAssignableFrom<LessonDateOverlappingException>(aggregateException.GetBaseException());
            A.CallTo(() => _lessonRepository.UpdateLessonAsync(A<ILessonDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void RemoveLessonAsync_NotExistingId_ThrowEntityNotFoundException()
        {
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(Enumerable.Empty<ILessonDto>().AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessonByIdAsync(42)).Returns(Task.FromResult((ILessonDto)null));

            var aggregateException = Assert.Throws<AggregateException>(() => _lessonService.RemoveLessonAsync(42).Wait());

            Assert.IsAssignableFrom<EntityNotFoundException>(aggregateException.GetBaseException());
            A.CallTo(() => _lessonRepository.RemoveLessonAsync(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void RemoveLessonAsync_ExistingId_Removed()
        {
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(Enumerable.Empty<ILessonDto>().AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessonByIdAsync(42)).Returns(Task.FromResult(A.Dummy<ILessonDto>()));

            _lessonService.RemoveLessonAsync(42).Wait();

            A.CallTo(() => _lessonRepository.RemoveLessonAsync(42)).MustHaveHappened();
        }

        [Fact]
        public void GetLessonByIdAsync_ExistingId_Lesson()
        {
            var lessonDto = A.Fake<ILessonDto>();
            A.CallTo(() => lessonDto.LessonId).Returns(42);
            A.CallTo(() => lessonDto.GroupId).Returns(11);
            A.CallTo(() => lessonDto.Date).Returns(new DateTime(2014, 5, 5));
            A.CallTo(() => _lessonRepository.GetLessonByIdAsync(42)).Returns(Task.FromResult(lessonDto));

            ILessonModel lesson = _lessonService.GetLessonByIdAsync(42).Result;

            Assert.Equal(42, lesson.LessonId);
            Assert.Equal(new DateTime(2014, 5, 5), lesson.Date);
            Assert.Equal(11, lesson.Group.GroupId);
        }

        [Fact]
        public void GetLessonByIdAsync_NotExistingId_Null()
        {
            A.CallTo(() => _lessonRepository.GetLessonByIdAsync(42)).Returns(Task.FromResult<ILessonDto>(null));

            ILessonModel lesson = _lessonService.GetLessonByIdAsync(42).Result;

            Assert.Null(lesson);
        }
    }
}