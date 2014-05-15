using EasyTeach.Core.Entities;

namespace EasyTeach.Web.Models.ViewModels.Quiz
{
    public class CreateQuizViewModel
    {
        public string Name { get; set; }

        public virtual Core.Entities.Quiz ToQuiz()
        {
            return new Core.Entities.Quiz { Name = Name };
        }
    }
}