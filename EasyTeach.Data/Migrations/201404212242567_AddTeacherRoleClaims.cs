namespace EasyTeach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeacherRoleClaims : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClaimDtoes", "User_UserId", "dbo.UserDtoes");
            DropIndex("dbo.UserClaimDtoes", new[] { "User_UserId" });
            DropTable("dbo.UserClaimDtoes");
        }
    }
}
