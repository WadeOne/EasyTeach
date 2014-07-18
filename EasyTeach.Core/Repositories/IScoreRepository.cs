using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data.Dashboard;

namespace EasyTeach.Core.Repositories
{
    public interface IScoreRepository
    {
        void AddScore(IScoreDto score);

        void DeleteScore(int scoreId);

        void UpdateScore(int scoreId);

        IQueryable<IScoreDto> GetGroupScore(int groupId);
    }
}
