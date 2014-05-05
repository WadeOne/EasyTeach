using System.Threading.Tasks;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.Quiz
{
    public interface IQuizManagementService
    {
        Task<IQuizModel> CreateQuizAsync(IQuizModel newQuiz);

        Task AssignQuizToGroupAsync(IAssignedTestModel assignedTest);

        Task AddQuestionToQuiz(int quizId, IQuestionModel question);
    }
}