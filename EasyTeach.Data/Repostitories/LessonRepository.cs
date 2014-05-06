using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateLessonAsync(ILessonDto lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            _context.Lessons.Add((LessonDto)lesson);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveLessonAsync(int lessonId)
        {
            LessonDto lesson = await _context.Lessons.SingleAsync(l => l.LessonId == lessonId);
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLessonAsync(ILessonDto lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            LessonDto oldLesson = await _context.Lessons.SingleAsync(l => l.LessonId == lesson.LessonId);
            oldLesson.Date = lesson.Date;
            await _context.SaveChangesAsync();
        }

        public async Task<ILessonDto> GetLessonByIdAsync(int lessonId)
        {
            return await _context.Lessons.SingleOrDefaultAsync(l => l.LessonId == lessonId);
        }

        public IQueryable<ILessonDto> GetLessons()
        {
            return _context.Lessons;
        }
    }
}