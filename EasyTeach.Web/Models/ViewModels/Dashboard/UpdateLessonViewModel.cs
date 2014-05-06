using System;
using EasyTeach.Core.Entities;

namespace EasyTeach.Web.Models.ViewModels.Dashboard
{
    public class UpdateLessonViewModel
    {
        public int LessonId { get; set; }

        public DateTime Date { get; set; }

        public virtual Lesson ToLesson()
        {
            return new Lesson
            {
                LessonId = LessonId,
                Date = Date
            };
        }

    }
}