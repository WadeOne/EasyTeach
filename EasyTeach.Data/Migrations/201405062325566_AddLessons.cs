namespace EasyTeach.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddLessons : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Questions", newName: "QuestionModels");
            RenameColumn(table: "dbo.QuestionItems", name: "Question_QuestionId", newName: "QuestionModel_QuestionId");
            RenameIndex(table: "dbo.QuestionItems", name: "IX_Question_QuestionId", newName: "IX_QuestionModel_QuestionId");
            CreateTable(
                "dbo.LessonDtoes",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LessonId);
            
            AddColumn("dbo.Groups", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "Year");
            DropTable("dbo.LessonDtoes");
            RenameIndex(table: "dbo.QuestionItems", name: "IX_QuestionModel_QuestionId", newName: "IX_Question_QuestionId");
            RenameColumn(table: "dbo.QuestionItems", name: "QuestionModel_QuestionId", newName: "Question_QuestionId");
            RenameTable(name: "dbo.QuestionModels", newName: "Questions");
        }
    }
}
