namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColPriceInOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Price");
        }
    }
}
