namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserSessionToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SessionToken", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "SessionToken");
        }
    }
}
