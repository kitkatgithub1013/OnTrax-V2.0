namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveactivepropertyfromProductionmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Productions", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Productions", "Active", c => c.Boolean(nullable: false));
        }
    }
}
