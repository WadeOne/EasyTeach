using System;
using System.Linq;
using System.Security.Claims;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EasyTeachContext>
    {
        private int _claimId;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "EasyTeach.Data.Context.EasyTeachContext";
        }

        protected override void Seed(EasyTeachContext context)
        {
            context.Groups.AddOrUpdate(
                g => g.GroupId,
                new GroupDto
                {
                    GroupId = 1,
                    GroupNumber = 1,
                    Year = 2010,
                    ContactEmail = "example@mail.com",
                    ContactName = "John Doe",
                    ContactPhone = "+37529123456"
                },
                new GroupDto
                {
                    GroupId = 2,
                    GroupNumber = 2,
                    Year = 2011,
                    ContactEmail = "example@mail.com",
                    ContactName = "Jaen Doe",
                    ContactPhone = "+37529123456"
                }
                );
            context.SaveChanges();

            context.Lessons.AddOrUpdate(
                l => l.LessonId,
                new LessonDto
                {
                    LessonId = 1,
                    GroupId = 1,
                    Date = new DateTime(2014, 9, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new LessonDto
                {
                    LessonId = 2,
                    GroupId = 1,
                    Date = new DateTime(2014, 9, 8, 0, 0, 0, DateTimeKind.Utc)
                },
                new LessonDto
                {
                    LessonId = 3,
                    GroupId = 1,
                    Date = new DateTime(2014, 9, 15, 0, 0, 0, DateTimeKind.Utc)
                },
                new LessonDto
                {
                    LessonId = 4,
                    GroupId = 1,
                    Date = new DateTime(2014, 9, 22, 0, 0, 0, DateTimeKind.Utc)
                }
                );
            context.SaveChanges();



            context.Users.AddOrUpdate(
                // password: testMoBiLe13
            new UserDto
            {
                UserId = 3,
                Email = "sample@hiqo-solutions.com",
                EmailIsValidated = true,
                FirstName = "Styopa",
                LastName = "Kolpachkov",
                PasswordHash = "ALTARCbT6yTe7DSX5LSHgKhBB0t2cR+OPUabn0vmCEmxPBIVT/jb9r64jRVfvpwD5A==",
                GroupId = 1
            },

                // password: testMoBiLe13
            new UserDto
            {
                UserId = 2,
                Email = "test@hiqo-solutions.com",
                EmailIsValidated = true,
                FirstName = "Leoned",
                LastName = "Bronevoy",
                PasswordHash = "ALTARCbT6yTe7DSX5LSHgKhBB0t2cR+OPUabn0vmCEmxPBIVT/jb9r64jRVfvpwD5A==",
                GroupId = 1
            },

            // password: testMoBiLe13
            new UserDto
            {
                UserId = 1,
                Email = "svetlana.panina@hiqo-solutions.com",
                EmailIsValidated = true,
                FirstName = "Svetlana",
                LastName = "Panina",
                PasswordHash = "ALTARCbT6yTe7DSX5LSHgKhBB0t2cR+OPUabn0vmCEmxPBIVT/jb9r64jRVfvpwD5A=="
            });

            context.SaveChanges();

            context.Visits.AddOrUpdate(v => v.VisitId,
            new VisitDto
            {
                VisitId = 1,
                LessonId = 1,
                UserId = 2
            });
            context.SaveChanges();

            context.Scores.AddOrUpdate(s => s.ScoreId,
            new ScoreDto
            {
                ScoreId = 1,
                Score = 9,
                AssignedToId = 2,
                AssignedById = 1,
                VisitId = 1
            });
            context.SaveChanges();

            context.UserClaims.AddOrUpdate(new UserClaimDto
            {
                UserClaimId = ++_claimId,
                Value = "Register",
                Type = "User",
                ValueType = ClaimValueTypes.String,
                User = context.Users.Single(u => u.UserId == 1)
            });

            AddClaims(context, 1, "Lesson", new[] { "Create", "Update", "Delete", "GetAll" });
            AddClaims(context, 1, "Visit", new[] { "Update", "GetAll" });
            AddClaims(context, 1, "Group", new[] { "Create", "Update", "Delete", "GetAll" });
            AddClaims(context, 1, "Score", new[] { "Create", "Update", "Delete", "GetAll" });

            context.SaveChanges();
        }

        private void AddClaims(EasyTeachContext context, int userId, string resource, string[] operations)
        {
            var user = context.Users.Single(u => u.UserId == userId);
            foreach (string operation in operations)
            {
                context.UserClaims.AddOrUpdate(new UserClaimDto
                {
                    UserClaimId = ++_claimId,
                    Value = operation,
                    Type = resource,
                    ValueType = ClaimValueTypes.String,
                    User = user
                });
            }
        }
    }
}