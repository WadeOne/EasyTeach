using System;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Repositories;
using EasyTeach.Data.Context;

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

        public void SaveUser(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
