using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Data.Dashboard
{
    public interface IVisitDto
    {
        int VisitId { get; }

        int LessonId { get; }

        ILessonDto Lesson { get; }

        VisitStatus Status { get; }

        string Note { get; }

        int UserId { get; }
    }
}