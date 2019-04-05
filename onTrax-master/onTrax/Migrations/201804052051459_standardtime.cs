namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class standardtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductProcesses", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductProcesses", "Duration");
        }
    }
}
