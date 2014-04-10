namespace EasyTeach.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTokens : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTokenDtoes",
                c => new
                    {
                        UserTokenId = c.Int(nullable: false, identity: true),
                        Puprose = c.String(nullable: false),
                        Token = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserTokenId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserTokenDtoes");
        }
    }
}
