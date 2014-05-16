using System;

namespace EasyTeach.Core.Entities.Services
{
    public interface ILessonModel
    {
        int LessonId { get; }

        DateTime Date { get; }

        IGroupModel Group { get; }
    }
}