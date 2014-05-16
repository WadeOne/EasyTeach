using System;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTeach.Core.Services.Dashboard;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using EasyTeach.Web.Models.ViewModels.Dashboard.Visits;

namespace EasyTeach.Web.Controllers
{
    [RoutePrefix("api/Visit")]
    public sealed class VisitController : ApiControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            if (visitService == null)
            {
                throw new ArgumentNullException("visitService");
            }

            _visitService = visitService;
        }

        [Route("")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "GetAll", Resource = "Visit")]
        public IQueryable<VisitViewModel> Get(int groupId)
        {
            return _visitService.GetGroupVisits(groupId).Select(v => new VisitViewModel
            {
                LessonId = v.Lesson.LessonId,
                Note = v.Note,
                Status = v.Status,
                VisitorId = v.Visitor.UserId
            });
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_visitService.GetGroupVisitsAvailableForStudent(User).Select(v => new VisitViewModel
                {
                    LessonId = v.Lesson.LessonId,
                    Note = v.Note,
                    Status = v.Status,
                    VisitorId = v.Visitor.UserId
                }));
            }
            catch (GroupNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("")]
        [HttpPut]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "Update", Resource = "Visit")]
        public async Task<IHttpActionResult> Put(VisitViewModel visit)
        {
            if (visit == null)
            {
                throw new ArgumentNullException("visit");
            }

            await _visitService.UpdateVisitAsync(visit.ToVisit());

            return Ok();
        }
    }
}