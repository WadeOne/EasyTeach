using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.Quiz;
using EasyTeach.Core.Services.Quiz.Exceptions;
using EasyTeach.Web.Models;
using EasyTeach.Web.Models.ViewModels;
using EasyTeach.Web.Results;

namespace EasyTeach.Web.Controllers
{
    public class QuizController : ApiControllerBase
    {
        private readonly IQuizManagementService _quizManagementService;

        public QuizController(IQuizManagementService quizManagementService)
        {
            if (quizManagementService == null)
            {
                throw new ArgumentNullException("quizManagementService");
            }

            _quizManagementService = quizManagementService;
        }

        public async Task<IHttpActionResult> Create(CreateQuizViewModel newQuizViewModel)
        {
            if (newQuizViewModel == null)
            {
                throw new ArgumentNullException("newQuizViewModel");
            }

            IQuizModel quizModel = newQuizViewModel.ToQuiz();
            IQuizModel createdQuiz;
            try
            {
                createdQuiz = await _quizManagementService.CreateQuizAsync(quizModel);
            }
            catch (InvalidQuizException exception)
            {
                return BadRequestWithModelState(exception);
            }
            return Ok(createdQuiz);
        }
        
        public async Task<IHttpActionResult> AddQuestion(AddQuestionToQuizViewModel questionToQuizViewModel)
        {
            if (questionToQuizViewModel == null)
            {
                throw new ArgumentNullException("questionToQuizViewModel");
            }
            try
            {
                var questionModel = questionToQuizViewModel.Question.ToQuestion();
                await _quizManagementService.AddQuestionToQuiz(questionToQuizViewModel.QuizId, questionModel);
            }
            catch (InvalidAddQuestionException exception)
            {
                return BadRequestWithModelState(exception);
            }
            return Ok();
        }

        private IHttpActionResult BadRequestWithModelState(ModelValidationException exception)
        {
            foreach (var validationResult in exception.ValidationResults)
            {
                ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault(), validationResult.ErrorMessage);
            }
            return BadRequest(ModelState);
        }
    }
}