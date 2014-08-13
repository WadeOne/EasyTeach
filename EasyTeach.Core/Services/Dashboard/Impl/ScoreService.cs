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
        private readonly IVisitRepository _visitRepository;
        private readonly ILessonRepository _lessonRepository;

        public ScoreService(EntityValidator entityValidator, IScoreRepository scoreRepository, IScoreDtoMapper scoreDtoMapper, IVisitRepository visitRepository, ILessonRepository lessonRepository)
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

            if (visitRepository == null)
            {
                throw new ArgumentNullException("visitRepository");
            }

            if (lessonRepository == null)
            {
                throw new ArgumentNullException("lessonRepository");
            }

            _entityValidator = entityValidator;
            _scoreRepository = scoreRepository;
            _scoreDtoMapper = scoreDtoMapper;
            _visitRepository = visitRepository;
            _lessonRepository = lessonRepository;
        }

        public void AddScore(IScoreModel score, int lessonId)
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

            if (score.Visit == null)
            {
                var lesson = _lessonRepository.GetLessonById(lessonId);
                ((ScoreModel)score).Visit = new Visit
                {
                    Visitor = new User
                    {
                        UserId = score.AssignedTo.UserId
                    }
                };
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
                Score = score.Score,
                AssignedTo = new User
                {
                    UserId = score.AssignedToId,
                    FirstName = score.AssignedTo.FirstName,
                    LastName = score.AssignedTo.LastName
                },
                AssignedBy = new User
                {
                    UserId = score.AssignedById,
                    FirstName = score.AssignedBy.FirstName,
                    LastName = score.AssignedBy.LastName
                },
                Visit = score.VisitId.HasValue ? new Visit 
                {
                    VisitId = score.VisitId.Value,
                    Lesson = new Lesson
                    {
                        LessonId = score.Visit.LessonId,
                        Date = score.Visit.Lesson.Date
                    },
                    Visitor = new User
                    {
                        UserId = score.AssignedToId
                    }
                } : null
            };
        }
    }
}
