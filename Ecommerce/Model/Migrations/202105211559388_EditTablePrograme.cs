namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTablePrograme : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Programme", "DateEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Programme", "DateEnd", c => c.DateTime(nullable: false));
        }
    }
}
