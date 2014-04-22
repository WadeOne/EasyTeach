using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers;

namespace EasyTeach.Data.Repostitories.Mappers
{
    public class QuizDtoMapper : IQuizDtoMapper
    {
        public IQuizDto Map(IQuizModel quiz)
        {
            throw new System.NotImplementedException();
        }

        public IQuizModel Map(IQuizDto quizDto)
        {
            throw new System.NotImplementedException();
        }

        public IAssignedTestDto Map(IAssignedTestModel assignedTest)
        {
            throw new System.NotImplementedException();
        }
    }
}
