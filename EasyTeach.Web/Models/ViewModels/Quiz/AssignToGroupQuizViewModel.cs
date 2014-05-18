using System;

namespace EasyTeach.Web.Models.ViewModels.Quiz
{
    public class AssignToGroupQuizViewModel
    {
        public int QuizId { get; set; }

        public AssignToGoupViewModel Group { get; set; }

        public DateTime? StartDateTime { get; set; }
        
        public DateTime? EndDateTime { get; set; }

        public int NumberOfQuestions { get; set; }
    }
}