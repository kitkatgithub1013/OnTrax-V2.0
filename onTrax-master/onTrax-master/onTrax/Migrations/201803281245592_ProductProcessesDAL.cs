namespace onTrax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductProcessesDAL : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductProcesses", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductProcesses", "Process_ProcessID", "dbo.Processes");
            DropIndex("dbo.ProductProcesses", new[] { "Product_ProductID" });
            DropIndex("dbo.ProductProcesses", new[] { "Process_ProcessID" });
            DropPrimaryKey("dbo.ProductProcesses");
            AddColumn("dbo.ProductProcesses", "ProductProcessID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ProductProcesses", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductProcesses", "Product_ProductID", c => c.Int());
            AlterColumn("dbo.ProductProcesses", "Process_ProcessID", c => c.Int());
            AddPrimaryKey("dbo.ProductProcesses", "ProductProcessID");
            CreateIndex("dbo.ProductProcesses", "Process_ProcessID");
            CreateIndex("dbo.ProductProcesses", "Product_ProductID");
            AddForeignKey("dbo.ProductProcesses", "Product_ProductID", "dbo.Products", "ProductID");
            AddForeignKey("dbo.ProductProcesses", "Process_ProcessID", "dbo.Processes", "ProcessID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductProcesses", "Process_ProcessID", "dbo.Processes");
            DropForeignKey("dbo.ProductProcesses", "Product_ProductID", "dbo.Products");
            DropIndex("dbo.ProductProcesses", new[] { "Product_ProductID" });
            DropIndex("dbo.ProductProcesses", new[] { "Process_ProcessID" });
            DropPrimaryKey("dbo.ProductProcesses");
            AlterColumn("dbo.ProductProcesses", "Process_ProcessID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductProcesses", "Product_ProductID", c => c.Int(nullable: false));
            DropColumn("dbo.ProductProcesses", "Quantity");
            DropColumn("dbo.ProductProcesses", "ProductProcessID");
            AddPrimaryKey("dbo.ProductProcesses", new[] { "Product_ProductID", "Process_ProcessID" });
            CreateIndex("dbo.ProductProcesses", "Process_ProcessID");
            CreateIndex("dbo.ProductProcesses", "Product_ProductID");
            AddForeignKey("dbo.ProductProcesses", "Process_ProcessID", "dbo.Processes", "ProcessID", cascadeDelete: true);
            AddForeignKey("dbo.ProductProcesses", "Product_ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
        }
    }
}
