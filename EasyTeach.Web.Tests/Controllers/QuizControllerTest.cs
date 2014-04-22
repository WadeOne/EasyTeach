using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.Results;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Quiz;
using EasyTeach.Core.Services.Quiz.Exceptions;
using EasyTeach.Core.Services.Quiz.Impl;
using EasyTeach.Core.Validation.EntityValidator;
using EasyTeach.Web.Controllers;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Web.Tests.Controllers
{
    public class QuizControllerTest
    {
        private readonly QuizController _controller;

        private readonly IQuizManagementService _quizManagementService;
        private readonly EntityValidator _entityValidator;

        private readonly QuizModel _quizModelWithoutId;
        private readonly IQuizModel _quizModelWithId;

        private readonly CreateQuizViewModel _createQuizViewModel;

        public QuizControllerTest()
        {
            _quizManagementService = A.Fake<IQuizManagementService>();
            _entityValidator = A.Fake<EntityValidator>();
            _controller = new QuizController(_quizManagementService);
            _quizModelWithId = A.Fake<IQuizModel>();
            _quizModelWithoutId = A.Fake<QuizModel>();
            _createQuizViewModel = A.Fake<CreateQuizViewModel>();
        }

        [Fact]
        public void CreateQuiz_ValidQuiz_QuizCreatedAndOkResultSent()
        {
            A.CallTo(() => _createQuizViewModel.ToQuizModel()).Returns(_quizModelWithoutId);
            A.CallTo(() => _quizManagementService.CreateQuizAsync(_quizModelWithoutId)).Returns(_quizModelWithId);
            A.CallTo(() => _quizModelWithId.Id).Returns(1);

            var result = _controller.CreateTest(_createQuizViewModel).Result as OkNegotiatedContentResult<IQuizModel>;
            Assert.NotNull(result);
            A.CallTo(() => _quizManagementService.CreateQuizAsync(_quizModelWithoutId)).MustHaveHappened();
            Assert.Equal(_quizModelWithId.Id, result.Content.Id);
        }

        [Fact]
        public void CreateQuiz_InvalidQuiz_QuizNotCreatedErrorResultSent()
        {
            A.CallTo(() => _createQuizViewModel.ToQuizModel()).Returns(_quizModelWithoutId);
            A.CallTo(() => _quizManagementService.CreateQuizAsync(_quizModelWithoutId))
                .Throws(
                    new InvalidQuizException(new List<ValidationResult>
                    {
                        new ValidationResult("Name is required", new[] {"Name"})
                    }));

            var result = _controller.CreateTest(_createQuizViewModel).Result as InvalidModelStateResult;

            Assert.NotNull(result);
            Assert.True(result.ModelState.Any(ei => ei.Key == "Name"));
        }

        [Fact]
        public void CreateQuiz_NullQuiz_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => _controller.CreateTest(null).Wait());
            var baseException = aggregateException.GetBaseException() as ArgumentNullException;

            Assert.NotNull(baseException);
            Assert.Equal(baseException.ParamName, "newQuizViewModel");
        }
    }
}
