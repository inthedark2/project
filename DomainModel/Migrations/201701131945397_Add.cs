namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductInOrders", "Order_Id", "dbo.Orders");
            DropIndex("dbo.ProductInOrders", new[] { "Order_Id" });
            RenameColumn(table: "dbo.ProductInOrders", name: "Order_Id", newName: "OrderID");
            AlterColumn("dbo.ProductInOrders", "OrderID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductInOrders", "OrderID");
            AddForeignKey("dbo.ProductInOrders", "OrderID", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductInOrders", "OrderID", "dbo.Orders");
            DropIndex("dbo.ProductInOrders", new[] { "OrderID" });
            AlterColumn("dbo.ProductInOrders", "OrderID", c => c.Int());
            RenameColumn(table: "dbo.ProductInOrders", name: "OrderID", newName: "Order_Id");
            CreateIndex("dbo.ProductInOrders", "Order_Id");
            AddForeignKey("dbo.ProductInOrders", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
