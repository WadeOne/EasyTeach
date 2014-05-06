using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.Dashboard
{
    public interface ILessonService
    {
        Task CreateLessonAsync(ILessonModel lesson);

        Task RemoveLessonAsync(int lessonId);

        Task UpdateLessonAsync(ILessonModel lesson);

        IQueryable<ILessonModel> GetLessons();
    }
}