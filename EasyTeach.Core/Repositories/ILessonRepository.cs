using System.Linq;
using EasyTeach.Core.Entities.Data.Dashboard;

namespace EasyTeach.Core.Repositories
{
    public interface ILessonRepository
    {
        void CreateLesson(ILessonDto lesson);

        void RemoveLesson(int lessonId);

        void UpdateLesson(ILessonDto lesson);

        IQueryable<ILessonDto> GetLessons();

        ILessonDto GetLessonById(int lessonId);
    }
}