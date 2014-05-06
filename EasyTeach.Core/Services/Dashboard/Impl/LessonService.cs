using System;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.Dashboard.Exceptions;

namespace EasyTeach.Core.Services.Dashboard.Impl
{
    public sealed class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ILessonDtoMapper _lessonDtoMapper;

        public LessonService(ILessonRepository lessonRepository, ILessonDtoMapper lessonDtoMapper)
        {
            if (lessonRepository == null)
            {
                throw new ArgumentNullException("lessonRepository");
            }

            if (lessonDtoMapper == null)
            {
                throw new ArgumentNullException("lessonDtoMapper");
            }

            _lessonRepository = lessonRepository;
            _lessonDtoMapper = lessonDtoMapper;
        }

        public async Task CreateLessonAsync(ILessonModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            if (_lessonRepository.GetLessons().Any(l => l.Date == lesson.Date))
            {
                throw new LessonDateOverlappingException();
            }

            await _lessonRepository.CreateLessonAsync(_lessonDtoMapper.Map(lesson));
        }

        public async Task RemoveLessonAsync(int lessonId)
        {
            if (_lessonRepository.GetLessonByIdAsync(lessonId) == null)
            {
                throw new EntityNotFoundException("lesson", lessonId);
            }

            await _lessonRepository.RemoveLessonAsync(lessonId);
        }

        public async Task UpdateLessonAsync(ILessonModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            if (_lessonRepository.GetLessonByIdAsync(lesson.LessonId) == null)
            {
                throw new EntityNotFoundException("lesson", lesson.LessonId);
            }

            if (_lessonRepository.GetLessons().Any(l => l.LessonId != lesson.LessonId && l.Date == lesson.Date))
            {
                throw new LessonDateOverlappingException();
            }

            await _lessonRepository.UpdateLessonAsync(_lessonDtoMapper.Map(lesson));
        }

        public IQueryable<ILessonModel> GetLessons()
        {
            return _lessonRepository.GetLessons().Select(l => new Lesson
            {
                LessonId = l.LessonId,
                Date = l.Date
            });
        }
    }
}