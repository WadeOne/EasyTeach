using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Quiz;
using EasyTeach.Core.Services.Quiz.Impl;
using EasyTeach.Web.Controllers;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Web.Tests.Controllers
{
    public class QuizControllerTest
    {
        private readonly QuizController _controller;

        private readonly IQuizManagementService _quizManagementService;

        private readonly QuizModel _validQuizModelWithoutId;
        private readonly IQuizModel _validQuizModelWithId;

        private readonly CreateQuizViewModel _createQuizViewModel;

        public QuizControllerTest()
        {
            _quizManagementService = A.Fake<IQuizManagementService>();
            _controller = new QuizController(_quizManagementService);
            _validQuizModelWithId = A.Fake<IQuizModel>();
            _validQuizModelWithoutId = A.Fake<QuizModel>();
            _createQuizViewModel = A.Fake<CreateQuizViewModel>();
        }

        //[Fact]
        //public void CreateQuiz_ValidQuiz_QuizCreated()
        //{
        //    A.CallTo(() => _createQuizViewModel.ToQuizModel()).Returns(_validQuizModelWithoutId);
        //    A.CallTo(() => _quizManagementService.CreateQuizAsync(_validQuizModelWithoutId)).Returns(_validQuizModelWithoutId);

        //    var result = _controller.CreateTest(_createQuizViewModel);
        //    Assert.NotNull(result);
        //    A.CallTo(() => _quizManagementService.CreateQuizAsync(_validQuizModelWithoutId)).MustHaveHappened();
        //}
    }
}
