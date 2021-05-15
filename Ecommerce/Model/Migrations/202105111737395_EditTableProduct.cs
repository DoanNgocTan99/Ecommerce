namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTableProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "FlaseSale", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Advertisement", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Advertisement");
            DropColumn("dbo.Product", "FlaseSale");
        }
    }
}
