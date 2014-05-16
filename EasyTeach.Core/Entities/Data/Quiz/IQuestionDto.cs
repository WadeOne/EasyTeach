using System.Collections.Generic;

using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Data.Quiz
{
    public interface IQuestionDto
    {
        int QuestionId { get; }

        QuestionType QuestionType { get; }

        IEnumerable<IQuestionItemDto> QuestionItems { get; set; }

        string TextAnswer { get; }

        string QuestionText { get; }  
    }
}