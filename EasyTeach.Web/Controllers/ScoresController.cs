using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using EasyTeach.Core.Services.Dashboard;
using EasyTeach.Web.Models.ViewModels.Dashboard.Scores;

namespace EasyTeach.Web.Controllers
{
    public class ScoresController : ODataController
    {
        private readonly IScoreService _scoreService;

        public ScoresController(IScoreService scoreService)
        {
            if (scoreService == null)
            {
                throw new ArgumentNullException("scoreService");
            }

            _scoreService = scoreService;
        }

        [HttpGet]
        public IQueryable<ScoreViewModel> Get(ODataQueryOptions<ScoreViewModel> queryOptions)
        {
            var result = _scoreService.GetScores().Select(s => new ScoreViewModel
            {
                ScoreId = s.ScoreId,
                Score = s.Score,
                AssignedToId = s.AssignedTo.UserId,
                AssignedById = s.AssignedBy.UserId,
                AssignedTo = s.AssignedTo.FirstName + " " + s.AssignedTo.LastName,
                AssignedBy = s.AssignedBy.FirstName + " " + s.AssignedBy.LastName,
                VisitId = s.Visit.VisitId,
                DisplayData = s.Visit.Lesson.Date
            });
            var filteredResult = ((IQueryable<ScoreViewModel>)queryOptions.ApplyTo(result));
            return filteredResult;
        }
        
        /*
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
        [HttpPut]
        public IHttpActionResult Put(UpdateScoreViewModel score)
        {
            if (score == null)
            {
                throw new ArgumentNullException("score");
            }

            _scoreService.UpdateScore(score.ToScore());

            return Ok();
        }

        [Route("")]
        [HttpDelete]
        public IHttpActionResult Delete(int scoreId)
        {
            _scoreService.DeleteScore(scoreId);

            return Ok();
        }
         * */
    }
}
