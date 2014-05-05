using System.Threading.Tasks;

using EasyTeach.Core.Entities.Data.Quiz;

namespace EasyTeach.Core.Repositories
{
    public interface IQuizRepository
    {
        Task<IQuizDto> GetQuiz(int quizId);
        Task CreateQuizAsync(IQuizDto quiz);
        Task AssignQuizAsync(IAssignedQuizDto assignedQuiz);
        Task AddQuestionToQuiz(int quizId, IQuestionDto questionDto);
    }
}
