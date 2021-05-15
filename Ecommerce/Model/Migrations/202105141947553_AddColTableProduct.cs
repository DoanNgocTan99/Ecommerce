namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColTableProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Warranty", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Warranty");
        }
    }
}
