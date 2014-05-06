using System;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities
{
    public sealed class Lesson : ILessonModel
    {
        public int LessonId { get; set; }

        public DateTime Date { get; set; }

        public IGroupModel Group { get; set; }
    }
}