namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelUserGenderNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Gender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Gender", c => c.Boolean());
        }
    }
}
