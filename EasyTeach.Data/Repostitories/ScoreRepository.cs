using System;
using System.Data.Entity;
using System.Linq;
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
                throw new ArgumentNullException("score");
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
            if (score == null)
            {
                throw new ArgumentNullException("score");
            }

            ScoreDto oldScore = _context.Scores.Single(s => s.ScoreId == score.ScoreId);
            
            oldScore.Score = score.Score;
            oldScore.Task = score.Task;
            oldScore.AssignedToId = score.AssignedTo.Id;
            oldScore.AssignedById = score.AssignedBy.Id;
            oldScore.Visit.VisitId = score.Visit.VisitId;
            _context.SaveChanges();
        }

        public IQueryable<IScoreDto> GetScores()
        {
            return _context.Scores.Include("AssignedTo").Include("AssignedBy").Include("Visit");
        }
    }
}