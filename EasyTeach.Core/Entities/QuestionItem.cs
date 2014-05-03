using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Entities
{
    public class QuestionItem
    {
        public int QuestionItemId { get; set; }

        public string Text { get; set; }

        public bool IsSolution { get; set; }
    }
}