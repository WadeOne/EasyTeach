using System.Collections.Generic;
using System.Linq;

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
            MappingConfiguration.Configure();
        }

        public IQuizDto Map(IQuizModel quiz)
        {
            return Mapper.Map<QuizDto>(quiz);
        }

        public IQuizModel Map(IQuizDto quizDto)
        {
            var result = Mapper.Map<QuizModel>(quizDto);
            return result;
        }

        public IAssignedQuizDto Map(IAssignedTestModel assignedTest)
        {
            throw new System.NotImplementedException();
        }

        
    }
}
