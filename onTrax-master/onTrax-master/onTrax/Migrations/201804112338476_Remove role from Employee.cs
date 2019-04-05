namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoverolefromEmployee : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Role", c => c.String());
        }
    }
}
