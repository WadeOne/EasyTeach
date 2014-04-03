using System.Data.Entity;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Context
{
    public class EasyTeachContext : DbContext
    {
        public virtual IDbSet<UserDto> Users { get; set; }

        public virtual IDbSet<Group> Groups { get; set; }
    }
}
