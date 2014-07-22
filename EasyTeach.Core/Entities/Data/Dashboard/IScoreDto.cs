using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities.Data.Dashboard
{
    public interface IScoreDto
    {
        int ScoreId { get; }

        int Score { get; }

        IUserDto AssignedTo { get; }

        int AssignedToId { get; }

        IUserDto AssignedBy { get; }

        int AssignedById { get; }

        IVariantProgressModel Task { get; }

        int? VisitId { get; }

        IVisitDto Visit { get; }
    }
}
