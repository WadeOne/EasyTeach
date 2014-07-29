using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Mvc;
using EasyTeach.Core.Entities.Services;
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

        public IQueryable<LessonViewModel> Get(ODataQueryOptions<LessonViewModel> queryOptions)
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
        public IHttpActionResult Post(LessonViewModel groupView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var create = groupView.ToLesson();
            _lessonService.CreateLesson(create);

            return Created(MapLessonToView(create));
        }
        /*
        [System.Web.Http.Route("")]
        [System.Web.Http.HttpPut]
        public IHttpActionResult Put(UpdateLessonViewModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            _lessonService.UpdateLesson(lesson.ToLesson());

            return Ok();
        }

        [System.Web.Http.Route("")]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(int lessonId)
        {
            _lessonService.RemoveLesson(lessonId);

            return Ok();
        }
         * */

        private static LessonViewModel MapLessonToView(ILessonModel l)
        {
            return new LessonViewModel()
            {
                LessonId = l.LessonId,
                GroupId = l.Group.GroupId,
                Date = l.Date
            };
        }
    }
}