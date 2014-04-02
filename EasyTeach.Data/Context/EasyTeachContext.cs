﻿using System.Data.Entity;

using EasyTeach.Core.Entities;

namespace EasyTeach.Data.Context
{
    internal class EasyTeachContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }
    }
}