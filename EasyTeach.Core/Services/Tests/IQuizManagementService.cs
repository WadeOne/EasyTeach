using System;
using System.Threading.Tasks;

using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.Tests
{
    public interface IQuizManagementService
    {
        Task CreateTestAsync(IQuizModel newQuiz);

        Task AssignTestToGroupAsync(IAssignedTestModel assignedTest);
    }
}