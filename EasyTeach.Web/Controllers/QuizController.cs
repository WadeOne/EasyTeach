using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Quiz;
using EasyTeach.Core.Services.Quiz.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;
using EasyTeach.Web.Models.ViewModels;

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

            IQuizModel quizModel = newQuizViewModel.ToQuizModel();
            IQuizModel createdQuiz;
            try
            {
                createdQuiz = await _quizManagementService.CreateQuizAsync(quizModel);
            }
            catch (InvalidQuizException exception)
            {
                foreach (var validationResult in exception.ValidationResults)
                {
                    ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault(), validationResult.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            return Ok(createdQuiz);
        }

        public Task<IHttpActionResult> AddQuestion(AddQuestionToQuizViewModel questionToQuizViewModel)
        {
            throw new NotImplementedException();
        }
    }

    public class AddQuestionToQuizViewModel
    {
        public int QuizId { get; set; }

        public QuestionViewModel Question { get; set; }
    }

    public class QuestionViewModel
    {
        public virtual Question ToQuestion()
        {
            throw new NotImplementedException();
        }
    }
}