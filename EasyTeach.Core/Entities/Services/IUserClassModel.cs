using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Services
{
    public interface IUserClassModel
    {
        IClassModel Class { get; }

        IUserModel User { get; }

        VisitStatus Visit { get; }

        string Note { get; }
    }
}