using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities
{
    public class QuestionItem : IQuestionItemModel
    {
        public int QuestionItemId { get; set; }

        public string Text { get; set; }

        public bool IsSolution { get; set; }
    }
}