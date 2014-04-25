using System.Collections.Generic;

namespace EasyTeach.Core.Entities.Data.Quiz
{
    public interface IQuizDto
    {
        int QuizId { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        ICollection<Question> Questions { get; set; }

        int Version { get; set; }
    }
}