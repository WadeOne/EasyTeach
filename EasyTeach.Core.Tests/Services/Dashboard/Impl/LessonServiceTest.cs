using System;
using System.Linq;
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
        public void CreateLesson_ValidNonOverlappingModel_Created()
        {
            ILessonModel lesson = new Lesson();
            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(Enumerable.Empty<ILessonDto>().AsQueryable());

            _lessonService.CreateLesson(lesson);

            A.CallTo(() => _lessonRepository.CreateLesson(A<ILessonDto>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void CreateLesson_ValidOverlappingModel_ThrowsOverlappingException()
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

            Assert.Throws<LessonDateOverlappingException>(() => _lessonService.CreateLesson(lesson));

            A.CallTo(() => _lessonRepository.CreateLesson(A<ILessonDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void CreateLesson_InvalidModel_ThrowsInvalidLessonException()
        {
            ILessonModel lesson = new Lesson();
            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(false));
            Assert.Throws<InvalidLessonException>(() => _lessonService.CreateLesson(lesson));
            A.CallTo(() => _lessonRepository.CreateLesson(A<ILessonDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void UpdateLesson_ExistingAndValidNonOverlappingModel_Updated()
        {
            ILessonModel lesson = new Lesson
            {
                LessonId = 42
            };

            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(Enumerable.Empty<ILessonDto>().AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessonById(42)).Returns(A.Dummy<ILessonDto>());

            _lessonService.UpdateLesson(lesson);

            A.CallTo(() => _lessonRepository.UpdateLesson(A<ILessonDto>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void UpdateLesson_InvalidModel_ThrowsInvalidLessonException()
        {
            ILessonModel lesson = new Lesson();
            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(false));

            Assert.Throws<InvalidLessonException>(() => _lessonService.UpdateLesson(lesson));

            A.CallTo(() => _lessonRepository.UpdateLesson(A<ILessonDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void UpdateLesson_NotExistingModel_ThrowEntityNotFoundException()
        {
            ILessonModel lesson = new Lesson
            {
                LessonId = 42
            };

            A.CallTo(() => _entityValidator.ValidateEntity(lesson)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(Enumerable.Empty<ILessonDto>().AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessonById(42)).Returns(null);

            Assert.Throws<EntityNotFoundException>(() => _lessonService.UpdateLesson(lesson));

            A.CallTo(() => _lessonRepository.UpdateLesson(A<ILessonDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void UpdateLesson_ValidOverlappingModel_ThrowsOverlappingException()
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
            A.CallTo(() => _lessonRepository.GetLessonById(42)).Returns(A.Dummy<ILessonDto>());

            Assert.Throws<LessonDateOverlappingException>(() => _lessonService.UpdateLesson(lesson));

            A.CallTo(() => _lessonRepository.UpdateLesson(A<ILessonDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void RemoveLesson_NotExistingId_ThrowEntityNotFoundException()
        {
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(Enumerable.Empty<ILessonDto>().AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessonById(42)).Returns(null);

            Assert.Throws<EntityNotFoundException>(() => _lessonService.RemoveLesson(42));

            A.CallTo(() => _lessonRepository.RemoveLesson(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void RemoveLesson_ExistingId_Removed()
        {
            A.CallTo(() => _lessonRepository.GetLessons()).Returns(Enumerable.Empty<ILessonDto>().AsQueryable());
            A.CallTo(() => _lessonRepository.GetLessonById(42)).Returns(A.Dummy<ILessonDto>());

            _lessonService.RemoveLesson(42);

            A.CallTo(() => _lessonRepository.RemoveLesson(42)).MustHaveHappened();
        }

        [Fact]
        public void GetLessonById_ExistingId_Lesson()
        {
            var lessonDto = A.Fake<ILessonDto>();
            A.CallTo(() => lessonDto.LessonId).Returns(42);
            A.CallTo(() => lessonDto.GroupId).Returns(11);
            A.CallTo(() => lessonDto.Date).Returns(new DateTime(2014, 5, 5));
            A.CallTo(() => _lessonRepository.GetLessonById(42)).Returns(lessonDto);

            ILessonModel lesson = _lessonService.GetLessonById(42);

            Assert.Equal(42, lesson.LessonId);
            Assert.Equal(new DateTime(2014, 5, 5), lesson.Date);
            Assert.Equal(11, lesson.Group.GroupId);
        }

        [Fact]
        public void GetLessonById_NotExistingId_Null()
        {
            A.CallTo(() => _lessonRepository.GetLessonById(42)).Returns(null);

            ILessonModel lesson = _lessonService.GetLessonById(42);

            Assert.Null(lesson);
        }
    }
}