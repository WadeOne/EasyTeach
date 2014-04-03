using System;
using System.Linq;

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

        public IUserDto GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public void SaveUser(IUserDto newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }

            _context.Users.Add((UserDto)newUser);
            _context.SaveChanges();
        }
    }
}
