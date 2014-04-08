namespace EasyTeach.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.UserDtoes",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        EmailIsValidated = c.Boolean(nullable: false),
                        UserType = c.Int(nullable: false),
                        Group_GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId)
                .Index(t => t.Group_GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDtoes", "Group_GroupId", "dbo.Groups");
            DropIndex("dbo.UserDtoes", new[] { "Group_GroupId" });
            DropTable("dbo.UserDtoes");
            DropTable("dbo.Groups");
        }
    }
}
