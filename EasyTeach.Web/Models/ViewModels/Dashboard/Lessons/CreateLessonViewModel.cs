using System;
using EasyTeach.Core.Entities;

namespace EasyTeach.Web.Models.ViewModels.Dashboard.Lessons
{
    public class CreateLessonViewModel
    {
        public DateTime Date { get; set; }

        public int GroupId { get; set; }

        public virtual Core.Entities.Lesson ToLesson()
        {
            return new Core.Entities.Lesson
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