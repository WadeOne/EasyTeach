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
                "dbo.QuestionModels",
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
                "dbo.QuestionItems",
                c => new
                    {
                        QuestionItemId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsSolution = c.Boolean(nullable: false),
                        QuestionModel_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionItemId)
                .ForeignKey("dbo.QuestionModels", t => t.QuestionModel_QuestionId)
                .Index(t => t.QuestionModel_QuestionId);
            
            CreateTable(
                "dbo.QuizDtoes",
                c => new
                    {
                        QuizId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Version = c.Int(nullable: false),
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
                        Email = c.String(),
                        EmailIsValidated = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        Group_GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.GroupDtoes", t => t.Group_GroupId)
                .Index(t => t.Group_GroupId);
            
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
            DropForeignKey("dbo.UserDtoes", "Group_GroupId", "dbo.GroupDtoes");
            DropForeignKey("dbo.QuestionModels", "QuizDto_QuizId", "dbo.QuizDtoes");
            DropForeignKey("dbo.QuestionItems", "QuestionModel_QuestionId", "dbo.QuestionModels");
            DropForeignKey("dbo.LessonDtoes", "GroupId", "dbo.GroupDtoes");
            DropIndex("dbo.UserDtoes", new[] { "Group_GroupId" });
            DropIndex("dbo.UserClaimDtoes", new[] { "User_UserId" });
            DropIndex("dbo.QuestionItems", new[] { "QuestionModel_QuestionId" });
            DropIndex("dbo.QuestionModels", new[] { "QuizDto_QuizId" });
            DropIndex("dbo.LessonDtoes", new[] { "GroupId" });
            DropTable("dbo.UserTokenDtoes");
            DropTable("dbo.UserDtoes");
            DropTable("dbo.UserClaimDtoes");
            DropTable("dbo.QuizDtoes");
            DropTable("dbo.QuestionItems");
            DropTable("dbo.QuestionModels");
            DropTable("dbo.LessonDtoes");
            DropTable("dbo.GroupDtoes");
        }
    }
}
