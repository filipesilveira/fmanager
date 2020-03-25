namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixPaymentStatus : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "PaymentStatusId" });
            AlterColumn("dbo.Users", "PaymentStatusId", c => c.Guid(nullable: false));
            AlterColumn("dbo.PaymentStatus", "Code", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "PaymentStatusId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "PaymentStatusId" });
            AlterColumn("dbo.PaymentStatus", "Code", c => c.String());
            AlterColumn("dbo.Users", "PaymentStatusId", c => c.Guid());
            CreateIndex("dbo.Users", "PaymentStatusId");
        }
    }
}
