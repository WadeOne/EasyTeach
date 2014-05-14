using AutoMapper;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers.QuizManagement;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers.QuizManagement
{
    public class QuestionDtoMapper : IQuestionDtoMapper
    {
        public QuestionDtoMapper()
        {
            MappingConfiguration.Configure();
        }

        public IQuestionDto Map(IQuestionModel model)
        {
            return Mapper.Map<QuestionDto>(model);
        }

        public IQuestionModel Map(IQuestionDto dto)
        {
            return Mapper.Map<QuestionModel>(dto);
        }
    }
}
