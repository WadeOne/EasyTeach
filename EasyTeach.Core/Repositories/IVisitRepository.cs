using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data.Dashboard;

namespace EasyTeach.Core.Repositories
{
    public interface IVisitRepository
    {
        IQueryable<IVisitDto> GetAll();

        void CreateVisit(IVisitDto visit);

        Task UpdateVisitAsync(IVisitDto visit);

        Task<IVisitDto> GetVisitAsync(int userId, int lessonId);
    }
}