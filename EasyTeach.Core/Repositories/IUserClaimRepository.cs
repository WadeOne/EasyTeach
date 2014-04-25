using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.User;

namespace EasyTeach.Core.Repositories
{
    public interface IUserClaimRepository
    {
        IQueryable<IUserClaimDto> GetUserClaims(int userId);

        Task AddClaimAsync(IUserClaimDto claim);

        Task RemoveClaimAsync(IUserClaimDto claim);
    }
}