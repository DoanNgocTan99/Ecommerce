namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangerPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.Product", "DelPrice", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "DelPrice", c => c.Decimal(precision: 2, scale: 0));
            AlterColumn("dbo.Product", "Price", c => c.Decimal(nullable: false, precision: 2, scale: 0));
        }
    }
}
