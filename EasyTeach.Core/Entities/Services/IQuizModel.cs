using System.Collections.Generic;

namespace EasyTeach.Core.Entities.Services
{
    public interface IQuizModel
    {
        int Id { get;  }

        string Name { get; }

        string Description { get; }

        IEnumerable<IQuestionModel> Questions { get; }
    }
}