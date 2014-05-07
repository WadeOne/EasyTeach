using System;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTeach.Core.Services.Dashboard;
using EasyTeach.Web.Models.ViewModels.Dashboard;

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
        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "Create", Resource = "Lesson")]
        public async Task<IHttpActionResult> Post(CreateLessonViewModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            await _lessonService.CreateLessonAsync(lesson.ToLesson());

            return Ok();
        }

        [Route("")]
        [HttpPut]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "Update", Resource = "Lesson")]
        public async Task<IHttpActionResult> Put(UpdateLessonViewModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            await _lessonService.UpdateLessonAsync(lesson.ToLesson());

            return Ok();
        }

        [Route("")]
        [HttpDelete]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "Delete", Resource = "Lesson")]
        public async Task<IHttpActionResult> Delete(int lessonId)
        {
            await _lessonService.RemoveLessonAsync(lessonId);

            return Ok();
        }
    }
}