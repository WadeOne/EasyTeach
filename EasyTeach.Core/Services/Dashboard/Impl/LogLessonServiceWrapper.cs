using System;
using System.Linq;
using System.Security;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using NLog;

namespace EasyTeach.Core.Services.Dashboard.Impl
{
    public sealed class LogLessonServiceWrapper : ILessonService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ILessonService _lessonService;

        public LogLessonServiceWrapper(ILessonService lessonService)
        {
            if (lessonService == null)
            {
                throw new ArgumentNullException("lessonService");
            }

            _lessonService = lessonService;
        }

        public void CreateLesson(ILessonModel lesson)
        {
            try
            {
                _lessonService.CreateLesson(lesson);
                _logger.Debug("User created lesson");
            }
            catch (ArgumentNullException)
            {
                _logger.Info("Lesson is null");
            }
            catch (ModelValidationException)
            {
                _logger.Info("Model of lesson is not valid");
            }
            catch (SecurityException)
            {
                _logger.Info("User doesn't have enough permission for created lesson.");
            }
        }

        public void RemoveLesson(int lessonId)
        {
            try
            {
                _lessonService.RemoveLesson(lessonId);
                _logger.Debug("User removed lesson");
            }
            catch (ArgumentNullException)
            {
                _logger.Info("Lesson is null");
            }
            catch (SecurityException)
            {
                _logger.Info("User doesn't have enough permission for removing lesson");
            }
            catch (EntityNotFoundException)
            {
                _logger.Info("Lesson not found");
            }
        }

        public void UpdateLesson(ILessonModel lesson)
        {
            try
            {
                _lessonService.UpdateLesson(lesson);
                _logger.Debug("User updated lesson");
            }
            catch (ArgumentNullException)
            {
                _logger.Info("Lesson is null");
            }
            catch (ModelValidationException)
            {
                _logger.Info("Model of lesson is not valid");
            }
            catch (SecurityException)
            {
                _logger.Info("User doesn't have enough permission for updated lesson.");
            }
            catch (EntityNotFoundException)
            {
                _logger.Info("Lesson not found");
            }
            catch (LessonDateOverlappingException)
            {
                _logger.Info("Lesson date overlapping");
            }
        }

        public ILessonModel GetLessonById(int lessonId)
        {
            try
            {
                ILessonModel lesson = _lessonService.GetLessonById(lessonId);
                _logger.Debug("User received a lesson by id.");

                return lesson;
            }
            catch (ArgumentNullException)
            {
                _logger.Info("Lesson is null");
                throw new ArgumentNullException();
            }
            catch (SecurityException)
            {
                _logger.Info("User doesn't have enough permission for retrieving lesson.");
                throw new SecurityException();
            }
        }

        public IQueryable<ILessonModel> GetLessons()
        {
            try
            {
                var result = _lessonService.GetLessons();
                _logger.Debug("User received a list of lessons.");

                return result;
            }
            catch (SecurityException)
            {
                _logger.Info("User doesn't have enough permission for retrieving lessons.");
                throw new SecurityException();
            }
            catch (Exception)
            {
                _logger.Error("User has not received a list of lessons.");
                throw new Exception();
            }
        }
    }
}
