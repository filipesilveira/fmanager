namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpirationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentHistories", "Expiration", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentHistories", "Expiration");
        }
    }
}
