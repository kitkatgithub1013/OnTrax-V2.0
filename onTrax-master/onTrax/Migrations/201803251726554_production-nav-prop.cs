namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productionnavprop : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProcessProducts", newName: "ProductProcesses");
            DropPrimaryKey("dbo.ProductProcesses");
            AddPrimaryKey("dbo.ProductProcesses", new[] { "Product_ProductID", "Process_ProcessID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProductProcesses");
            AddPrimaryKey("dbo.ProductProcesses", new[] { "Process_ProcessID", "Product_ProductID" });
            RenameTable(name: "dbo.ProductProcesses", newName: "ProcessProducts");
        }
    }
}
