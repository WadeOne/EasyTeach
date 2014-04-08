namespace EasyTeach.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddUserPasswordHash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDtoes", "PasswordHash", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDtoes", "PasswordHash");
        }
    }
}
