using System.Linq;
using System.Security.Claims;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Enums;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EasyTeachContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "EasyTeach.Data.Context.EasyTeachContext";
        }

        protected override void Seed(EasyTeachContext context)
        {
            context.Groups.AddOrUpdate(new Group
            {
                GroupId = 1,
                GroupNumber = 1,
                Year = 1953
            });

            context.SaveChanges();

            context.Users.AddOrUpdate(
            // password: testMoBiLe13
            new UserDto
            {
                UserId = 2,
                Email = "test@hiqo-solutions.com",
                EmailIsValidated = true,
                FirstName = "Леонид Сергеевич",
                LastName = "Броневой",
                PasswordHash = "ALTARCbT6yTe7DSX5LSHgKhBB0t2cR+OPUabn0vmCEmxPBIVT/jb9r64jRVfvpwD5A==",
                UserType = UserType.Student,
                Group = context.Groups.Single(g => g.GroupId == 1)
            },

            // password: testMoBiLe13
            new UserDto
            {
                UserId = 1,
                Email = "svetlana.panina@hiqo-solutions.com",
                EmailIsValidated = true,
                FirstName = "Светлана",
                LastName = "Панина",
                PasswordHash = "ALTARCbT6yTe7DSX5LSHgKhBB0t2cR+OPUabn0vmCEmxPBIVT/jb9r64jRVfvpwD5A==",
                UserType = UserType.Teacher,
                Group = null
            });

            context.SaveChanges();

            context.UserClaims.AddOrUpdate(new UserClaimDto
            {
                UserClaimId = 1,
                Value = "Register",
                Type = "User",
                ValueType = ClaimValueTypes.String,
                User = context.Users.Single(u => u.UserId == 1)
            });

            context.SaveChanges();
        }
    }
}