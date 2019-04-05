namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProcessesnavpropertytoIssuesmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "Process_ProcessID", "dbo.Processes");
            DropIndex("dbo.Issues", new[] { "Process_ProcessID" });
            CreateTable(
                "dbo.ProcessIssues",
                c => new
                    {
                        Process_ProcessID = c.Int(nullable: false),
                        Issue_IssueID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Process_ProcessID, t.Issue_IssueID })
                .ForeignKey("dbo.Processes", t => t.Process_ProcessID, cascadeDelete: true)
                .ForeignKey("dbo.Issues", t => t.Issue_IssueID, cascadeDelete: true)
                .Index(t => t.Process_ProcessID)
                .Index(t => t.Issue_IssueID);
            
            DropColumn("dbo.Issues", "Process_ProcessID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "Process_ProcessID", c => c.Int());
            DropForeignKey("dbo.ProcessIssues", "Issue_IssueID", "dbo.Issues");
            DropForeignKey("dbo.ProcessIssues", "Process_ProcessID", "dbo.Processes");
            DropIndex("dbo.ProcessIssues", new[] { "Issue_IssueID" });
            DropIndex("dbo.ProcessIssues", new[] { "Process_ProcessID" });
            DropTable("dbo.ProcessIssues");
            CreateIndex("dbo.Issues", "Process_ProcessID");
            AddForeignKey("dbo.Issues", "Process_ProcessID", "dbo.Processes", "ProcessID");
        }
    }
}
