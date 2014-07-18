using System;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Permissions;
using System.Web.Http;
using EasyTeach.Core.Services.Dashboard;
using EasyTeach.Web.Models.ViewModels.Dashboard.Lessons;

namespace EasyTeach.Web.Controllers
{
    [RoutePrefix("api/Lesson")]
    public sealed class LessonController : ApiControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            if (lessonService == null)
            {
                throw new ArgumentNullException("lessonService");
            }

            _lessonService = lessonService;
        }

        [Route("")]
        public IQueryable<LessonViewModel> Get()
        {
            return _lessonService.GetLessons().Select(l => new LessonViewModel
            {
                Date = l.Date,
                GroupId = l.Group.GroupId,
                LessonId = l.LessonId
            });
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