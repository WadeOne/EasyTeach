using EasyTeach.Core.Entities;
using EasyTeach.Core.Interfaces.Repositories;
using EasyTeach.Data.Context;

namespace EasyTeach.Data.Repostitories
{
    public class UserRepository : IUserRepository
    {
        private EasyTeachContext dbContext;

        public void SaveUser(User newUser)
        {
            throw new System.NotImplementedException();
        }
    }
}
