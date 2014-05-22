using System.Linq;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.Dashboard
{
    public interface ILessonService
    {
        void CreateLesson(ILessonModel lesson);

        void RemoveLesson(int lessonId);

        void UpdateLesson(ILessonModel lesson);

        ILessonModel GetLessonById(int lessonId);

        IQueryable<ILessonModel> GetLessons();
    }
}