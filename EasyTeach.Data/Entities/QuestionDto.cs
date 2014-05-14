using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Enums;

namespace EasyTeach.Data.Entities
{
    public class QuestionDto : IQuestionDto
    {
        [Key]
        public int QuestionId { get; set; }

        public QuestionType QuestionType { get; set; }

        public ICollection<QuestionItemDto> QuestionItems;

        IEnumerable<IQuestionItemDto> IQuestionDto.QuestionItems
        {
            get
            {
                return QuestionItems;
            }
            set
            {
                QuestionItems = (ICollection<QuestionItemDto>)value;
            }
        }

        public string TextAnswer { get; set; }

        public string QuestionText { get; set; }
    }

}
