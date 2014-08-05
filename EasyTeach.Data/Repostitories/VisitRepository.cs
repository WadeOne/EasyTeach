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
    public sealed class VisitRepository : IVisitRepository
    {
        private readonly EasyTeachContext _context;

        public VisitRepository(EasyTeachContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public IQueryable<IVisitDto> GetAll()
        {
            return _context.Visits.Include("Lesson").Include("User");
        }

        public async Task CreateVisitAsync(IVisitDto visit)
        {
            if (visit == null)
            {
                throw new ArgumentNullException("visit");
            }

            _context.Visits.Add((VisitDto) visit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVisitAsync(IVisitDto visit)
        {
            if (visit == null)
            {
                throw new ArgumentNullException("visit");
            }

            VisitDto oldVisit = await _context.Visits.SingleOrDefaultAsync(v => v.LessonId == visit.LessonId && v.UserId == visit.UserId);
            oldVisit.Status = visit.Status;
            oldVisit.Note = visit.Note;
            
            await _context.SaveChangesAsync();
        }

        public async Task<IVisitDto> GetVisitAsync(int userId, int lessonId)
        {
            return await _context.Visits.SingleOrDefaultAsync(v => v.LessonId == lessonId && v.UserId == userId);
        }
    }
}