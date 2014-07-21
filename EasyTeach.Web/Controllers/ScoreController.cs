using System;
using System.Linq;
using System.Web.Http;
using EasyTeach.Core.Services.Dashboard;
using EasyTeach.Web.Models.ViewModels.Dashboard.Scores;

namespace EasyTeach.Web.Controllers
{

    [RoutePrefix("api/Score")]
    public class ScoreController : ApiControllerBase
    {
        private readonly IScoreService _scoreService;

        public ScoreController(IScoreService scoreService)
        {
            if (scoreService == null)
            {
                throw new ArgumentNullException("scoreService");
            }

            _scoreService = scoreService;
        }

        [Route("")]
        [HttpGet]
        public IQueryable<ScoreViewModel> Get()
        {
            return _scoreService.GetScores().Select(s => new ScoreViewModel
            {
                ScoreId = s.ScoreId,
                Score = s.Score,
                AssignedToId = s.AssignedTo.UserId,
                AssignedById = s.AssignedBy.UserId,
                Task = s.Task,
                //VisitId = s.Visit.
            });
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(CreateScoreViewModel score)
        {
            if (score == null)
            {
                throw new ArgumentNullException("score");
            }

            _scoreService.AddScore(score.ToScore());

            return Ok();
        }

        [Route("")]
        [HttpDelete]
        public IHttpActionResult Delete(int scoreId)
        {
            _scoreService.DeleteScore(scoreId);

            return Ok();
        }
    }
}
