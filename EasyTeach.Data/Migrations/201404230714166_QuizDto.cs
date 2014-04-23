namespace EasyTeach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizDto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuizDtoes",
                c => new
                    {
                        QuizId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.QuizId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QuizDtoes");
        }
    }
}
