using System.Threading.Tasks;

using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories
{
    public interface IQuizRepository
    {
        Task CreateQuizAsync(IQuizDto quiz);
        Task AssignQuizAsync(IAssignedTestDto assignedTest);
    }
}
