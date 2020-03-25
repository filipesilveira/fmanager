namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DayBookEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayBookItems",
                c => new
                    {
                        DayBookItemId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Note = c.String(),
                        DayBookId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Change = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.DayBookItemId)
                .ForeignKey("dbo.DayBooks", t => t.DayBookId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.DayBookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DayBooks",
                c => new
                    {
                        DayBookId = c.Guid(nullable: false),
                        Name = c.String(),
                        UserId = c.Guid(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Change = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.DayBookId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DayBookItems", "UserId", "dbo.Users");
            DropForeignKey("dbo.DayBookItems", "DayBookId", "dbo.DayBooks");
            DropForeignKey("dbo.DayBooks", "UserId", "dbo.Users");
            DropIndex("dbo.DayBooks", new[] { "UserId" });
            DropIndex("dbo.DayBookItems", new[] { "UserId" });
            DropIndex("dbo.DayBookItems", new[] { "DayBookId" });
            DropTable("dbo.DayBooks");
            DropTable("dbo.DayBookItems");
        }
    }
}
