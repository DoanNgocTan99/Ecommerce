namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTableTransactions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "IdShop", c => c.Long());
            CreateIndex("dbo.Transaction", "IdShop");
            AddForeignKey("dbo.Transaction", "IdShop", "dbo.Shop", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "IdShop", "dbo.Shop");
            DropIndex("dbo.Transaction", new[] { "IdShop" });
            DropColumn("dbo.Transaction", "IdShop");
        }
    }
}
