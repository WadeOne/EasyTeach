using System;
using EasyTeach.Core.Entities;

namespace EasyTeach.Web.Models.ViewModels.Dashboard.Lessons
{
    public class UpdateLessonViewModel
    {
        public int LessonId { get; set; }

        public int GroupId { get; set; }

        public DateTime Date { get; set; }

        public virtual Lesson ToLesson()
        {
            return new Lesson
            {
                LessonId = LessonId,
                Date = Date,
                Group = new Group
                {
                    GroupId = GroupId
                }
            };
        }

    }
}