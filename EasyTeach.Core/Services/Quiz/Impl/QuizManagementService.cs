using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers.QuizManagement;
using EasyTeach.Core.Services.Quiz.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;

namespace EasyTeach.Core.Services.Quiz.Impl
{
    public class QuizManagementService : IQuizManagementService
    {
        private readonly IQuizRepository _quizRepository;

        private readonly IQuizDtoMapper _quizDtoMapper;

        private readonly EntityValidator _entityValidator;

        public readonly IQuestionDtoMapper _questionDtoMapper;

        public QuizManagementService(IQuizRepository quizRepository, IQuizDtoMapper quizDtoMapper, EntityValidator entityValidator, IQuestionDtoMapper questionDtoMapper)
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

            if (questionDtoMapper == null)
            {
                throw new ArgumentNullException("questionDtoMapper");
            }

            _quizRepository = quizRepository;
            _quizDtoMapper = quizDtoMapper;
            _entityValidator = entityValidator;
            _questionDtoMapper = questionDtoMapper;
        }

        public async Task<IQuizModel> CreateQuizAsync(IQuizModel newQuiz)
        {
            if (newQuiz == null)
            {
                throw new ArgumentNullException("newQuiz");
            }

            var result = _entityValidator.ValidateEntity(newQuiz);
            if (result.IsValid == false)
            {
                throw new InvalidQuizException(result.ValidationResults);
            }

            var newQuizDto = _quizDtoMapper.Map(newQuiz);

            await _quizRepository.CreateQuizAsync(newQuizDto);

            return _quizDtoMapper.Map(newQuizDto);
        }

        public async Task AssignQuizToGroupAsync(IAssignedQuizModel assignedQuiz)
        {
            if (assignedQuiz == null)
            {
                throw new ArgumentNullException("assignedQuiz");
            }

            if (assignedQuiz.EndDate < assignedQuiz.StartDate)
            {
                throw new InvalidAssignedTestException(new List<ValidationResult>
                {
                    new ValidationResult("Quiz can't be finished before it starts", new[] {"StartDate", "EndDate"})
                });
            }

            var result = _entityValidator.ValidateEntity(assignedQuiz);
            if (result.IsValid == false)
            {
                throw new InvalidAssignedTestException(result.ValidationResults);
            }

            var assignmentDto = _quizDtoMapper.Map(assignedQuiz);

            await _quizRepository.AssignQuizAsync(assignmentDto);
        }

        public async Task AddQuestionToQuizAsync(int quizId, IQuestionModel question)
        {
            if (question == null)
            {
                throw new ArgumentNullException("question");
            }
           
            //TODO add validation for multiple solutions
            IQuizDto quizDto = await _quizRepository.GetQuiz(quizId);
            if (quizDto == null)
            {
                throw new InvalidAddQuestionException(new List<ValidationResult>
                                                      {
                                                          new ValidationResult(string.Format("Quiz with Id {0} doesn't exist", quizId), new [] {"QuizId"})
                                                      });
            }
            EntityValidationResult result = _entityValidator.ValidateEntity(question);
            if (result.IsValid == false)
            {
                throw new InvalidAddQuestionException(result.ValidationResults);
            }

            var questionDto = _questionDtoMapper.Map(question);
            await _quizRepository.AddQuestionToQuiz(quizId, questionDto);
        }

        public async Task<IEnumerable<IQuizModel>> GetAllQuizes()
        {
            var quizes = await _quizRepository.GetAllQuizes();

            return quizes.Select(_quizDtoMapper.Map);
        }

        public async Task<IQuizModel> GetQuiz(int quizId)
        {
            IQuizDto quizDto = await _quizRepository.GetQuiz(quizId);
            if (quizDto == null)
            {
                throw new InvalidQuizException(new List<ValidationResult>
                {
                    new ValidationResult(string.Format("Quiz with id {0} doesn't exist", quizId), new[] {"QuizId"})
                });
            }
            IQuizModel quiz = _quizDtoMapper.Map(quizDto);
            return quiz;
        }
    }
}
