using System.Threading.Tasks;

using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories
{
    public interface ITestsRepository
    {
        Task CreateTestAsync(ITestDto testDto);
        Task AssignTestAsync(IAssignedTestDto assignedTest);
    }
}
