namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTableProgramme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programme", "IsActive", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Programme", "IsActive");
        }
    }
}
