using System.Data.Entity;
using EasyTeach.Core.Entities;

namespace EasyTeach.Data.Context
{
    public class EasyTeachContext : DbContext
    {
        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Group> Groups { get; set; }
    }
}
