using EasyTeach.Core.Entities;

namespace EasyTeach.Web.Models.ViewModels
{
    public class CreateQuizViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual QuizModel ToQuizModel()
        {
            return new QuizModel { Description = Description, Name = Name };
        }
    }
}