using System;
using System.Linq;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;

namespace EasyTeach.Core.Services.Dashboard.Impl
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

            EntityValidationResult result = _entityValidator.ValidateEntity(score);
            if (result.IsValid == false)
            {
                throw new InvalidScoreException(result.ValidationResults);
            }

            _scoreRepository.AddScore(_scoreDtoMapper.Map(score));
        }

        public void DeleteScore(int scoreId)
        {
            if (!_scoreRepository.GetScores().Any(s => s.ScoreId == scoreId))
            {
                throw new EntityNotFoundException("score", scoreId);
            }

            _scoreRepository.DeleteScore(scoreId);
        }

        public void UpdateScore(IScoreModel score)
        {
            if (score == null)
            {
                throw new ArgumentNullException("score");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(score);
            if (result.IsValid == false)
            {
                throw new InvalidScoreException(result.ValidationResults);
            }

            if (_scoreRepository.GetScores().Any(s => s.ScoreId == score.ScoreId))
            {
                throw new EntityNotFoundException("score", score.ScoreId);
            }

            _scoreRepository.UpdateScore(_scoreDtoMapper.Map(score));
        }

        public IQueryable<IScoreModel> GetScores()
        {
            return _scoreRepository.GetScores().Select(Map).AsQueryable();
        }

        private ScoreModel Map(IScoreDto score)
        {
            return new ScoreModel
            {
                ScoreId = score.ScoreId,
                Score = score.ScoreId,
                AssignedTo = new User{UserId = score.AssignedTo.Id },
                AssignedBy = new User{UserId = score.AssignedBy.Id },
            };
        }
    }
}
