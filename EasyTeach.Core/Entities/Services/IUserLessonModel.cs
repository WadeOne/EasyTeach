using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Services
{
    public interface IUserLessonModel
    {
        ILessonModel Lesson { get; }

        IUserModel User { get; }

        VisitStatus Visit { get; }

        string Note { get; }
    }
}