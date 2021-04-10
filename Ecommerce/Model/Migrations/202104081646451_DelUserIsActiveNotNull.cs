namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelUserIsActiveNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "IsActive", c => c.Boolean());
        }
    }
}
