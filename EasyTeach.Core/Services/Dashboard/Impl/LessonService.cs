using System;
using System.Linq;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;

namespace EasyTeach.Core.Services.Dashboard.Impl
{
    public sealed class LessonService : ILessonService
    {
        private readonly EntityValidator _entityValidator;
        private readonly ILessonRepository _lessonRepository;
        private readonly ILessonDtoMapper _lessonDtoMapper;

        public LessonService(EntityValidator entityValidator, ILessonRepository lessonRepository, ILessonDtoMapper lessonDtoMapper)
        {
            if (entityValidator == null)
            {
                throw new ArgumentNullException("entityValidator");
            }

            if (lessonRepository == null)
            {
                throw new ArgumentNullException("lessonRepository");
            }

            if (lessonDtoMapper == null)
            {
                throw new ArgumentNullException("lessonDtoMapper");
            }

            _entityValidator = entityValidator;
            _lessonRepository = lessonRepository;
            _lessonDtoMapper = lessonDtoMapper;
        }

        public void CreateLesson(ILessonModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(lesson);
            if (result.IsValid == false)
            {
                throw new InvalidLessonException(result.ValidationResults);
            }

            if (_lessonRepository.GetLessons().Any(l => l.Date == lesson.Date && l.GroupId == lesson.Group.GroupId))
            {
                throw new LessonDateOverlappingException();
            }

            _lessonRepository.CreateLesson(_lessonDtoMapper.Map(lesson));
        }

        public void RemoveLesson(int lessonId)
        {
            if (_lessonRepository.GetLessonById(lessonId) == null)
            {
                throw new EntityNotFoundException("lesson", lessonId);
            }

            _lessonRepository.RemoveLesson(lessonId);
        }

        public void UpdateLesson(ILessonModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(lesson);
            if (result.IsValid == false)
            {
                throw new InvalidLessonException(result.ValidationResults);
            }

            if (_lessonRepository.GetLessonById(lesson.LessonId) == null)
            {
                throw new EntityNotFoundException("lesson", lesson.LessonId);
            }

            if (_lessonRepository.GetLessons().Any(l => l.LessonId != lesson.LessonId && l.Date == lesson.Date && l.GroupId == lesson.Group.GroupId))
            {
                throw new LessonDateOverlappingException();
            }

            _lessonRepository.UpdateLesson(_lessonDtoMapper.Map(lesson));
        }

        public ILessonModel GetLessonById(int lessonId)
        {
            ILessonDto lesson = _lessonRepository.GetLessonById(lessonId);
            if (lesson == null)
            {
                return null;
            }

            return Map(lesson);
        }

        public IQueryable<ILessonModel> GetLessons()
        {
            return _lessonRepository.GetLessons().Select(Map).AsQueryable();
        }

        private Lesson Map(ILessonDto lesson)
        {
            return new Lesson
            {
                LessonId = lesson.LessonId,
                Date = lesson.Date,
                Group = new Group
                {
                    GroupId = lesson.GroupId
                }
            };
        }
    }
}