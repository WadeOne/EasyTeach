using System;
using EasyTeach.Core.Entities;

namespace EasyTeach.Web.Models.ViewModels.Dashboard.Lessons
{
    public class CreateLessonViewModel
    {
        public DateTime Date { get; set; }

        public int GroupId { get; set; }

        public virtual Lesson ToLesson()
        {
            return new Lesson
            {
                Date = Date,
                Group = new Group
                {
                    GroupId = GroupId
                }
            };
        }
    }
}