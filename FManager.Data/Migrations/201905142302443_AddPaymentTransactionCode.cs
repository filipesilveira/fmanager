namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentTransactionCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PaymentTransactionCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PaymentTransactionCode");
        }
    }
}
