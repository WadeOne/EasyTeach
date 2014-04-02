using EasyTeach.Core.Entities;
using EasyTeach.Core.Repositories;
using EasyTeach.Data.Context;

namespace EasyTeach.Data.Repostitories
{
    public sealed class UserRepository : IUserRepository
    {
        private EasyTeachContext dbContext;

        public void SaveUser(User newUser)
        {
            throw new System.NotImplementedException();
        }
    }
}
