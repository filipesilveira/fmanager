namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "PaymentStatusId", "dbo.PaymentStatus");
            DropIndex("dbo.Users", new[] { "PaymentStatusId" });
            CreateTable(
                "dbo.PaymentHistories",
                c => new
                    {
                        PaymentHistoryId = c.Guid(nullable: false),
                        Reference = c.String(),
                        PaymentStatusId = c.Guid(nullable: false),
                        PaymentPlanId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Change = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PaymentHistoryId)
                .ForeignKey("dbo.PaymentPlans", t => t.PaymentPlanId)
                .ForeignKey("dbo.PaymentStatus", t => t.PaymentStatusId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.PaymentStatusId)
                .Index(t => t.PaymentPlanId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PaymentPlans",
                c => new
                    {
                        PaymentPlanId = c.Guid(nullable: false),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Days = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Change = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PaymentPlanId);
            
            DropColumn("dbo.Users", "PaymentTransactionCode");
            DropColumn("dbo.Users", "PaymentStatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "PaymentStatusId", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "PaymentTransactionCode", c => c.String());
            DropForeignKey("dbo.PaymentHistories", "UserId", "dbo.Users");
            DropForeignKey("dbo.PaymentHistories", "PaymentStatusId", "dbo.PaymentStatus");
            DropForeignKey("dbo.PaymentHistories", "PaymentPlanId", "dbo.PaymentPlans");
            DropIndex("dbo.PaymentHistories", new[] { "UserId" });
            DropIndex("dbo.PaymentHistories", new[] { "PaymentPlanId" });
            DropIndex("dbo.PaymentHistories", new[] { "PaymentStatusId" });
            DropTable("dbo.PaymentPlans");
            DropTable("dbo.PaymentHistories");
            CreateIndex("dbo.Users", "PaymentStatusId");
            AddForeignKey("dbo.Users", "PaymentStatusId", "dbo.PaymentStatus", "PaymentStatusId");
        }
    }
}
