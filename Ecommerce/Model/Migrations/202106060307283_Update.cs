namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transaction", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Transaction", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transaction", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Transaction", "ModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
