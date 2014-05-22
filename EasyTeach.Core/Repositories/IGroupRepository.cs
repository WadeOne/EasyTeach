using System.Linq;
using EasyTeach.Core.Entities.Data.Group;

namespace EasyTeach.Core.Repositories
{
    public interface IGroupRepository
    {
        void CreateGroup(IGroupDto group);

        void RemoveGroup(int groupId);

        void UpdateGroup(IGroupDto group);

        IQueryable<IGroupDto> GetGroups();

        IGroupDto GetGroupById(int groupId);
    }
}