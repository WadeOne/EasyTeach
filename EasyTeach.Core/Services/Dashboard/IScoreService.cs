using System.Linq;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.Dashboard
{
    public interface IScoreService
    {
        void AddScore(IScoreModel score);

        void DeleteScore(int scoreId);

        void UpdateScore(IScoreModel score);

        IQueryable<IScoreModel> GetScores();
    }
}
