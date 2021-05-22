namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ModifiedPriceDate", c => c.DateTime());
            AddColumn("dbo.Product", "PriceAfterChange", c => c.Int());
            AlterColumn("dbo.Image", "Path", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Image", "Path", c => c.String(nullable: false, maxLength: 500, unicode: false));
            DropColumn("dbo.Product", "PriceAfterChange");
            DropColumn("dbo.Product", "ModifiedPriceDate");
        }
    }
}
