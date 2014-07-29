using System;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Web.Models.ViewModels.Dashboard.Lessons
{
    public class LessonViewModel
    {
        [Key]
        public int LessonId { get; set; }

        public int GroupId { get; set; }

        public DateTime Date { get; set; }

        public virtual ILessonModel ToLesson()
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