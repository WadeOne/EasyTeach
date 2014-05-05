using System.Collections.Generic;

using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Services
{
    public interface IQuestionModel
    {
        int QuestionId { get; }

        QuestionType QuestionType { get; }

        ICollection<QuestionItem> QuestionItems { get; }

        string TextAnswer { get; }

        string QuestionText { get; } 
    }
}