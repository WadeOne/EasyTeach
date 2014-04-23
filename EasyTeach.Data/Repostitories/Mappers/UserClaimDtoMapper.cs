using System.Security.Claims;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers
{
    public sealed class UserClaimDtoMapper : IUserClaimDtoMapper
    {
        public IUserClaimDto Map(IUserDto userDto, Claim claim)
        {
            return new UserClaimDto
            {
                Type = claim.Type,
                Value = claim.Value,
                ValueType = claim.ValueType,
                User = (UserDto)userDto
            };
        }
    }
}