namespace EasyTeach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizDtoUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionType = c.Int(nullable: false),
                        TextAnswer = c.String(),
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
                        Question_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionItemId)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId)
                .Index(t => t.Question_QuestionId);
            
            AddColumn("dbo.QuizDtoes", "Name", c => c.String());
            AddColumn("dbo.QuizDtoes", "Description", c => c.String());
            AddColumn("dbo.QuizDtoes", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "QuizDto_QuizId", "dbo.QuizDtoes");
            DropForeignKey("dbo.QuestionItems", "Question_QuestionId", "dbo.Questions");
            DropIndex("dbo.QuestionItems", new[] { "Question_QuestionId" });
            DropIndex("dbo.Questions", new[] { "QuizDto_QuizId" });
            DropColumn("dbo.QuizDtoes", "Version");
            DropColumn("dbo.QuizDtoes", "Description");
            DropColumn("dbo.QuizDtoes", "Name");
            DropTable("dbo.QuestionItems");
            DropTable("dbo.Questions");
        }
    }
}
