using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.Quiz.Exceptions;
using EasyTeach.Core.Services.Quiz.Impl;
using EasyTeach.Core.Validation.EntityValidator;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Core.Tests.Services.Quiz.Impl
{
    public class QuizManagementServiceTest
    {
        private readonly QuizManagementService quizManagementService;


        private readonly IQuizRepository quizRepository;
        private readonly IQuizDtoMapper _quizDtoMapper;
        private readonly EntityValidator _entityValidator;

        private readonly IQuizModel _validQuiz;
        private readonly AssignedTestModel _validAssignment;


        public QuizManagementServiceTest()
        {
            _entityValidator = A.Fake<EntityValidator>();
            quizRepository = A.Fake<IQuizRepository>();
            _quizDtoMapper = A.Fake<IQuizDtoMapper>();
            
            quizManagementService = new QuizManagementService(quizRepository, _quizDtoMapper, _entityValidator);

            _validQuiz = new QuizModel
                         {
                             Name = "Test",
                             Description = "Description",
                             Questions =
                                 new List<IQuestionModel>
                                 {
                                     A.Fake<IQuestionModel>(),
                                     A.Fake<IQuestionModel>()
                                 }
                         };

            _validAssignment = new AssignedTestModel
                         {
                            Quiz = new QuizModel(),
                            Group = new Group()
                         };
        }

        [Fact]
        public void CreateTestAsync_ValidTest_TestCreated()
        {
            var testDto = A.Fake<IQuizDto>();

            A.CallTo(() => _quizDtoMapper.Map(_validQuiz)).Returns(testDto);
            A.CallTo(() => _entityValidator.ValidateEntity(_validQuiz)).Returns(new EntityValidationResult(true));

            Assert.DoesNotThrow(() => quizManagementService.CreateQuizAsync(_validQuiz).Wait());
            A.CallTo(() => _quizDtoMapper.Map(_validQuiz)).MustHaveHappened();
            A.CallTo(() => quizRepository.CreateQuizAsync(testDto)).MustHaveHappened();
        }

        [Fact]
        public void CreateTestAsync_InvalidTest_ExceptionThrown()
        {
            var invalidTest = new QuizModel();
            A.CallTo(() => _entityValidator.ValidateEntity<IQuizModel>(invalidTest))
                .Returns(
                    new EntityValidationResult(
                        false,
                        new[]
                        {
                            new ValidationResult("Name required", new[] { "Name" }),
                            new ValidationResult("Questions must be not empty", new[] { "Questions" })
                        }));


            var aggregateException = Assert.Throws<AggregateException>(() => quizManagementService.CreateQuizAsync(invalidTest).Wait());
            var exception = (InvalidQuizException)aggregateException.GetBaseException();

            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Name"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Questions"));
        }

        [Fact]
        public void CreateTestAsync_NullModel_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => quizManagementService.CreateQuizAsync(null).Wait());
            var exception = aggregateException.GetBaseException() as ArgumentNullException;
            
            Assert.NotNull(exception);
            Assert.True(exception.ParamName == "newQuiz");
            A.CallTo(() => quizRepository.CreateQuizAsync(A<IQuizDto>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _entityValidator.ValidateEntity(A<IQuizModel>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AssignTestToGroupAsync_NullModel_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => quizManagementService.AssignQuizToGroupAsync(null).Wait());

            var exception = aggregateException.GetBaseException() as ArgumentNullException;

            Assert.NotNull(exception);
            Assert.True(exception.ParamName == "assignedTest");
        }

        [Fact]
        public void AssignTestToGroupAsync_ValidAssignment_Assigned()
        {
            var assignmentDto = A.Fake<IAssignedTestDto>();
            A.CallTo(() => _quizDtoMapper.Map(_validAssignment)).Returns(assignmentDto);
            A.CallTo(
                () =>
                    _entityValidator.ValidateEntity<IAssignedTestModel>(_validAssignment)).Returns(null);

            Assert.DoesNotThrow(() => quizManagementService.AssignQuizToGroupAsync(_validAssignment));

            A.CallTo(() => _quizDtoMapper.Map(_validAssignment)).MustHaveHappened();
            A.CallTo(() => quizRepository.AssignQuizAsync(assignmentDto)).MustHaveHappened();
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

            var aggregateException = Assert.Throws<AggregateException>(() => quizManagementService.AssignQuizToGroupAsync(invalidAssignment).Wait());
            var exception = (InvalidAssignedTestException) aggregateException.GetBaseException();

            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Test"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Group"));
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
