namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableProduct2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "DateAddFlaseSale", c => c.DateTime());
            AddColumn("dbo.Product", "DateAddAdvertisement", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "DateAddAdvertisement");
            DropColumn("dbo.Product", "DateAddFlaseSale");
        }
    }
}
