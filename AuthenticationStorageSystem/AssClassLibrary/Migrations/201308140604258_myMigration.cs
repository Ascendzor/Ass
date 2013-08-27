namespace AssClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class myMigration : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        isArchived = c.Boolean(nullable: false),
                        Notes = c.String(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClientId = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        isArchived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Entries",
                c => new
                    {
                        EntryId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(),
                        Website = c.String(),
                        Notes = c.String(),
                        isArchived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EntryId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Entries", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "ClientId" });
            DropForeignKey("dbo.Entries", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ClientId", "dbo.Clients");
            DropTable("dbo.Entries");
            DropTable("dbo.Projects");
            DropTable("dbo.Clients");
        }
    }
}
