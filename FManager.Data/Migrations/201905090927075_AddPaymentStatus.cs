namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        PaymentStatusId = c.Guid(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.PaymentStatusId);
            
            AddColumn("dbo.Users", "PaymentStatusId", c => c.Guid());
            CreateIndex("dbo.Users", "PaymentStatusId");
            AddForeignKey("dbo.Users", "PaymentStatusId", "dbo.PaymentStatus", "PaymentStatusId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PaymentStatusId", "dbo.PaymentStatus");
            DropIndex("dbo.Users", new[] { "PaymentStatusId" });
            DropColumn("dbo.Users", "PaymentStatusId");
            DropTable("dbo.PaymentStatus");
        }
    }
}
