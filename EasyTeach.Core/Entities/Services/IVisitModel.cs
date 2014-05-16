using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Services
{
    public interface IVisitModel
    {
        ILessonModel Lesson { get; }

        IUserModel Visitor { get; }

        VisitStatus Status { get; }

        string Note { get; }
    }
}