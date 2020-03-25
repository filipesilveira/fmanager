namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntryPayout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entries", "Payout", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Entries", "Payout");
        }
    }
}
