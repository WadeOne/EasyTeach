using System;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly EasyTeachContext _context;

        public UserRepository(EasyTeachContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public async Task<IUserDto> GetUserByEmail(string email)
        {
            // Fix problem with async/await non thread safe calling
            return await Task.FromResult((IUserDto) _context.Users.SingleOrDefault(u => u.Email == email));
        }

        public async Task<IUserDto> GetUserById(int userId)
        {
            // Fix problem with async/await non thread safe calling
            return await Task.FromResult((IUserDto)_context.Users.SingleOrDefault(u => u.UserId == userId));
        }

        public async Task CreateAsync(IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _context.Users.Add((UserDto)user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            await _context.SaveChangesAsync();
        }
    }
}
