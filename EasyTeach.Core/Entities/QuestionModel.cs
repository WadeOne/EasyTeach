using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities
{
    public class QuestionModel : IQuestionModel
    {
    	[Key]
        public int QuestionId { get; set; }

        public QuestionType QuestionType { get; set; }

        public ICollection<QuestionItemModel> QuestionItems { get; set; }

        IEnumerable<IQuestionItemModel> IQuestionModel.QuestionItems
        {
            get
            {
                return QuestionItems;
            }
            set
            {
                QuestionItems = (ICollection<QuestionItemModel>)value;
            }
        }

        public string TextAnswer { get; set; }

        public string QuestionText { get; set; }
    }
}