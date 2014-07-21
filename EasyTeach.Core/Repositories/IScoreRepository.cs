using System.Linq;
using EasyTeach.Core.Entities.Data.Dashboard;

namespace EasyTeach.Core.Repositories
{
    public interface IScoreRepository
    {
        void AddScore(IScoreDto score);

        void DeleteScore(int scoreId);

        void UpdateScore(IScoreDto score);

        IQueryable<IScoreDto> GetScores();
    }
}
