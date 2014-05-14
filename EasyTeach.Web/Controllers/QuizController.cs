using System;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Permissions;
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
    [RoutePrefix("api/Quiz")]
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

        [Route("")]
        [HttpPost]
        //[ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "Create", Resource = "Quiz")]
        public async Task<IHttpActionResult> Post(CreateQuizViewModel newQuizViewModel)
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