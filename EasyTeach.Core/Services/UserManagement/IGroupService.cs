using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.UserManagement
{
    public interface IGroupService
    {
        IQueryable<IGroupModel> GetAll();

        Task CreateGroupAsync(IGroupModel groupModel);

        Task UpdateGroupAsync(IGroupModel groupModel);

        Task DeleteGroupAsync(int groupId);
    }
}