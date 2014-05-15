namespace EasyTeach.Web.Models.ViewModels.Quiz
{
    public class EditQuizViewModel
    {
        public int QuizId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsReadOnly { get; set; }
    }
}