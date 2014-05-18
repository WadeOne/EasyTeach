using System;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities
{
    public class AssignedQuiz : IAssignedQuizModel
    {
        [Required]
        public IQuizModel Quiz { get; set; }

        [Required]
        public IGroupModel Group { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        [Required]
        public int NumberOfQuestions { get; set; }
    }
}
