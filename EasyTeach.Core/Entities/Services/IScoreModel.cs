namespace EasyTeach.Core.Entities.Services
{
    public interface IScoreModel
    {
        int ScoreId { get; }

        int Score { get; }

        IUserModel AssignedTo { get; }

        IUserModel AssignedBy { get; }

        IVariantProgressModel Task { get; }

        IVisitModel Visit { get; }
    }
}