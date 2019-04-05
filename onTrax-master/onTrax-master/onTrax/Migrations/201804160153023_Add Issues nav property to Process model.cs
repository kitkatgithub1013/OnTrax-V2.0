namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIssuesnavpropertytoProcessmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "Process_ProcessID", c => c.Int());
            CreateIndex("dbo.Issues", "Process_ProcessID");
            AddForeignKey("dbo.Issues", "Process_ProcessID", "dbo.Processes", "ProcessID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "Process_ProcessID", "dbo.Processes");
            DropIndex("dbo.Issues", new[] { "Process_ProcessID" });
            DropColumn("dbo.Issues", "Process_ProcessID");
        }
    }
}
