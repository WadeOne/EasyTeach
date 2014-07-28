using System.Data.Entity;

using EasyTeach.Data.Entities;
using EasyTeach.Data.Migrations;

namespace EasyTeach.Data.Context
{
    public class EasyTeachContext : DbContext
    {
        public virtual IDbSet<UserDto> Users { get; set; }

        public virtual IDbSet<GroupDto> Groups { get; set; }

        public virtual IDbSet<UserTokenDto> UserTokens { get; set; }

        public virtual IDbSet<UserClaimDto> UserClaims { get; set; }

        public virtual IDbSet<QuizDto> Quizes { get; set; }

        public virtual IDbSet<QuestionDto> Questions { get; set; }

        public virtual IDbSet<QuestionItemDto> QuestionItems { get; set; }

        public virtual IDbSet<AssignedQuizDto> AssignedQuizes { get; set; }

        public virtual IDbSet<LessonDto> Lessons { get; set; }

        public virtual IDbSet<VisitDto> Visits { get; set; }

        public virtual IDbSet<ScoreDto> Scores { get; set; }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EasyTeachContext, Configuration>());
        }*/
    }
}
