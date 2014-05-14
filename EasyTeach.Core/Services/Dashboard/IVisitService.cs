using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.Dashboard
{
    public interface IVisitService
    {
        IQueryable<IVisitModel> GetVisits(int groupId);

        Task UpdateVisitAsync(IVisitModel visit);
    }
}