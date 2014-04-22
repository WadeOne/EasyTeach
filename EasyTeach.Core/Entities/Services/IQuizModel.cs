using System.Collections.Generic;

namespace EasyTeach.Core.Entities.Services
{
    public interface IQuizModel
    {
        string Name { get; }

        string Description { get; }

        IEnumerable<IQuestionModel> Questions { get; }
    }
}