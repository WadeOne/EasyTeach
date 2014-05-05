using System;

namespace EasyTeach.Core.Entities.Services
{
    public interface IClassModel
    {
        DateTime Date { get; }

        ITaskModel Task { get; }

        IGroupModel Group { get; }
    }
}