using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.Dashboard
{
    public interface IVisitService
    {
        IQueryable<IVisitModel> GetGroupVisits(int groupId);

        IQueryable<IVisitModel> GetGroupVisitsAvailableForStudent(IPrincipal principal);

        Task UpdateVisitAsync(IVisitModel visit);
    }
}