namespace EasyTeach.Web.Models.ViewModels.Quiz
{
    public class AddQuestionToQuizViewModel
    {
        public int QuizId { get; set; }

        public QuestionViewModel Question { get; set; }
    }
}