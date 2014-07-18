using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Repositories;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories
{
    public sealed class ScoreRepository : IScoreRepository
    {
        private readonly EasyTeachContext _context;

        public ScoreRepository(EasyTeachContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            _context = context;
        }

        public void AddScore(IScoreDto score)
        {
            if (score == null)
            {
                throw new ArgumentNullException("group");
            }

            _context.Scores.Add((ScoreDto)score);
            _context.SaveChanges();
        }

        public void DeleteScore(int scoreId)
        {
            ScoreDto score = _context.Scores.Single(s => s.ScoreId == scoreId);
            _context.Scores.Remove(score);
            _context.SaveChanges();
        }

        public void UpdateScore(IScoreDto score)
        {
            ScoreDto oldScore = _context.Scores.Single(s => s.ScoreId == score.ScoreId);
            /*
            oldScore.Score = score.Score;
            oldScore.Date = lesson.Date;
            oldScore.GroupId = lesson.GroupId;
             * */
            _context.SaveChanges();
        }

        public IQueryable<IScoreDto> GetGroupScore(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
