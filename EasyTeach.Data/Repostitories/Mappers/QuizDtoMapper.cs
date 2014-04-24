using AutoMapper;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers
{
    public class QuizDtoMapper : IQuizDtoMapper
    {
        public QuizDtoMapper()
        {
            Mapper.CreateMap<IQuizDto, QuizModel>();
            Mapper.CreateMap<IQuizModel, QuizDto>();
        }

        public IQuizDto Map(IQuizModel quiz)
        {
            return Mapper.Map<QuizDto>(quiz);
        }

        public IQuizModel Map(IQuizDto quizDto)
        {
            return Mapper.Map<QuizModel>(quizDto);
        }

        public IAssignedTestDto Map(IAssignedTestModel assignedTest)
        {
            throw new System.NotImplementedException();
        }
    }
}
