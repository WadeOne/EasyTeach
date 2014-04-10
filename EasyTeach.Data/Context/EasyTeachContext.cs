using System.Data.Entity;
using EasyTeach.Core.Entities;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Context
{
    public class EasyTeachContext : DbContext
    {
        public virtual IDbSet<UserDto> Users { get; set; }

        public virtual IDbSet<Group> Groups { get; set; }

        public virtual IDbSet<UserTokenDto> UserTokens { get; set; }
    }
}
