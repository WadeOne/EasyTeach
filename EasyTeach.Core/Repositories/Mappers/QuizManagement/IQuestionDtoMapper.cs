using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories.Mappers.QuizManagement
{
    public interface IQuestionDtoMapper
    {
        IQuestionDto Map(IQuestionModel model);
        IQuestionModel Map(IQuestionDto dto);
    }
}