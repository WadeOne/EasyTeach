using System;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class UserTokenProvider : IUserTokenProvider<IUserDto, int>
    {
        private static readonly TimeSpan ExpirationTokenPeriod = new TimeSpan(14, 0, 0, 0);
        private readonly IUserTokenRepository _userTokenRepository;

        public UserTokenProvider(IUserTokenRepository userTokenRepository)
        {
            if (userTokenRepository == null)
            {
                throw new ArgumentNullException("userTokenRepository");
            }

            _userTokenRepository = userTokenRepository;
        }

        public Task<string> GenerateAsync(string purpose, UserManager<IUserDto, int> manager, IUserDto user)
        {
            if (String.IsNullOrWhiteSpace(purpose))
            {
                throw new ArgumentNullException("purpose");
            }

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            string token = GenerateToken();

            _userTokenRepository.CreateAsync(purpose, token, user.UserId);

            return Task.FromResult(token);
        }

        public async Task<bool> ValidateAsync(string purpose, string token, UserManager<IUserDto, int> manager, IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            IUserTokenDto userToken = await _userTokenRepository.GetUserTokenAsync(purpose, token, user.UserId);

            if (userToken == null)
            {
                return false;
            }

            return DateTime.UtcNow - userToken.Created < ExpirationTokenPeriod;
        }

        public Task NotifyAsync(string token, UserManager<IUserDto, int> manager, IUserDto user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> IsValidProviderForUserAsync(UserManager<IUserDto, int> manager, IUserDto user)
        {
            return Task.FromResult(true);
        }

        private static string GenerateToken()
        {
            long i = Guid.NewGuid().ToByteArray().Aggregate<byte, long>(1, (current, b) => current*((int) b + 1));
            return string.Format("{0:x}", i - DateTime.UtcNow.Ticks);
        }
    }
}