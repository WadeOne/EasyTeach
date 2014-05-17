using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data.Group;

namespace EasyTeach.Core.Repositories
{
    public interface IGroupRepository
    {
        Task CreateGroupAsync(IGroupDto group);

        Task RemoveGroupAsync(int groupId);

        Task UpdateGroupAsync(IGroupDto group);

        IQueryable<IGroupDto> GetGroups();

        Task<IGroupDto> GetGroupByIdAsync(int groupId);
    }
}