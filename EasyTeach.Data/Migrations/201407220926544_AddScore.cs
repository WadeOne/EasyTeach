namespace EasyTeach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScoreDtoes", "AssignedToId", c => c.Int(nullable: false));
            AddColumn("dbo.ScoreDtoes", "AssignedById", c => c.Int(nullable: false));
            AddColumn("dbo.ScoreDtoes", "VisitId", c => c.Int());
            CreateIndex("dbo.ScoreDtoes", "AssignedToId");
            CreateIndex("dbo.ScoreDtoes", "AssignedById");
            CreateIndex("dbo.ScoreDtoes", "VisitId");
            AddForeignKey("dbo.ScoreDtoes", "AssignedById", "dbo.UserDtoes", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.ScoreDtoes", "AssignedToId", "dbo.UserDtoes", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.ScoreDtoes", "VisitId", "dbo.VisitDtoes", "VisitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScoreDtoes", "VisitId", "dbo.VisitDtoes");
            DropForeignKey("dbo.ScoreDtoes", "AssignedToId", "dbo.UserDtoes");
            DropForeignKey("dbo.ScoreDtoes", "AssignedById", "dbo.UserDtoes");
            DropIndex("dbo.ScoreDtoes", new[] { "VisitId" });
            DropIndex("dbo.ScoreDtoes", new[] { "AssignedById" });
            DropIndex("dbo.ScoreDtoes", new[] { "AssignedToId" });
            DropColumn("dbo.ScoreDtoes", "VisitId");
            DropColumn("dbo.ScoreDtoes", "AssignedById");
            DropColumn("dbo.ScoreDtoes", "AssignedToId");
        }
    }
}
