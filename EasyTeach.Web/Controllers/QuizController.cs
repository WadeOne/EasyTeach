using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.Quiz;
using EasyTeach.Core.Services.Quiz.Exceptions;
using EasyTeach.Web.Models.ViewModels.Quiz;

//TODO Add claims
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

        [Route("")]
        [HttpPut]
        public Task<IHttpActionResult> Put(UpdateQuizViewModel updateQuizViewModel)
        {
            throw new NotImplementedException();
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

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<QuizListingViewModel>> Get()
        {
            var quizes = await _quizManagementService.GetAllQuizes();

            return
                quizes.Select(
                    x => new QuizListingViewModel { QuizId = x.QuizId, Name = x.Name, Description = x.Description });
        }

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int quizId)
        {
            try
            {
                IQuizModel quiz = await _quizManagementService.GetQuiz(quizId);
                return Ok(new EditQuizViewModel {Description = quiz.Description, Name = quiz.Name, QuizId = quiz.QuizId, IsReadOnly = quiz.IsDeprecated});
            }
            catch (InvalidQuizException exception)
            {
                return BadRequestWithModelState(exception);
            }
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