namespace FManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        Broker = c.String(),
                        InitialInvestment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Change = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.CurrencyId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyId = c.Guid(nullable: false),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Change = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Change = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Entries",
                c => new
                    {
                        EntryId = c.Guid(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Win = c.Boolean(nullable: false),
                        Summary = c.String(),
                        ParityId = c.Guid(nullable: false),
                        SectionId = c.Guid(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Change = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.EntryId)
                .ForeignKey("dbo.Parities", t => t.ParityId)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.ParityId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Parities",
                c => new
                    {
                        ParityId = c.Guid(nullable: false),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Change = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ParityId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Summary = c.String(),
                        AccountId = c.Guid(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Change = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.SectionId)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entries", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Sections", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Entries", "ParityId", "dbo.Parities");
            DropForeignKey("dbo.Accounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Accounts", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.Sections", new[] { "AccountId" });
            DropIndex("dbo.Entries", new[] { "SectionId" });
            DropIndex("dbo.Entries", new[] { "ParityId" });
            DropIndex("dbo.Accounts", new[] { "UserId" });
            DropIndex("dbo.Accounts", new[] { "CurrencyId" });
            DropTable("dbo.Sections");
            DropTable("dbo.Parities");
            DropTable("dbo.Entries");
            DropTable("dbo.Users");
            DropTable("dbo.Currencies");
            DropTable("dbo.Accounts");
        }
    }
}
