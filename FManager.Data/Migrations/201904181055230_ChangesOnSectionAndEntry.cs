namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesOnSectionAndEntry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entries", "DateAndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Sections", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sections", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Entries", "DateAndTime");
        }
    }
}
