namespace EasyTeach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizDto_Updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuizDtoes", "IsDeprecated", c => c.Boolean(nullable: false));
            DropColumn("dbo.QuizDtoes", "Version");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuizDtoes", "Version", c => c.Int(nullable: false));
            DropColumn("dbo.QuizDtoes", "IsDeprecated");
        }
    }
}
