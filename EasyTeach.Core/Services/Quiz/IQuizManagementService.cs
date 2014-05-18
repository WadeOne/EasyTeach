using System.Collections.Generic;
using System.Threading.Tasks;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.Quiz
{
    public interface IQuizManagementService
    {
        Task<IQuizModel> CreateQuizAsync(IQuizModel newQuiz);

        Task AssignQuizToGroupAsync(IAssignedQuizModel assignedQuiz);

        Task AddQuestionToQuizAsync(int quizId, IQuestionModel question);

        Task<IEnumerable<IQuizModel>> GetAllQuizes();

        Task<IQuizModel> GetQuiz(int quizId);
    }
}