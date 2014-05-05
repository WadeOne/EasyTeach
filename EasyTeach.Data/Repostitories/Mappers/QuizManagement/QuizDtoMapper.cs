using AutoMapper;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers.QuizManagement;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers.QuizManagement
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

        public IAssignedQuizDto Map(IAssignedTestModel assignedTest)
        {
            throw new System.NotImplementedException();
        }
    }
}
