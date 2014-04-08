using System;
using System.Data.Entity;
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
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public void Dispose()
        {
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

        public Task UpdateAsync(IUserDto user)
        {
            throw new NotImplementedException();
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
            return GetUserByEmail(userName);
        }
    }
}
