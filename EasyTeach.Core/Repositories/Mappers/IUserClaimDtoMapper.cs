using System.Security.Claims;
using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Core.Repositories.Mappers
{
    public interface IUserClaimDtoMapper
    {
        IUserClaimDto Map(IUserDto userDto, Claim claim);
    }
}