using System.Threading.Tasks;

using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Core.Repositories
{
    public interface ITestsRepository
    {
        Task CreateTestAsync(ITestDto testDto);
    }
}
