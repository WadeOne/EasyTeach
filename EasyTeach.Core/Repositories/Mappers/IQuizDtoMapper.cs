using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories.Mappers
{
    public interface IQuizDtoMapper
    {
        ITestDto Map(IQuizModel quiz);

        IAssignedTestDto Map(IAssignedTestModel assignedTest);
    }
}