using System.Threading.Tasks;

using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.Tests
{
    public interface ITestsManagementService
    {
        Task CreateTestAsync(ITestModel testModel);
    }
}