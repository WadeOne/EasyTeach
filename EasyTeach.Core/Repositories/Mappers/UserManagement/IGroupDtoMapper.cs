using EasyTeach.Core.Entities.Data.Group;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories.Mappers.UserManagement
{
    public interface IGroupDtoMapper
    {
        IGroupDto Map(IGroupModel group);
    }
}