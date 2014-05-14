using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities.Data.Quiz;

namespace EasyTeach.Data.Entities
{

    public class QuestionItemDto : IQuestionItemDto
    {
        [Key]
        public int QuestionItemId { get; set; }

        public string Text { get; set; }

        public bool IsSolution { get; set; }

        public QuestionDto Question { get; set; }
    }
}
