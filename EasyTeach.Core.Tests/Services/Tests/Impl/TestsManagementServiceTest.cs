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
        private readonly TestsManagementService _testsManagementService;


        private readonly ITestsRepository _testsRepository;
        private readonly ITestDtoMapper _testDtoMapper;
        private readonly EntityValidator _entityValidator;

        private readonly ITestModel _validTest;
        private readonly AssignedTestModel _validAssignment;


        public TestsManagementServiceTest()
        {
            _entityValidator = A.Fake<EntityValidator>();
            _testsRepository = A.Fake<ITestsRepository>();
            _testDtoMapper = A.Fake<ITestDtoMapper>();
            
            _testsManagementService = new TestsManagementService(_testsRepository, _testDtoMapper, _entityValidator);

            _validTest = new TestModel
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
                            Test = new TestModel(),
                            Group = new Group()
                         };
        }

        [Fact]
        public void CreateTestAsync_ValidTest_TestCreated()
        {
            var testDto = A.Fake<ITestDto>();

            A.CallTo(() => _testDtoMapper.Map(_validTest)).Returns(testDto);
            A.CallTo(() => _entityValidator.ValidateEntity(_validTest)).Returns(new EntityValidationResult(true));

            Assert.DoesNotThrow(() => _testsManagementService.CreateTestAsync(_validTest).Wait());
            A.CallTo(() => _testDtoMapper.Map(_validTest)).MustHaveHappened();
            A.CallTo(() => _testsRepository.CreateTestAsync(testDto)).MustHaveHappened();
        }

        [Fact]
        public void CreateTestAsync_InvalidTest_ExceptionThrown()
        {
            var invalidTest = new TestModel();
            A.CallTo(() => _entityValidator.ValidateEntity<ITestModel>(invalidTest))
                .Returns(
                    new EntityValidationResult(
                        false,
                        new[]
                        {
                            new ValidationResult("Name required", new[] { "Name" }),
                            new ValidationResult("Questions must be not empty", new[] { "Questions" })
                        }));


            var aggregateException = Assert.Throws<AggregateException>(() => _testsManagementService.CreateTestAsync(invalidTest).Wait());
            var exception = (InvalidTestException)aggregateException.GetBaseException();

            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Name"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Questions"));
        }

        [Fact]
        public void CreateTestAsync_NullModel_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => _testsManagementService.CreateTestAsync(null).Wait());
            var exception = aggregateException.GetBaseException() as ArgumentNullException;
            
            Assert.NotNull(exception);
            Assert.True(exception.ParamName == "newTest");
            A.CallTo(() => _testsRepository.CreateTestAsync(A<ITestDto>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _entityValidator.ValidateEntity(A<ITestModel>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AssignTestToGroupAsync_NullModel_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => _testsManagementService.AssignTestToGroupAsync(null).Wait());

            var exception = aggregateException.GetBaseException() as ArgumentNullException;

            Assert.NotNull(exception);
            Assert.True(exception.ParamName == "assignedTest");
        }

        [Fact]
        public void AssignTestToGroupAsync_ValidAssignment_Assigned()
        {
            var assignmentDto = A.Fake<IAssignedTestDto>();
            A.CallTo(() => _testDtoMapper.Map(_validAssignment)).Returns(assignmentDto);
            A.CallTo(
                () =>
                    _entityValidator.ValidateEntity<IAssignedTestModel>(_validAssignment)).Returns(null);

            Assert.DoesNotThrow(() => _testsManagementService.AssignTestToGroupAsync(_validAssignment));

            A.CallTo(() => _testDtoMapper.Map(_validAssignment)).MustHaveHappened();
            A.CallTo(() => _testsRepository.AssignTestAsync(assignmentDto)).MustHaveHappened();
        }

        //[Fact]
        //public void AssignTestToGroupAsync_AssignmentWithTimeSpan_Assigned()
        //{
        //    _validAssignment.StartDate = DateTime.Now;
        //    _validAssignment.EndDate = DateTime.Now.AddHours(1);
        //    var assignmentDto = A.Fake<IAssignedTestDto>();

        //    A.CallTo(() => _testDtoMapper.Map(_validAssignment)).Returns(assignmentDto);

        //    Assert.DoesNotThrow(() => _testsManagementService.AssignTestToGroupAsync(_validAssignment));

        //    A.CallTo(() => _testDtoMapper.Map(_validAssignment)).MustHaveHappened();
        //    A.CallTo(() => _testsRepository.AssignTestAsync(assignmentDto)).MustHaveHappened();
        //}

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

            var aggregateException = Assert.Throws<AggregateException>(() => _testsManagementService.AssignTestToGroupAsync(invalidAssignment).Wait());
            var exception = (InvalidAssignedTestException) aggregateException.GetBaseException();

            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Test"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Group"));
        }

        //[Fact]
        //public void AssignTestToGroupAsync_InvalidDateSpan_ExceptionThrown()
        //{
        //    _validAssignment.StartDate = DateTime.Now;
        //    _validAssignment.EndDate = _validAssignment.StartDate.Value.Subtract(new TimeSpan(1, 0, 0));

        //    var aggregateException = Assert.Throws<AggregateException>(() => _testsManagementService.AssignTestToGroupAsync(_validAssignment).Wait());
        //    var exception = (InvalidAssignedTestException)aggregateException.GetBaseException(); 

        //    Assert.True(exception.ValidationResults.Any(x => x.MemberNames.Any(mn => mn == "StartDate")));
        //    Assert.True(exception.ValidationResults.Any(x => x.MemberNames.Any(mn => mn == "EndDate")));
        //}

        private class AssignedTestModel : IAssignedTestModel
        {
            [Required]
            public ITestModel Test { get; set; }

            [Required]
            public Group Group { get; set; }
            
            public DateTime? StartDate { get; set; }
            
            public DateTime? EndDate { get; set; }
        }
    }
}
