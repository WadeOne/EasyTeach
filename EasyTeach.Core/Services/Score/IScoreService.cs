using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.Score
{
    public interface IScoreService
    {
        void AddScore(IScoreModel score);

        void DeleteScore(int scoreId);

        void UpdateScore(int scoreId);

        IQueryable<IScoreModel> GetGroupScore(int groupId);
    }
}
