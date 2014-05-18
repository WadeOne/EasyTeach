using AutoMapper;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers.QuizManagement;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers.QuizManagement
{
    public class AssignedQuizDtoMapper : IAssignedQuizDtoMapper
    {
        public AssignedQuizDtoMapper()
        {
            MappingConfiguration.Configure();
        }

        public IAssignedQuizDto Map(IAssignedQuizModel model)
        {
            return Mapper.Map<AssignedQuizDto>(model);
        }

        public IAssignedQuizModel Map(IAssignedQuizDto dto)
        {
            return Mapper.Map<AssignedQuiz>(dto);
        }
    }
}
