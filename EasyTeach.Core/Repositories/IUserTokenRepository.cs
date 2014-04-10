﻿using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Core.Repositories
{
    public interface IUserTokenRepository
    {
        Task CreateAsync(string purpose, string token, int userId);

        Task<IUserTokenDto> GetUserTokenAsync(string purpose, string token, int userId);
    }
}