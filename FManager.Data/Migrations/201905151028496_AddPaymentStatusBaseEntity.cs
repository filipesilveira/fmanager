namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentStatusBaseEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentStatus", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaymentStatus", "Change", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaymentStatus", "CreationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentStatus", "CreationDate");
            DropColumn("dbo.PaymentStatus", "Change");
            DropColumn("dbo.PaymentStatus", "Deleted");
        }
    }
}
