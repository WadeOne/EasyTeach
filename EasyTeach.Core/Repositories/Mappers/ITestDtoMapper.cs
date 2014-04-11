using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories.Mappers
{
    public interface ITestDtoMapper
    {
        ITestDto Map(ITestModel testModel);
    }
}