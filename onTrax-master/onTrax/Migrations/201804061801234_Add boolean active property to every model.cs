namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addbooleanactivepropertytoeverymodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Batches", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Productions", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Issues", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Processes", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Processes", "Active");
            DropColumn("dbo.Issues", "Active");
            DropColumn("dbo.Employees", "Active");
            DropColumn("dbo.Productions", "Active");
            DropColumn("dbo.Batches", "Active");
        }
    }
}
