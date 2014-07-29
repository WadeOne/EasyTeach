using System;
using System.Data.Entity;
using System.Linq;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Repositories;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories
{
    public sealed class LessonRepository : ILessonRepository
    {
        private readonly EasyTeachContext _context;

        public LessonRepository(EasyTeachContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public void CreateLesson(ILessonDto lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            _context.Lessons.Add((LessonDto)lesson);
            _context.SaveChanges();
        }

        public void RemoveLesson(int lessonId)
        {
            LessonDto lesson = _context.Lessons.Single(l => l.LessonId == lessonId);
            _context.Lessons.Remove(lesson);
            _context.SaveChanges();
        }

        public void UpdateLesson(ILessonDto lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            LessonDto oldLesson = _context.Lessons.Single(l => l.LessonId == lesson.LessonId);
            oldLesson.Date = lesson.Date;
            oldLesson.GroupId = lesson.GroupId;
            _context.SaveChanges();
        }

        public ILessonDto GetLessonById(int lessonId)
        {
            return _context.Lessons.SingleOrDefault(l => l.LessonId == lessonId);
        }

        public IQueryable<ILessonDto> GetLessons()
        {
            return _context.Lessons;
        }
    }
}