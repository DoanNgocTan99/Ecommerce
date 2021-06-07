namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transaction", "IdDeliveryStatus", "dbo.DeliveryStatus");
            DropIndex("dbo.Transaction", new[] { "IdDeliveryStatus" });
            AlterColumn("dbo.Transaction", "IdDeliveryStatus", c => c.Long());
            CreateIndex("dbo.Transaction", "IdDeliveryStatus");
            AddForeignKey("dbo.Transaction", "IdDeliveryStatus", "dbo.DeliveryStatus", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "IdDeliveryStatus", "dbo.DeliveryStatus");
            DropIndex("dbo.Transaction", new[] { "IdDeliveryStatus" });
            AlterColumn("dbo.Transaction", "IdDeliveryStatus", c => c.Long(nullable: false));
            CreateIndex("dbo.Transaction", "IdDeliveryStatus");
            AddForeignKey("dbo.Transaction", "IdDeliveryStatus", "dbo.DeliveryStatus", "id", cascadeDelete: true);
        }
    }
}
