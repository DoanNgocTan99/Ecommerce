namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTableTransac : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Discount", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Discount", c => c.Int(nullable: false));
        }
    }
}
