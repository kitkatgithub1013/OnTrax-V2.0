namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class durationDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductProcesses", "Duration", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductProcesses", "Duration", c => c.Int(nullable: false));
        }
    }
}
