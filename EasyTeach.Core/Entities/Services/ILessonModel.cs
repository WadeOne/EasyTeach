using System;

namespace EasyTeach.Core.Entities.Services
{
    public interface ILessonModel
    {
        DateTime Date { get; }

        IGroupModel Group { get; }
    }
}