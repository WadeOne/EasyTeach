using System.Collections.Generic;

namespace EasyTeach.Core.Entities.Services
{
    public interface IQuizModel
    {
        int QuizId { get;  }

        string Name { get; }

        string Description { get; }

        IEnumerable<IQuestionModel> Questions { get; set;  }

        bool IsDeprecated { get; set; }
    }
}