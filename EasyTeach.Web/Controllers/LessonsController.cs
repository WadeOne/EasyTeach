using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using EasyTeach.Core.Services.Dashboard;
using EasyTeach.Web.Models.ViewModels.Dashboard.Lessons;
using EasyTeach.Web.Models.ViewModels.Groups;

namespace EasyTeach.Web.Controllers
{
    public sealed class LessonsController : ODataController
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            if (lessonService == null)
            {
                throw new ArgumentNullException("lessonService");
            }

            _lessonService = lessonService;
        }

        public IQueryable<LessonViewModel> Get(ODataQueryOptions<GroupViewModel> queryOptions)
        {
            var result =  _lessonService.GetLessons().Select(l => new LessonViewModel
            {
                Date = l.Date,
                GroupId = l.Group.GroupId,
                LessonId = l.LessonId
            });

            var filteredResult = ((IQueryable<LessonViewModel>)queryOptions.ApplyTo(result));
            return filteredResult;
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(CreateLessonViewModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            _lessonService.CreateLesson(lesson.ToLesson());

            return Ok();
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(UpdateLessonViewModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            _lessonService.UpdateLesson(lesson.ToLesson());

            return Ok();
        }

        [Route("")]
        [HttpDelete]
        public IHttpActionResult Delete(int lessonId)
        {
            _lessonService.RemoveLesson(lessonId);

            return Ok();
        }
    }
}