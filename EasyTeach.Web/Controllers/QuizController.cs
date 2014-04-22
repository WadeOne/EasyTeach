using System;
using System.Threading.Tasks;
using System.Web.Http;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Quiz;

namespace EasyTeach.Web.Controllers
{
    public class QuizController : ApiController
    {
        private readonly IQuizManagementService _quizManagementService;

        public QuizController(IQuizManagementService quizManagementService)
        {
            _quizManagementService = quizManagementService;
        }

        public async Task<IHttpActionResult> CreateTest(CreateQuizViewModel newQuizViewModel)
        {
            IQuizModel quizModel = newQuizViewModel.ToQuizModel();
            var createdQuiz = await _quizManagementService.CreateQuizAsync(quizModel);
            return Ok(createdQuiz);
        }
    }

    public class CreateQuizViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public QuizModel ToQuizModel()
        {
            return new QuizModel { Description = Description, Name = Name };
        }
    }
}