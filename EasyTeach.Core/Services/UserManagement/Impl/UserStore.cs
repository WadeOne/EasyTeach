using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class UserStore : IUserPasswordStore<IUserDto, int>, IUserEmailStore<IUserDto, int>, IUserClaimStore<IUserDto, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly IUserClaimDtoMapper _userClaimDtoMapper;

        public UserStore(IUserRepository userRepository, IUserClaimRepository userClaimRepository, IUserClaimDtoMapper userClaimDtoMapper)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            if (userClaimRepository == null)
            {
                throw new ArgumentNullException("userClaimRepository");
            }

            if (userClaimDtoMapper == null)
            {
                throw new ArgumentNullException("userClaimDtoMapper");
            }

            _userRepository = userRepository;
            _userClaimRepository = userClaimRepository;
            _userClaimDtoMapper = userClaimDtoMapper;
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

        public async Task<IUserDto> FindByIdAsync(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<IUserDto> FindByNameAsync(string userName)
        {
            return await FindByEmailAsync(userName);
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

        public Task SetEmailAsync(IUserDto user, string email)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            user.Email = email;

            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.EmailIsValidated);
        }

        public Task SetEmailConfirmedAsync(IUserDto user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.EmailIsValidated = confirmed;

            return Task.FromResult(0);
        }

        public async Task<IUserDto> FindByEmailAsync(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            return await _userRepository.GetUserByEmail(email);
        }

        public Task<IList<Claim>> GetClaimsAsync(IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var claims = _userClaimRepository.GetUserClaims(user.UserId)
                .AsEnumerable()
                .Select(c => new Claim(c.Type, c.Value, c.ValueType))
                .ToList();

            return Task.FromResult((IList<Claim>)claims);
        }

        public Task AddClaimAsync(IUserDto user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            return _userClaimRepository.AddClaimAsync(_userClaimDtoMapper.Map(user, claim));
        }

        public Task RemoveClaimAsync(IUserDto user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            return _userClaimRepository.RemoveClaimAsync(_userClaimDtoMapper.Map(user, claim));
        }
    }
}