namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditColTableProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "WarrantyDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "WarrantyDate", c => c.DateTime(nullable: false));
        }
    }
}
