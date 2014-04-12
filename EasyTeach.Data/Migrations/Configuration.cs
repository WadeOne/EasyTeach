using EasyTeach.Data.Context;

namespace EasyTeach.Data.Migrations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EasyTeachContext>
    {
        public Configuration()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EasyTeachContext>());
            AutomaticMigrationsEnabled = false;
            ContextKey = "EasyTeach.Data.Context.EasyTeachContext";
        }

        protected override void Seed(EasyTeach.Data.Context.EasyTeachContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
