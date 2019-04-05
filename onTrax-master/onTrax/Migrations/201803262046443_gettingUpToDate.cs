namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gettingUpToDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Processes", "ChunkQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Processes", "ChunkQuantity", c => c.Int(nullable: false));
        }
    }
}
