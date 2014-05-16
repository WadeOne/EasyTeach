using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers.QuizManagement;
using EasyTeach.Core.Services.Quiz.Exceptions;
using EasyTeach.Core.Services.Quiz.Impl;
using EasyTeach.Core.Validation.EntityValidator;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Core.Tests.Services.Quiz.Impl
{
    public class QuizManagementServiceTest
    {
        private readonly QuizManagementService _quizManagementService;


        private readonly IQuizRepository _quizRepository;
        private readonly IQuizDtoMapper _quizDtoMapper;
        private readonly IQuestionDtoMapper _questionDtoMapper;
        private readonly EntityValidator _entityValidator;

        private readonly IQuizModel _validQuiz;
        private readonly AssignedTestModel _validAssignment;


        public QuizManagementServiceTest()
        {
            _entityValidator = A.Fake<EntityValidator>();
            _quizRepository = A.Fake<IQuizRepository>();
            _quizDtoMapper = A.Fake<IQuizDtoMapper>();
            _questionDtoMapper = A.Fake<IQuestionDtoMapper>();
            
            _quizManagementService = new QuizManagementService(_quizRepository, _quizDtoMapper, _entityValidator, _questionDtoMapper);

            _validQuiz = new Entities.Quiz
                         {
                             Name = "Test",
                             Description = "Description",
                             Questions =
                                 new List<Question>
                                 {
                                     A.Fake<Question>(),
                                     A.Fake<Question>()
                                 }
                         };

            _validAssignment = new AssignedTestModel
                         {
                            Quiz = new Entities.Quiz(),
                            Group = new Group()
                         };
        }

        [Fact]
        public void CreateTestAsync_ValidTest_TestCreated()
        {
            var testDto = A.Fake<IQuizDto>();

            A.CallTo(() => _quizDtoMapper.Map(_validQuiz)).Returns(testDto);
            A.CallTo(() => _entityValidator.ValidateEntity(_validQuiz)).Returns(new EntityValidationResult(true));

            Assert.DoesNotThrow(() => _quizManagementService.CreateQuizAsync(_validQuiz).Wait());
            A.CallTo(() => _quizDtoMapper.Map(_validQuiz)).MustHaveHappened();
            A.CallTo(() => _quizRepository.CreateQuizAsync(testDto)).MustHaveHappened();
        }

        [Fact]
        public void CreateTestAsync_InvalidTest_ExceptionThrown()
        {
            var invalidTest = new Entities.Quiz();
            A.CallTo(() => _entityValidator.ValidateEntity<IQuizModel>(invalidTest))
                .Returns(
                    new EntityValidationResult(
                        false,
                        new[]
                        {
                            new ValidationResult("Name required", new[] { "Name" }),
                            new ValidationResult("Questions must be not empty", new[] { "Questions" })
                        }));


            var aggregateException = Assert.Throws<AggregateException>(() => _quizManagementService.CreateQuizAsync(invalidTest).Wait());
            var exception = (InvalidQuizException)aggregateException.GetBaseException();

            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Name"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Questions"));
        }

        [Fact]
        public void CreateTestAsync_NullModel_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => _quizManagementService.CreateQuizAsync(null).Wait());
            var exception = aggregateException.GetBaseException() as ArgumentNullException;
            
            Assert.NotNull(exception);
            Assert.True(exception.ParamName == "newQuiz");
            A.CallTo(() => _quizRepository.CreateQuizAsync(A<IQuizDto>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _entityValidator.ValidateEntity(A<IQuizModel>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AssignTestToGroupAsync_NullModel_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => _quizManagementService.AssignQuizToGroupAsync(null).Wait());

            var exception = aggregateException.GetBaseException() as ArgumentNullException;

            Assert.NotNull(exception);
            Assert.True(exception.ParamName == "assignedTest");
        }

        [Fact]
        public void AssignTestToGroupAsync_ValidAssignment_Assigned()
        {
            var assignmentDto = A.Fake<IAssignedQuizDto>();
            A.CallTo(() => _quizDtoMapper.Map(_validAssignment)).Returns(assignmentDto);
            A.CallTo(
                () =>
                    _entityValidator.ValidateEntity<IAssignedTestModel>(_validAssignment)).Returns(null);

            Assert.DoesNotThrow(() => _quizManagementService.AssignQuizToGroupAsync(_validAssignment));

            A.CallTo(() => _quizDtoMapper.Map(_validAssignment)).MustHaveHappened();
            A.CallTo(() => _quizRepository.AssignQuizAsync(assignmentDto)).MustHaveHappened();
        }

        [Fact]
        public void AssignTestToGroupAsync_InvalidAssignment_ExceptionThrown()
        {
            var invalidAssignment = new AssignedTestModel();

            A.CallTo(
                () =>
                    _entityValidator.ValidateEntity<IAssignedTestModel>(invalidAssignment))
                .Returns(
                    new EntityValidationResult(false, new[]
                    {
                        new ValidationResult("Test required", new[] {"Test"}),
                        new ValidationResult("Group required", new[] {"Group"}),
                    }));

            var aggregateException = Assert.Throws<AggregateException>(() => _quizManagementService.AssignQuizToGroupAsync(invalidAssignment).Wait());
            var exception = (InvalidAssignedTestException) aggregateException.GetBaseException();

            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Test"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Group"));
        }

        [Fact]
        public void AddQuestionToQuiz_NullQuestion_ExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => _quizManagementService.AddQuestionToQuiz(1, null).Wait());
            var exception = aggregateException.GetBaseException() as ArgumentNullException;

            Assert.NotNull(exception);
            Assert.Equal("question", exception.ParamName);
        }

        [Fact]
        public void AddQuestionToQuiz_ValidQuestionQuizExists_QuestionAddedToQuiz()
        {
            int quizId = 1;
            IQuizDto quizDto = A.Fake<IQuizDto>();
            var question = A.Fake<IQuestionModel>();
            var questionDto = A.Fake<IQuestionDto>();
            A.CallTo(() => _quizRepository.GetQuiz(quizId)).Returns(quizDto);
            A.CallTo(() => _entityValidator.ValidateEntity(question)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _questionDtoMapper.Map(question)).Returns(questionDto);

            _quizManagementService.AddQuestionToQuiz(quizId, question);

            A.CallTo(() => _quizRepository.AddQuestionToQuiz(quizId, questionDto)).MustHaveHappened();
        }

        [Fact]
        public void AddQuestionToQuiz_ValidQuestionQuizDoesntExist_ExceptionThrown()
        {
            int quizId = 1;
            var question = A.Fake<IQuestionModel>();
            A.CallTo(() => _quizRepository.GetQuiz(quizId)).Returns((IQuizDto)null);

            var aggregateException = Assert.Throws<AggregateException>(() => _quizManagementService.AddQuestionToQuiz(quizId, question).Wait());
            var baseException = aggregateException.GetBaseException() as InvalidAddQuestionException;

            Assert.NotNull(baseException);
            Assert.True(baseException.ValidationResults.Any(vr => vr.MemberNames.First() == "QuizId"));
            A.CallTo(() => _entityValidator.ValidateEntity(A<object>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _questionDtoMapper.Map(A<IQuestionModel>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _quizRepository.AddQuestionToQuiz(A<int>.Ignored, A<IQuestionDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AddQuestionToQuiz_InvalidQuestionQuizExist_ExceptionThrown()
        {
            int quizId = 1;
            IQuizDto quizDto = A.Fake<IQuizDto>();
            var question = A.Fake<IQuestionModel>();
            A.CallTo(() => _quizRepository.GetQuiz(quizId)).Returns(quizDto);
            A.CallTo(() => _entityValidator.ValidateEntity(question))
                .Returns( new EntityValidationResult(false, new List<ValidationResult> 
                                                            {
                                                                new ValidationResult(
                                                                    "Question items should not be empty",
                                                                    new[] { "QuestionItems" })
                                                    }));

            var aggregateException = Assert.Throws<AggregateException>(() => _quizManagementService.AddQuestionToQuiz(quizId, question).Wait());
            var baseException = aggregateException.GetBaseException() as InvalidAddQuestionException;

            Assert.NotNull(baseException);
            Assert.True(baseException.ValidationResults.Any(vr => vr.MemberNames.First() == "QuestionItems"));
            A.CallTo(() => _questionDtoMapper.Map(A<IQuestionModel>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _quizRepository.AddQuestionToQuiz(A<int>.Ignored, A<IQuestionDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void GetAllQuizes_ReturnedAllQuizes()
        {
            var quizDto = A.Fake<IQuizDto>();
            var quizModel = A.Fake<IQuizModel>();
            A.CallTo(() => quizDto.QuizId).Returns(1);
            A.CallTo(() => quizDto.Name).Returns("Name");
            A.CallTo(() => quizDto.Description).Returns("Description");
            A.CallTo(() => quizModel.QuizId).Returns(1);
            A.CallTo(() => quizModel.Name).Returns("Name");
            A.CallTo(() => quizModel.Description).Returns("Description");
            A.CallTo(() => _quizRepository.GetAllQuizes()).Returns(new List<IQuizDto> { quizDto });
            A.CallTo(() => _quizDtoMapper.Map(quizDto)).Returns(quizModel);

            var result = _quizManagementService.GetAllQuizes().Result;

            Assert.NotNull(result);
            Assert.True(result.Count() == 1);
            Assert.True(result.Any(x => x.QuizId == quizDto.QuizId && x.Name == quizDto.Name && x.Description == quizDto.Description));
        }

        [Fact]
        public void GetQuiz_QuizExists_ReturnedQuiz()
        {
            int quizId = 1;
            IQuizModel quiz = A.Fake<IQuizModel>();
            IQuizDto quizDto = A.Fake<IQuizDto>();
            A.CallTo(() => _quizRepository.GetQuiz(quizId)).Returns(quizDto);
            A.CallTo(() => _quizDtoMapper.Map(quizDto)).Returns(quiz);

            var result = _quizManagementService.GetQuiz(quizId).Result;

            Assert.NotNull(result);
            Assert.Equal(quiz, result);
        }

        [Fact]
        public void GetQuiz_QuizDoesntExist_ExceptionThrown()
        {
            int quizId = 1;
            A.CallTo(() => _quizRepository.GetQuiz(quizId)).Returns((IQuizDto) null);

            var aggregateException = Assert.Throws<AggregateException>(() => _quizManagementService.GetQuiz(quizId).Wait());
            var exception = aggregateException.GetBaseException() as InvalidQuizException;

            Assert.NotNull(exception);
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "QuizId"));
            Assert.True(exception.ValidationResults.Any(x => x.ErrorMessage == string.Format("Quiz with id {0} doesn't exist", quizId)));
        }

        private class AssignedTestModel : IAssignedTestModel
        {
            [Required]
            public IQuizModel Quiz { get; set; }

            [Required]
            public Group Group { get; set; }
            
            public DateTime? StartDate { get; set; }
            
            public DateTime? EndDate { get; set; }
        }
    }
}
