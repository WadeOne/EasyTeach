using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.Tests.Exceptions;

namespace EasyTeach.Core.Services.Tests.Impl
{
    public class TestsManagementService : ITestsManagementService
    {
        private ITestsRepository _testsRepository;

        private ITestDtoMapper _testDtoMapper;

        public TestsManagementService(ITestsRepository testsRepository, ITestDtoMapper testDtoMapper)
        {
            if (testsRepository == null)
            {
                throw new ArgumentNullException("testsRepository");
            }

            if (testDtoMapper == null)
            {
                throw new ArgumentNullException("testDtoMapper");
            }

            _testsRepository = testsRepository;
            _testDtoMapper = testDtoMapper;
        }

        public async Task CreateTestAsync(ITestModel newTest)
        {
            if (newTest == null)
            {
                throw new ArgumentNullException("newTest");
            }

            var validationResults = new List<ValidationResult>();
            bool testIsValid = Validator.TryValidateObject(newTest,
                                                        new ValidationContext(newTest, null, null),
                                                        validationResults, true);

            if (newTest.Questions != null && newTest.Questions.Any() == false)
            {
                validationResults.Add(new ValidationResult(String.Format("Test can't contain no questions", newTest.Questions), new[] { "Questions" }));
                testIsValid = false;
            }

            if (testIsValid == false)
            {
                throw new InvalidTestException(validationResults);
            }

            var newTestDto = _testDtoMapper.Map(newTest);

            await _testsRepository.CreateTestAsync(newTestDto);
        }
    }
}
