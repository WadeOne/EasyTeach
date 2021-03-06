﻿using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories.Mappers.UserManagement
{
    public interface IUserDtoMapper
    {
        IUserDto Map(IUserModel userModel);

        IUserDto Map(IUserIdentityModel userIdentityModel);
    }
}