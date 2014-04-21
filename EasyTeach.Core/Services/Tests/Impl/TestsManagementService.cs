using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.Tests.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;

namespace EasyTeach.Core.Services.Tests.Impl
{
    public class TestsManagementService : ITestsManagementService
    {
        private readonly ITestsRepository _testsRepository;

        private readonly ITestDtoMapper _testDtoMapper;

        private readonly EntityValidator _entityValidator;

        public TestsManagementService(ITestsRepository testsRepository, ITestDtoMapper testDtoMapper, EntityValidator entityValidator)
        {
            if (testsRepository == null)
            {
                throw new ArgumentNullException("testsRepository");
            }

            if (testDtoMapper == null)
            {
                throw new ArgumentNullException("testDtoMapper");
            }

            if (entityValidator == null)
            {
                throw new ArgumentNullException("entityValidator");
            }

            _testsRepository = testsRepository;
            _testDtoMapper = testDtoMapper;
            _entityValidator = entityValidator;
        }

        public async Task CreateTestAsync(ITestModel newTest)
        {
            if (newTest == null)
            {
                throw new ArgumentNullException("newTest");
            }

            var result = _entityValidator.ValidateEntity(newTest);
            if (result.IsValid == false)
            {
                throw new InvalidTestException(result.ValidationResults);
            }

            var newTestDto = _testDtoMapper.Map(newTest);

            await _testsRepository.CreateTestAsync(newTestDto);
        }

        public async Task AssignTestToGroupAsync(IAssignedTestModel assignedTest)
        {
            if (assignedTest == null)
            {
                throw new ArgumentNullException("assignedTest");
            }

            var result = _entityValidator.ValidateEntity(assignedTest);
            if (result != null)
            {
                throw new InvalidAssignedTestException(result.ValidationResults);
            }

            var assignmentDto = _testDtoMapper.Map(assignedTest);

            await _testsRepository.AssignTestAsync(assignmentDto);
        }

        
    }
}
