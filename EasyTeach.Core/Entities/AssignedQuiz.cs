using System;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities
{
    public class AssignedQuiz : IAssignedQuizModel
    {
        public Quiz Quiz { get; set; }

        public Group Group { get; set; }

        [Required]
        IQuizModel IAssignedQuizModel.Quiz
        {
            get { return Quiz; }
        }

        [Required]
        IGroupModel IAssignedQuizModel.Group
        {
            get { return Group; }
        }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        [Required]
        public int NumberOfQuestions { get; set; }
    }
}
