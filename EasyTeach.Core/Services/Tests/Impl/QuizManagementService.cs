﻿using System;
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
    public class QuizManagementService : IQuizManagementService
    {
        private readonly IQuizRepository _quizRepository;

        private readonly IQuizDtoMapper _quizDtoMapper;

        private readonly EntityValidator _entityValidator;

        public QuizManagementService(IQuizRepository quizRepository, IQuizDtoMapper quizDtoMapper, EntityValidator entityValidator)
        {
            if (quizRepository == null)
            {
                throw new ArgumentNullException("quizRepository");
            }

            if (quizDtoMapper == null)
            {
                throw new ArgumentNullException("quizDtoMapper");
            }

            if (entityValidator == null)
            {
                throw new ArgumentNullException("entityValidator");
            }

            _quizRepository = quizRepository;
            _quizDtoMapper = quizDtoMapper;
            _entityValidator = entityValidator;
        }

        public async Task CreateTestAsync(IQuizModel newQuiz)
        {
            if (newQuiz == null)
            {
                throw new ArgumentNullException("newQuiz");
            }

            var result = _entityValidator.ValidateEntity(newQuiz);
            if (result.IsValid == false)
            {
                throw new InvalidTestException(result.ValidationResults);
            }

            var newTestDto = _quizDtoMapper.Map(newQuiz);

            await _quizRepository.CreateTestAsync(newTestDto);
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

            var assignmentDto = _quizDtoMapper.Map(assignedTest);

            await _quizRepository.AssignTestAsync(assignmentDto);
        }

        
    }
}