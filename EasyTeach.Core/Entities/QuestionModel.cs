using System.Collections.Generic;

using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities
{
    public class QuestionModel : IQuestionModel
    {
        public int QuestionId { get; set; }

        public QuestionType QuestionType { get; set; }

        public ICollection<QuestionItem> QuestionItems { get; set; }

        public string TextAnswer { get; set; }

        public string QuestionText { get; set; }
    }
}