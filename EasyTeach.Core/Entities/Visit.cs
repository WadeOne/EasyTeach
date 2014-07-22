using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities
{
    public sealed class Visit : IVisitModel
    {
        public int VisitId { get; set; }

        [Required]
        public ILessonModel Lesson { get; set; }

        [Required]
        public IUserModel Visitor { get; set; }

        public VisitStatus Status { get; set; }

        public string Note { get; set; }
    }
}