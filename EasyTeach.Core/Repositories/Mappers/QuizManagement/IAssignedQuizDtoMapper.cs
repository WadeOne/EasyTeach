using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories.Mappers.QuizManagement
{
    public interface IAssignedQuizDtoMapper
    {
        IAssignedQuizDto Map(IAssignedQuizModel model);

        IAssignedQuizModel Map(IAssignedQuizDto dto);
    }
}