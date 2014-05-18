using EasyTeach.Web.Models.ViewModels.Quiz;
using Xunit;

namespace EasyTeach.Web.Tests.Models.ViewModels
{
    public class AssignToGoupViewModelTest
    {
        [Fact]
        public void ToQuestion_ReturnedQuestion()
        {
            var viewModel = new AssignToGoupViewModel {GroupNumber = 1, Year = 1953};
            
            var result = viewModel.ToGroup();

            Assert.Equal(viewModel.GroupNumber, result.GroupNumber);
            Assert.Equal(viewModel.Year, result.Year);
        }
    }
}
