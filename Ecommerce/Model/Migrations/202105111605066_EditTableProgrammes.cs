namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTableProgrammes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Programme", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Programme", "IsActive", c => c.Boolean(nullable: false));
        }
    }
}
