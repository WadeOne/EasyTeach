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
using EasyTeach.Web.Models;
using EasyTeach.Web.Models.ViewModels;
using EasyTeach.Web.Models.ViewModels.Quiz;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Web.Tests.Controllers
{
    public class QuizControllerTest
    {
        private readonly QuizController _controller;

        private readonly IQuizManagementService _quizManagementService;
        private readonly EntityValidator _entityValidator;

        private readonly Quiz quizWithoutId;
        private readonly IQuizModel _quizModelWithId;

        private readonly CreateQuizViewModel _createQuizViewModel;
        private readonly AddQuestionToQuizViewModel _addQuestionToQuizViewModel;
        private readonly QuestionModel questionModel;

        private readonly QuestionViewModel _questionViewModel;

        public QuizControllerTest()
        {
            _quizManagementService = A.Fake<IQuizManagementService>();
            _entityValidator = A.Fake<EntityValidator>();
            _controller = new QuizController(_quizManagementService);
            _quizModelWithId = A.Fake<IQuizModel>();
            quizWithoutId = A.Fake<Quiz>();
            _createQuizViewModel = A.Fake<CreateQuizViewModel>();
            _questionViewModel = A.Fake<QuestionViewModel>();
            _addQuestionToQuizViewModel = new AddQuestionToQuizViewModel
            {
                QuizId = 1,
                Question = _questionViewModel
            };
            questionModel = new QuestionModel();
        }

        [Fact]
        public void PostQuiz_ValidQuiz_QuizCreatedAndOkResultSent()
        {
            A.CallTo(() => _createQuizViewModel.ToQuiz()).Returns(quizWithoutId);
            A.CallTo(() => _quizManagementService.CreateQuizAsync(quizWithoutId)).Returns(_quizModelWithId);
            A.CallTo(() => _quizModelWithId.QuizId).Returns(1);

            var result = _controller.Post(_createQuizViewModel).Result as OkNegotiatedContentResult<IQuizModel>;
            Assert.NotNull(result);
            A.CallTo(() => _quizManagementService.CreateQuizAsync(quizWithoutId)).MustHaveHappened();
            Assert.Equal(_quizModelWithId.QuizId, result.Content.QuizId);
        }

        [Fact]
        public void PostQuiz_InvalidQuiz_QuizNotCreatedErrorResultSent()
        {
            A.CallTo(() => _createQuizViewModel.ToQuiz()).Returns(quizWithoutId);
            A.CallTo(() => _quizManagementService.CreateQuizAsync(quizWithoutId))
                .Throws(
                    new InvalidQuizException(new List<ValidationResult>
                    {
                        new ValidationResult("Name is required", new[] {"Name"})
                    }));

            var result = _controller.Post(_createQuizViewModel).Result as InvalidModelStateResult;

            Assert.NotNull(result);
            Assert.True(result.ModelState.Any(ei => ei.Key == "Name"));
        }

        [Fact]
        public void PostQuiz_NullQuiz_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => _controller.Post(null).Wait());
            var baseException = aggregateException.GetBaseException() as ArgumentNullException;

            Assert.NotNull(baseException);
            Assert.Equal(baseException.ParamName, "newQuizViewModel");
        }

        [Fact]
        public void AddQuestion_NullModel_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => _controller.AddQuestion(null).Wait());
            var baseException = aggregateException.GetBaseException() as ArgumentNullException;

            Assert.NotNull(baseException);
            Assert.Equal(baseException.ParamName, "questionToQuizViewModel");
        }

        [Fact]
        public void AddQuestion_ValidModel_QuestionAdded()
        {
            A.CallTo(() => _questionViewModel.ToQuestion()).Returns(questionModel);

            var result = _controller.AddQuestion(_addQuestionToQuizViewModel).Result as OkResult;
            Assert.NotNull(result);
            A.CallTo(() => _quizManagementService.AddQuestionToQuiz(_addQuestionToQuizViewModel.QuizId, questionModel)).MustHaveHappened();
        }

        [Fact]
        public void AddQuestion_InvalidModel_QuestionNotAddedResultSent()
        {
            A.CallTo(() => _questionViewModel.ToQuestion()).Returns(questionModel);
            A.CallTo(() => _quizManagementService.AddQuestionToQuiz(_addQuestionToQuizViewModel.QuizId, questionModel))
                .Throws(new InvalidAddQuestionException(new List<ValidationResult>
                                                        {
                                                            new ValidationResult("QuizId is required", new []{"QuizId"})
                                                        }));

            var result = _controller.AddQuestion(_addQuestionToQuizViewModel).Result as InvalidModelStateResult;
            Assert.NotNull(result);
            A.CallTo(() => _quizManagementService.AddQuestionToQuiz(_addQuestionToQuizViewModel.QuizId, questionModel)).MustHaveHappened();
            Assert.True(result.ModelState.Any(ms => ms.Key == "QuizId"));
        }

        [Fact]
        public void Get_ReturnedAllQuizes()
        {
            A.CallTo(() => _quizManagementService.GetAllQuizes())
                .Returns(
                    new List<IQuizModel>
                    {
                        new Quiz { QuizId = 1, Name = "Quiz 1", Description = "Description 1" }
                    });

            var result = _controller.Get().Result;

            Assert.NotNull(result);
            Assert.True(result.Count() == 1);
            Assert.True(result.Any(x => x.QuizId == 1 && x.Name == "Quiz 1" && x.Description == "Description 1"));
        }

        //[Fact]
        //public void Get_QuizExists_ReturnedQuiz()
        //{
        //    int quizId = 1;
        //    A.CallTo(() => _quizManagementService.GetQuiz(quizId)).Returns(_quizModelWithId);

        //    var result = _controller.Get(quizId).Result as NegotiatedContentResult<Quiz>;

        //    Assert.NotNull(result);
        //    Assert.NotNull(result.Content);
        //    Assert.Equal(_quizModelWithId.QuizId, result.Content.QuizId);
        //}
    }
}
