using System;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Core.Repositories.Mappers.UserManagement;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers.Dashboard
{
    public sealed class ScoreDtoMapper : IScoreDtoMapper
    {
        private readonly IUserDtoMapper _userDtoMapper;
        private readonly IVisitDtoMapper _visitDtoMapper;

        public ScoreDtoMapper(IUserDtoMapper userDtoMapper, IVisitDtoMapper visitDtoMapper)
        {
            _userDtoMapper = userDtoMapper;
            _visitDtoMapper = visitDtoMapper;
        }

        public IScoreDto Map(IScoreModel score)
        {
            if (score == null)
            {
                throw new ArgumentNullException("score");
            }

            return new ScoreDto
            {
                ScoreId = score.ScoreId,
                Score = score.Score,
                AssignedTo = (UserDto)_userDtoMapper.Map(score.AssignedTo),
                AssignedBy = (UserDto)_userDtoMapper.Map(score.AssignedBy),
                Task = score.Task,
                Visit = score.Visit == null ? null : (VisitDto)_visitDtoMapper.Map(score.Visit)
            };
        }
    }
}
