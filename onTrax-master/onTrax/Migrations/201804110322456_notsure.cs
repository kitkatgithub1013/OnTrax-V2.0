namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notsure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductProcesses", "StandardDuration", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ProductProcesses", "Duration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductProcesses", "Duration", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ProductProcesses", "StandardDuration");
        }
    }
}
