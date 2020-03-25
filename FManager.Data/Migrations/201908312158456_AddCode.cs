namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentHistories", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentHistories", "Code");
        }
    }
}
