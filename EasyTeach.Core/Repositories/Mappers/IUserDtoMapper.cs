using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories.Mappers
{
    public interface IUserDtoMapper
    {
        IUserDto Map(IUserModel userModel);

        IUserDto Map(IUserIdentityModel userIdentityModel);
    }
}