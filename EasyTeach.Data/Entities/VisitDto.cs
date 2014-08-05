using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Enums;

namespace EasyTeach.Data.Entities
{
    public sealed class VisitDto : IVisitDto
    {
        [Key]
        public int VisitId { get; set; }

        [ForeignKey("Lesson")]
        public int LessonId { get; set; }

        ILessonDto IVisitDto.Lesson
        {
            get { return Lesson; }
        }

        public LessonDto Lesson { get; set; }

        public VisitStatus Status { get; set; }

        public string Note { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public UserDto User { get; set; }
    }
}