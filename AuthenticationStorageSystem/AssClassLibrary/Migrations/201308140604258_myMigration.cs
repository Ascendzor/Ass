namespace AssClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class myMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entries", "DevState", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Entries", "DevState");
        }
    }
}
