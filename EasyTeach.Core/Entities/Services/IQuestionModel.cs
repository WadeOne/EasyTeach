using System.Collections.Generic;

using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Services
{
    public interface IQuestionModel
    {
        int QuestionId { get; }

        QuestionType QuestionType { get; }

        IEnumerable<IQuestionItemModel> QuestionItems { get; set; }

        string TextAnswer { get; }

        string QuestionText { get; } 
    }
}