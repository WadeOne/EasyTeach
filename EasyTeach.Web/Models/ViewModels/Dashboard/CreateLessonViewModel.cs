
using System;
using EasyTeach.Core.Entities;

namespace EasyTeach.Web.Models.ViewModels.Dashboard
{
    public class CreateLessonViewModel
    {
         public DateTime Date { get; set; }

         public virtual Lesson ToLesson()
         {
             return new Lesson
             {
                 Date = Date
             };
         }
    }
}