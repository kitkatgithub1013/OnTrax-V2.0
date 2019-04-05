namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetActivetotruebydefault : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Active");
        }
    }
}
