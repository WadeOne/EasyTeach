using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Services
{
    public interface IVisitModel
    {
        int VisitId { get; }

        ILessonModel Lesson { get; }

        IUserModel Visitor { get; }

        VisitStatus Status { get; }

        string Note { get; }
    }
}