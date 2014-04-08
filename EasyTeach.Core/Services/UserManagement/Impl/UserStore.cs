using System;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class UserStore : IUserPasswordStore<IUserDto, int>
    {
        private readonly IUserRepository _userRepository;

        public UserStore(IUserRepository userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            _userRepository = userRepository;
        }

        public void Dispose()
        {
        }

        public Task CreateAsync(IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return _userRepository.CreateAsync(user);
        }

        public Task UpdateAsync(IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return _userRepository.UpdateAsync(user);
        }

        public Task DeleteAsync(IUserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<IUserDto> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IUserDto> FindByNameAsync(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }

            return _userRepository.GetUserByEmail(userName);
        }

        public Task SetPasswordHashAsync(IUserDto user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PasswordHash != null);
        }
    }
}