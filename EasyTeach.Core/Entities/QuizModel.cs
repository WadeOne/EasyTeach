using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities
{
    public class QuizModel : IQuizModel
    {
        public int QuizId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }

        IEnumerable<IQuestionModel> IQuizModel.Questions
        {
            get
            {
                return Questions;
            }
            set
            {
                Questions = (ICollection<QuestionModel>)value;
            }
        }

        public int Version { get; set; }
    }
}
