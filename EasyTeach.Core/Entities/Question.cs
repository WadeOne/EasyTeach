using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities
{
    public class Question : IQuestionModel
    {
    	[Key]
        public int QuestionId { get; set; }

        public QuestionType QuestionType { get; set; }

        public ICollection<QuestionItem> QuestionItems { get; set; }

        IEnumerable<IQuestionItemModel> IQuestionModel.QuestionItems
        {
            get
            {
                return QuestionItems;
            }
            set
            {
                QuestionItems = (ICollection<QuestionItem>)value;
            }
        }

        public string TextAnswer { get; set; }

        public string QuestionText { get; set; }
    }
}