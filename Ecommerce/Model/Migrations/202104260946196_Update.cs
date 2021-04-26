namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "DOB", c => c.DateTime());
            AlterColumn("dbo.User", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.User", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.User", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.User", "DOB", c => c.DateTime(nullable: false));
        }
    }
}
