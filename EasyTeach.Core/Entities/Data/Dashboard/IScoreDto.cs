using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities.Data.Dashboard
{
    public interface IScoreDto
    {
        int ScoreId { get; }

        int Score { get; }

        IUserDto AssignedTo { get; }

        IUserDto AssignedBy { get; }

        IVariantProgressModel Task { get; }

        IVisitDto Visit { get; }
    }
}
