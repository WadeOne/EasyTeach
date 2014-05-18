using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories.Mappers.QuizManagement
{
    public interface IQuizDtoMapper
    {
        IQuizDto Map(IQuizModel quiz);

        IQuizModel Map(IQuizDto quizDto);

        IAssignedQuizDto Map(IAssignedQuizModel assignedQuiz);
    }
}