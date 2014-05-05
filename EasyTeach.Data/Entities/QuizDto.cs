﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;

namespace EasyTeach.Data.Entities
{
    public class QuizDto : IQuizDto
    {
        [Key]
        public int QuizId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }

        public int Version { get; set; }
    }
}