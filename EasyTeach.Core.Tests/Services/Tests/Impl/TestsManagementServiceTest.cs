using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.Tests.Exceptions;
using EasyTeach.Core.Services.Tests.Impl;
using EasyTeach.Core.Validation;
using EasyTeach.Core.Validation.EntityValidator;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Core.Tests.Services.Tests.Impl
{
    public class TestsManagementServiceTest
    {
        private readonly QuizManagementService quizManagementService;


        private readonly IQuizRepository quizRepository;
        private readonly IQuizDtoMapper _quizDtoMapper;
        private readonly EntityValidator _entityValidator;

        private readonly IQuizModel _validQuiz;
        private readonly AssignedTestModel _validAssignment;


        public TestsManagementServiceTest()
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
            var testDto = A.Fake<ITestDto>();

            A.CallTo(() => _quizDtoMapper.Map(_validQuiz)).Returns(testDto);
            A.CallTo(() => _entityValidator.ValidateEntity(_validQuiz)).Returns(new EntityValidationResult(true));

            Assert.DoesNotThrow(() => quizManagementService.CreateTestAsync(_validQuiz).Wait());
            A.CallTo(() => _quizDtoMapper.Map(_validQuiz)).MustHaveHappened();
            A.CallTo(() => quizRepository.CreateTestAsync(testDto)).MustHaveHappened();
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


            var aggregateException = Assert.Throws<AggregateException>(() => quizManagementService.CreateTestAsync(invalidTest).Wait());
            var exception = (InvalidTestException)aggregateException.GetBaseException();

            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Name"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Questions"));
        }

        [Fact]
        public void CreateTestAsync_NullModel_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => quizManagementService.CreateTestAsync(null).Wait());
            var exception = aggregateException.GetBaseException() as ArgumentNullException;
            
            Assert.NotNull(exception);
            Assert.True(exception.ParamName == "newQuiz");
            A.CallTo(() => quizRepository.CreateTestAsync(A<ITestDto>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _entityValidator.ValidateEntity(A<IQuizModel>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AssignTestToGroupAsync_NullModel_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => quizManagementService.AssignTestToGroupAsync(null).Wait());

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

            Assert.DoesNotThrow(() => quizManagementService.AssignTestToGroupAsync(_validAssignment));

            A.CallTo(() => _quizDtoMapper.Map(_validAssignment)).MustHaveHappened();
            A.CallTo(() => quizRepository.AssignTestAsync(assignmentDto)).MustHaveHappened();
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

            var aggregateException = Assert.Throws<AggregateException>(() => quizManagementService.AssignTestToGroupAsync(invalidAssignment).Wait());
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
