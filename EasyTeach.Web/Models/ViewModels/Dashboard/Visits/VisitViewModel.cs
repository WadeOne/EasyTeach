using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;

namespace EasyTeach.Web.Models.ViewModels.Dashboard.Visits
{
    public class VisitViewModel
    {
        public VisitStatus Status { get; set; }

        public int VisitorId { get; set; }

        public int LessonId { get; set; }

        public string Note { get; set; }

        public virtual IVisitModel ToVisit()
        {
            return new Visit
            {
                Lesson = new Lesson
                {
                    LessonId = LessonId
                },
                Visitor = new User
                {
                    UserId = VisitorId
                },
                Status = Status,
                Note = Note
            };
        }
    }
}