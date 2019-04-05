namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowNullDateRecorded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productions", "DateRecorded", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productions", "DateRecorded", c => c.DateTime(nullable: false));
        }
    }
}
