using System;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Data.Dashboard;

namespace EasyTeach.Data.Entities
{
    public class LessonDto : ILessonDto
    {
        [Key]
        public int LessonId { get; set; }

        public DateTime Date { get; set; }
    }
}