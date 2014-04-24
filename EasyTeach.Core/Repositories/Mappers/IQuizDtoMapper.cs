using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories.Mappers
{
    public interface IQuizDtoMapper
    {
        IQuizDto Map(IQuizModel quiz);

        IQuizModel Map(IQuizDto quizDto);

        IAssignedTestDto Map(IAssignedTestModel assignedTest);
    }
}