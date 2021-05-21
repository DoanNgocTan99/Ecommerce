namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColAmoutnInTableOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Amount");
        }
    }
}
