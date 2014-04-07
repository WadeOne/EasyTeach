using EasyTeach.Core.Entities.Data;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Repositories
{
    public interface IUserRepository : IUserStore<IUserDto, int>
    {
        IUserDto GetUserByEmail(string email);
    }
}