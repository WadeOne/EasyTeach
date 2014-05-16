using System.Security.Claims;

using EasyTeach.Core.Entities.Data.User;

namespace EasyTeach.Core.Repositories.Mappers.UserManagement
{
    public interface IUserClaimDtoMapper
    {
        IUserClaimDto Map(IUserDto userDto, Claim claim);
    }
}