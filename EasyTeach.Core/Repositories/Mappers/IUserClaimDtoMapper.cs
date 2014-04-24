using System.Security.Claims;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.User;

namespace EasyTeach.Core.Repositories.Mappers
{
    public interface IUserClaimDtoMapper
    {
        IUserClaimDto Map(IUserDto userDto, Claim claim);
    }
}