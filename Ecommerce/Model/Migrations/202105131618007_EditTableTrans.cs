namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTableTrans : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Discount", c => c.Int(nullable: true));
            AlterColumn("dbo.Transaction", "Amount", c => c.Decimal(nullable: false, precision: 20, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transaction", "Amount", c => c.String(maxLength: 250));
            AlterColumn("dbo.Product", "Discount", c => c.Int());
        }
    }
}
