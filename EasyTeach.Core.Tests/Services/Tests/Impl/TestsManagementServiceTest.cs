using System;
using System.Collections.Generic;
using System.Linq;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.Tests.Exceptions;
using EasyTeach.Core.Services.Tests.Impl;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Core.Tests.Services.Tests.Impl
{
    public class TestsManagementServiceTest
    {
        private readonly ITestsRepository _testsRepository;
        private readonly ITestDtoMapper _testDtoMapper;
        private readonly TestsManagementService _testsManagementService;
        private readonly ITestModel _validTest;


        public TestsManagementServiceTest()
        {
            _testsRepository = A.Fake<ITestsRepository>();
            _testDtoMapper = A.Fake<ITestDtoMapper>();
            _testsManagementService = new TestsManagementService(_testsRepository, _testDtoMapper);
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
        }

        [Fact]
        public void CreateTestAsync_ValidTest_TestCreated()
        {
            
            var testDto = A.Fake<ITestDto>();

            A.CallTo(() => _testDtoMapper.Map(_validTest)).Returns(testDto);

            Assert.DoesNotThrow(() => _testsManagementService.CreateTestAsync(_validTest).Wait());
            A.CallTo(() => _testDtoMapper.Map(_validTest)).MustHaveHappened();
            A.CallTo(() => _testsRepository.CreateTestAsync(testDto)).MustHaveHappened();
        }

        [Fact]
        public void CreateTestAsync_InvalidTest_ExceptionThrown()
        {
            var invalidTest = new TestModel();

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
        }
    }
}
