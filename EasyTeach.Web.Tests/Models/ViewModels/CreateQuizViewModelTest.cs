using EasyTeach.Web.Models.ViewModels;
using EasyTeach.Web.Models.ViewModels.Quiz;

using Xunit;

namespace EasyTeach.Web.Tests.Models.ViewModels
{
    public class CreateQuizViewModelTest
    {
        [Fact]
        public void ToQuiz_CreateQuizViewModel_CorrectlyMapped()
        {
            var viewModel = new CreateQuizViewModel { Name = "Name" };
            var model = viewModel.ToQuiz();

            Assert.Equal(viewModel.Name, model.Name);
        }
    }
}
