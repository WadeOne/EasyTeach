﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities
{
    public class Quiz : IQuizModel
    {
        public int QuizId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Question> Questions { get; set; }

        public bool IsDeprecated { get; set; }

        IEnumerable<IQuestionModel> IQuizModel.Questions
        {
            get
            {
                return Questions;
            }
            set
            {
                Questions = (ICollection<Question>)value;
            }
        }
    }
}
