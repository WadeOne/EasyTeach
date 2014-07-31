using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Dashboard;
using EasyTeach.Web.Models.ViewModels.Dashboard.Lessons;

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
                LessonId = l.LessonId,
                GroupDisplayName = l.Group.GroupNumber + " (" + l.Group.Year + ")"
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
        
        public IHttpActionResult Put([FromODataUri] int key, LessonViewModel lessonView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!_lessonService.GetLessons().Any(l => l.LessonId == key))
            {
                return BadRequest();
            }

            var update = lessonView.ToLesson();
            _lessonService.UpdateLesson(update);

            return Updated(MapLessonToView(update));
        }

        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ILessonModel lesson = _lessonService.GetLessons().FirstOrDefault(l => l.LessonId == key);

            if (lesson == null)
            {
                return NotFound();
            }

            _lessonService.RemoveLesson(key);

            return StatusCode(HttpStatusCode.NoContent);
        }

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