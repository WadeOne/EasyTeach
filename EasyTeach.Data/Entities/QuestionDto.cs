using System.Collections.Generic;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Enums;

namespace EasyTeach.Data.Entities
{
    public class QuestionDto : IQuestionDto
    {
        public int QuestionId { get; set; }

        public QuestionType QuestionType { get; set; }

        public ICollection<QuestionItem> QuestionItems { get; set; }

        public string TextAnswer { get; set; }

        public string QuestionText { get; set; }
    }
}
