using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data.Dashboard;

namespace EasyTeach.Core.Repositories
{
    public interface ILessonRepository
    {
        Task CreateLessonAsync(ILessonDto lesson);

        Task RemoveLessonAsync(int lessonId);

        Task UpdateLessonAsync(ILessonDto lesson);

        IQueryable<ILessonDto> GetLessons();

        Task<ILessonDto> GetLessonByIdAsync(int lessonId);
    }
}