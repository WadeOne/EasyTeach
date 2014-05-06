using System;

namespace EasyTeach.Core.Entities.Data.Dashboard
{
    public interface ILessonDto
    {
        int LessonId { get; set; }

        DateTime Date { get; set; }
    }
}