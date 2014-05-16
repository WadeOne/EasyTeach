using System;
using EasyTeach.Core.Entities.Data.Group;

namespace EasyTeach.Core.Entities.Data.Dashboard
{
    public interface ILessonDto
    {
        int LessonId { get; }

        DateTime Date { get; }

        int GroupId { get; }

        IGroupDto Group { get; }
    }
}