using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories
{
    public sealed class UserTokenRepository : IUserTokenRepository
    {
        private readonly EasyTeachContext _context;

        public UserTokenRepository(EasyTeachContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public async Task CreateAsync(string purpose, string token, int userId)
        {
            if (String.IsNullOrWhiteSpace(purpose))
            {
                throw new ArgumentNullException("purpose");
            }

            if (String.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException("token");
            }

            _context.UserTokens.Add(new UserTokenDto
            {
                Created = DateTime.UtcNow,
                Puprose = purpose,
                Token = token,
                UserId = userId
            });

            await _context.SaveChangesAsync();
        }

        public async Task<IUserTokenDto> GetUserTokenAsync(string purpose, string token, int userId)
        {
            return await _context.UserTokens.SingleOrDefaultAsync(t => t.Puprose == purpose && t.Token == token && t.UserId == userId);
        }
    }
}