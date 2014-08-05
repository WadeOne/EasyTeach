namespace EasyTeach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedQuizDtoes",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        NumberOfQuestions = c.Int(nullable: false),
                        Group_GroupId = c.Int(),
                        Quiz_QuizId = c.Int(),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.GroupDtoes", t => t.Group_GroupId)
                .ForeignKey("dbo.QuizDtoes", t => t.Quiz_QuizId)
                .Index(t => t.Group_GroupId)
                .Index(t => t.Quiz_QuizId);
            
            CreateTable(
                "dbo.GroupDtoes",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupNumber = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        ContactEmail = c.String(),
                        ContactPhone = c.String(),
                        ContactName = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.QuizDtoes",
                c => new
                    {
                        QuizId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Deprecated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QuizId);
            
            CreateTable(
                "dbo.QuestionDtoes",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionType = c.Int(nullable: false),
                        TextAnswer = c.String(),
                        QuestionText = c.String(),
                        QuizDto_QuizId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.QuizDtoes", t => t.QuizDto_QuizId)
                .Index(t => t.QuizDto_QuizId);
            
            CreateTable(
                "dbo.LessonDtoes",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LessonId)
                .ForeignKey("dbo.GroupDtoes", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.QuestionItemDtoes",
                c => new
                    {
                        QuestionItemId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsSolution = c.Boolean(nullable: false),
                        Question_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionItemId)
                .ForeignKey("dbo.QuestionDtoes", t => t.Question_QuestionId)
                .Index(t => t.Question_QuestionId);
            
            CreateTable(
                "dbo.ScoreDtoes",
                c => new
                    {
                        ScoreId = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        AssignedToId = c.Int(nullable: false),
                        AssignedById = c.Int(nullable: false),
                        VisitId = c.Int(),
                    })
                .PrimaryKey(t => t.ScoreId)
                .ForeignKey("dbo.UserDtoes", t => t.AssignedById, cascadeDelete: false)
                .ForeignKey("dbo.UserDtoes", t => t.AssignedToId, cascadeDelete: false)
                .ForeignKey("dbo.VisitDtoes", t => t.VisitId)
                .Index(t => t.AssignedToId)
                .Index(t => t.AssignedById)
                .Index(t => t.VisitId);
            
            CreateTable(
                "dbo.UserDtoes",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        GroupId = c.Int(),
                        Email = c.String(),
                        EmailIsValidated = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.GroupDtoes", t => t.GroupId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.VisitDtoes",
                c => new
                    {
                        VisitId = c.Int(nullable: false, identity: true),
                        LessonId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Note = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VisitId)
                .ForeignKey("dbo.LessonDtoes", t => t.LessonId, cascadeDelete: true)
                .ForeignKey("dbo.UserDtoes", t => t.UserId, cascadeDelete: true)
                .Index(t => t.LessonId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserClaimDtoes",
                c => new
                    {
                        UserClaimId = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Type = c.String(),
                        ValueType = c.String(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UserClaimId)
                .ForeignKey("dbo.UserDtoes", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.UserTokenDtoes",
                c => new
                    {
                        UserTokenId = c.Int(nullable: false, identity: true),
                        Puprose = c.String(),
                        Token = c.String(),
                        UserId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserTokenId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClaimDtoes", "User_UserId", "dbo.UserDtoes");
            DropForeignKey("dbo.ScoreDtoes", "VisitId", "dbo.VisitDtoes");
            DropForeignKey("dbo.VisitDtoes", "UserId", "dbo.UserDtoes");
            DropForeignKey("dbo.VisitDtoes", "LessonId", "dbo.LessonDtoes");
            DropForeignKey("dbo.ScoreDtoes", "AssignedToId", "dbo.UserDtoes");
            DropForeignKey("dbo.ScoreDtoes", "AssignedById", "dbo.UserDtoes");
            DropForeignKey("dbo.UserDtoes", "GroupId", "dbo.GroupDtoes");
            DropForeignKey("dbo.QuestionItemDtoes", "Question_QuestionId", "dbo.QuestionDtoes");
            DropForeignKey("dbo.LessonDtoes", "GroupId", "dbo.GroupDtoes");
            DropForeignKey("dbo.QuestionDtoes", "QuizDto_QuizId", "dbo.QuizDtoes");
            DropForeignKey("dbo.AssignedQuizDtoes", "Quiz_QuizId", "dbo.QuizDtoes");
            DropForeignKey("dbo.AssignedQuizDtoes", "Group_GroupId", "dbo.GroupDtoes");
            DropIndex("dbo.UserClaimDtoes", new[] { "User_UserId" });
            DropIndex("dbo.VisitDtoes", new[] { "UserId" });
            DropIndex("dbo.VisitDtoes", new[] { "LessonId" });
            DropIndex("dbo.UserDtoes", new[] { "GroupId" });
            DropIndex("dbo.ScoreDtoes", new[] { "VisitId" });
            DropIndex("dbo.ScoreDtoes", new[] { "AssignedById" });
            DropIndex("dbo.ScoreDtoes", new[] { "AssignedToId" });
            DropIndex("dbo.QuestionItemDtoes", new[] { "Question_QuestionId" });
            DropIndex("dbo.LessonDtoes", new[] { "GroupId" });
            DropIndex("dbo.QuestionDtoes", new[] { "QuizDto_QuizId" });
            DropIndex("dbo.AssignedQuizDtoes", new[] { "Quiz_QuizId" });
            DropIndex("dbo.AssignedQuizDtoes", new[] { "Group_GroupId" });
            DropTable("dbo.UserTokenDtoes");
            DropTable("dbo.UserClaimDtoes");
            DropTable("dbo.VisitDtoes");
            DropTable("dbo.UserDtoes");
            DropTable("dbo.ScoreDtoes");
            DropTable("dbo.QuestionItemDtoes");
            DropTable("dbo.LessonDtoes");
            DropTable("dbo.QuestionDtoes");
            DropTable("dbo.QuizDtoes");
            DropTable("dbo.GroupDtoes");
            DropTable("dbo.AssignedQuizDtoes");
        }
    }
}
