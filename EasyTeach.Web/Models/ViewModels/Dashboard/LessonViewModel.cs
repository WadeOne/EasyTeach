using System;

namespace EasyTeach.Web.Models.ViewModels.Dashboard
{
    public sealed class LessonViewModel
    {
        public int LessonId { get; set; }

        public int GroupId { get; set; }

        public DateTime Date { get; set; }
    }
}