using System;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities
{
    public sealed class Lesson : ILessonModel
    {
        public int LessonId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public IGroupModel Group { get; set; }
    }
}