using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Core.Services.Score;
using EasyTeach.Core.Validation.EntityValidator;

namespace EasyTeach.Core.Services.Score.Impl
{
    public sealed class ScoreService : IScoreService
    {
        private readonly EntityValidator _entityValidator;
        private readonly IScoreRepository _scoreRepository;
        private readonly IScoreDtoMapper _scoreDtoMapper;

        public ScoreService(EntityValidator entityValidator, IScoreRepository scoreRepository, IScoreDtoMapper scoreDtoMapper)
        {
            if (entityValidator == null)
            {
                throw new ArgumentNullException("entityValidator");
            }

            if (scoreRepository == null)
            {
                throw new ArgumentNullException("scoreRepository");
            }

            if (scoreDtoMapper == null)
            {
                throw new ArgumentNullException("scoreDtoMapper");
            }

            _entityValidator = entityValidator;
            _scoreRepository = scoreRepository;
            _scoreDtoMapper = scoreDtoMapper;
        }

        public void AddScore(IScoreModel score)
        {
            if (score == null)
            {
                throw new ArgumentNullException("score");
            }

        }

        public void DeleteScore(int scoreId)
        {
            throw new NotImplementedException();
        }

        public void UpdateScore(int scoreId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IScoreModel> GetGroupScore(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
