using AutoMapper;

using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers;

namespace EasyTeach.Data.Repostitories.Mappers
{
    public class UserDtoMapper : IUserDtoMapper
    {
        public UserDtoMapper()
        {
            Mapper.CreateMap<IUserModel, IUserDto>();
        }

        public IUserDto Map(IUserModel userModel)
        {
            return Mapper.Map<IUserDto>(userModel);
        }
    }
}
