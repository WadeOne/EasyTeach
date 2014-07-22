using System.Data.Entity.Core.Common.CommandTrees;

namespace EasyTeach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitDtoes", "UserId", "dbo.UserDtoes");
            DropForeignKey("dbo.VisitDtoes", "LessonId", "dbo.LessonDtoes");
            DropForeignKey("dbo.UserClaimDtoes", "User_UserId", "dbo.UserDtoes");
            DropForeignKey("dbo.UserDtoes", "GroupId", "dbo.GroupDtoes");
            DropForeignKey("dbo.QuestionDtoes", "QuizDto_QuizId", "dbo.QuizDtoes");
            DropForeignKey("dbo.QuestionItemDtoes", "Question_QuestionId", "dbo.QuestionDtoes");
            DropForeignKey("dbo.LessonDtoes", "GroupId", "dbo.GroupDtoes");
            DropIndex("dbo.VisitDtoes", new[] { "UserId" });
            DropIndex("dbo.VisitDtoes", new[] { "LessonId" });
            DropIndex("dbo.UserDtoes", new[] { "GroupId" });
            DropIndex("dbo.UserClaimDtoes", new[] { "User_UserId" });
            DropIndex("dbo.QuestionDtoes", new[] { "QuizDto_QuizId" });
            DropIndex("dbo.QuestionItemDtoes", new[] { "Question_QuestionId" });
            DropIndex("dbo.LessonDtoes", new[] { "GroupId" });
            DropTable("dbo.VisitDtoes");
            DropTable("dbo.UserTokenDtoes");
            DropTable("dbo.UserDtoes");
            DropTable("dbo.UserClaimDtoes");
            DropTable("dbo.QuizDtoes");
            DropTable("dbo.QuestionDtoes");
            DropTable("dbo.QuestionItemDtoes");
            DropTable("dbo.LessonDtoes");
            DropTable("dbo.GroupDtoes");
        }
    }
}
