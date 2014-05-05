using System.Data.Entity;
using EasyTeach.Core.Entities;
using EasyTeach.Data.Entities;
using EasyTeach.Data.Migrations;

using QuizDto = EasyTeach.Data.Entities.QuizDto;

namespace EasyTeach.Data.Context
{
    public class EasyTeachContext : DbContext
    {
        public virtual IDbSet<UserDto> Users { get; set; }

        public virtual IDbSet<Group> Groups { get; set; }

        public virtual IDbSet<UserTokenDto> UserTokens { get; set; }

        public virtual IDbSet<UserClaimDto> UserClaims { get; set; }

        public virtual IDbSet<QuizDto> Quizes { get; set; }

        public virtual IDbSet<QuestionModel> Questions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EasyTeachContext, Configuration>());
        }
    }
}
