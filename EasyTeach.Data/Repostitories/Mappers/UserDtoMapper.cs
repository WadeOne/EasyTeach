using AutoMapper;

using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers
{
    public class UserDtoMapper : IUserDtoMapper
    {
        public UserDtoMapper()
        {
            Mapper.CreateMap<IUserModel, UserDto>();
        }

        public IUserDto Map(IUserModel userModel)
        {
            return Mapper.Map<UserDto>(userModel);
        }

        public IUserDto Map(IUserIdentityModel userIdentityModel)
        {
            return new UserDto
            {
                UserId = userIdentityModel.UserId,
                Email = userIdentityModel.Email
            };
        }
    }
}
