namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "Name", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.DeliveryStatus", "Status", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Payment", "Nam");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payment", "Nam", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.DeliveryStatus", "Status", c => c.String(maxLength: 250));
            DropColumn("dbo.Payment", "Name");
        }
    }
}
