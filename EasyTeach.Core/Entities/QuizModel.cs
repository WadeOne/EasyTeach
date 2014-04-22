using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Validation.Attributes;

namespace EasyTeach.Core.Entities
{
    public class QuizModel : IQuizModel
    {
        public int Id { get; private set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [NotEmptyCollection]
        public IEnumerable<IQuestionModel> Questions { get; set; }
    }
}
