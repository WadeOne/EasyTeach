using EasyTeach.Core.Entities;

namespace EasyTeach.Web.Models.ViewModels
{
    public class CreateQuizViewModel
    {
        public string Name { get; set; }

        public virtual QuizModel ToQuiz()
        {
            return new QuizModel { Name = Name };
        }
    }
}