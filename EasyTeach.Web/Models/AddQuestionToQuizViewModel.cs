using EasyTeach.Web.Controllers;

namespace EasyTeach.Web.Models
{
    public class AddQuestionToQuizViewModel
    {
        public int QuizId { get; set; }

        public QuestionViewModel Question { get; set; }
    }
}