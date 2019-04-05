namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductProcessbridgeproperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productions", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Processes", "ChunkQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Processes", "ChunkQuantity");
            DropColumn("dbo.Productions", "Quantity");
        }
    }
}
