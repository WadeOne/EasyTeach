using System.Security.Claims;

using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Repositories.Mappers.UserManagement;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers.UserManagement
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