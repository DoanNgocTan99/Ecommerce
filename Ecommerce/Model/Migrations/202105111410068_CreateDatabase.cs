namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 250),
                    Description = c.String(nullable: true, maxLength: 250),
                    IsActive = c.Boolean(nullable: true, defaultValue: true),
                    CreatedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedDate = c.DateTime(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Image",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Path = c.String(nullable: true, maxLength: 500, unicode: false),
                    IdProduct = c.Long(nullable: true),
                    IdCategory = c.Long(nullable: true),
                    IdUser = c.Long(nullable: true),
                    IdShop = c.Long(nullable: true),
                    CreatedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedDate = c.DateTime(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.IdProduct, cascadeDelete: true)
                .ForeignKey("dbo.Shop", t => t.IdShop, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.IdUser, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.IdCategory, cascadeDelete: true)
                .Index(t => t.IdProduct)
                .Index(t => t.IdCategory)
                .Index(t => t.IdUser)
                .Index(t => t.IdShop);

            CreateTable(
                "dbo.Product",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 250),
                    Description = c.String(nullable: true, maxLength: 250),
                    Brand = c.String(nullable: true, maxLength: 250),
                    Material = c.String(maxLength: 250, nullable: true),
                    Origin = c.String(nullable: true, maxLength: 250),
                    Price = c.Decimal(nullable: false, precision: 2, scale: 0),
                    DelPrice = c.Decimal(nullable: true, precision: 2, scale: 0),
                    WarrantyDate = c.DateTime(nullable: true),
                    Stock = c.Int(nullable: true),
                    Discount = c.Int(nullable: true),
                    Views = c.Int(nullable: true),
                    Rate = c.Int(nullable: true),
                    IsActive = c.Boolean(nullable: true, defaultValue: true),
                    CreatedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedDate = c.DateTime(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                    IdCategory = c.Long(nullable: true),
                    IdShop = c.Long(nullable: true),
                    IdProgramme = c.Long(nullable: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.IdShop)
                .ForeignKey("dbo.Programme", t => t.IdProgramme, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.IdCategory)
                .Index(t => t.IdCategory)
                .Index(t => t.IdShop)
                .Index(t => t.IdProgramme);

            CreateTable(
                "dbo.Order",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    IdTransaction = c.Long(nullable: true),
                    IdProduct = c.Long(nullable: true),
                    IdShop = c.Long(nullable: true),
                    IdPayment = c.Long(nullable: true),
                    Message = c.String(maxLength: 250, nullable: true),
                    CreatedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedDate = c.DateTime(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payment", t => t.IdPayment)
                .ForeignKey("dbo.Shop", t => t.IdShop)
                .ForeignKey("dbo.Transaction", t => t.IdTransaction, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.IdProduct, cascadeDelete: true)
                .Index(t => t.IdTransaction)
                .Index(t => t.IdProduct)
                .Index(t => t.IdShop)
                .Index(t => t.IdPayment);

            CreateTable(
                "dbo.Payment",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: true, maxLength: 250),
                    Type = c.String(maxLength: 250, nullable: true),
                    Allow = c.Boolean(nullable: true),
                    CreatedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedDate = c.DateTime(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Shop",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: true, maxLength: 250),
                    Description = c.String(nullable: true, maxLength: 250),
                    Address = c.String(nullable: true, maxLength: 250),
                    Phone = c.String(nullable: true, maxLength: 250),
                    CreatedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedDate = c.DateTime(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                    IdUser = c.Long(nullable: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdUser)
                .Index(t => t.IdUser);

            CreateTable(
                "dbo.User",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 250),
                    UserName = c.String(nullable: false, maxLength: 250),
                    Password = c.String(nullable: false, maxLength: 250),
                    Email = c.String(maxLength: 250, nullable: true),
                    Phone = c.String(maxLength: 250, nullable: true),
                    Gender = c.Boolean(nullable: true),
                    DOB = c.DateTime(nullable: true),
                    Address = c.String(maxLength: 250, nullable: true),
                    IsActive = c.Boolean(nullable: true, defaultValue: true),
                    CreatedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedDate = c.DateTime(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                    IdRole = c.Long(nullable: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.IdRole, cascadeDelete: true)
                .Index(t => t.IdRole);

            CreateTable(
                "dbo.Review",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    IdProduct = c.Long(nullable: true),
                    IdUser = c.Long(nullable: true),
                    Content = c.String(storeType: "ntext", nullable: true),
                    CreateDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.IdProduct, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.IdUser)
                .Index(t => t.IdProduct)
                .Index(t => t.IdUser);

            CreateTable(
                "dbo.Role",
                c => new
                {
                    Id = c.Long(nullable: false),
                    Name = c.String(nullable: false, maxLength: 250),
                    CreatedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedDate = c.DateTime(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Transaction",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Address = c.String(maxLength: 250, nullable: true),
                    Amount = c.String(maxLength: 250, nullable: true),
                    CheckoutStatus = c.String(maxLength: 250, nullable: true),
                    CreatedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedDate = c.DateTime(nullable: true),
                    CreatedDate = c.DateTime(nullable: true),
                    IdUser = c.Long(nullable: true),
                    IdDeliveryStatus = c.Long(nullable: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeliveryStatus", t => t.IdDeliveryStatus, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser)
                .Index(t => t.IdDeliveryStatus);

            CreateTable(
                "dbo.DeliveryStatus",
                c => new
                {
                    id = c.Long(nullable: false, identity: true),
                    Status = c.String(nullable: true, maxLength: 250),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.Programme",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(maxLength: 250),
                    DateEnd = c.DateTime(nullable: true),
                    CreatedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedBy = c.String(maxLength: 250, nullable: true),
                    ModifiedDate = c.DateTime(nullable: true),
                    MreatedDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Product", "IdCategory", "dbo.Category");
            DropForeignKey("dbo.Image", "IdCategory", "dbo.Category");
            DropForeignKey("dbo.Product", "IdProgramme", "dbo.Programme");
            DropForeignKey("dbo.Order", "IdProduct", "dbo.Product");
            DropForeignKey("dbo.Transaction", "IdUser", "dbo.User");
            DropForeignKey("dbo.Order", "IdTransaction", "dbo.Transaction");
            DropForeignKey("dbo.Transaction", "IdDeliveryStatus", "dbo.DeliveryStatus");
            DropForeignKey("dbo.Shop", "IdUser", "dbo.User");
            DropForeignKey("dbo.User", "IdRole", "dbo.Role");
            DropForeignKey("dbo.Review", "IdUser", "dbo.User");
            DropForeignKey("dbo.Review", "IdProduct", "dbo.Product");
            DropForeignKey("dbo.Image", "IdUser", "dbo.User");
            DropForeignKey("dbo.Product", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.Order", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.Image", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.Order", "IdPayment", "dbo.Payment");
            DropForeignKey("dbo.Image", "IdProduct", "dbo.Product");
            DropIndex("dbo.Transaction", new[] { "IdDeliveryStatus" });
            DropIndex("dbo.Transaction", new[] { "IdUser" });
            DropIndex("dbo.Review", new[] { "IdUser" });
            DropIndex("dbo.Review", new[] { "IdProduct" });
            DropIndex("dbo.User", new[] { "IdRole" });
            DropIndex("dbo.Shop", new[] { "IdUser" });
            DropIndex("dbo.Order", new[] { "IdPayment" });
            DropIndex("dbo.Order", new[] { "IdShop" });
            DropIndex("dbo.Order", new[] { "IdProduct" });
            DropIndex("dbo.Order", new[] { "IdTransaction" });
            DropIndex("dbo.Product", new[] { "IdProgramme" });
            DropIndex("dbo.Product", new[] { "IdShop" });
            DropIndex("dbo.Product", new[] { "IdCategory" });
            DropIndex("dbo.Image", new[] { "IdShop" });
            DropIndex("dbo.Image", new[] { "IdUser" });
            DropIndex("dbo.Image", new[] { "IdCategory" });
            DropIndex("dbo.Image", new[] { "IdProduct" });
            DropTable("dbo.Programme");
            DropTable("dbo.DeliveryStatus");
            DropTable("dbo.Transaction");
            DropTable("dbo.Role");
            DropTable("dbo.Review");
            DropTable("dbo.User");
            DropTable("dbo.Shop");
            DropTable("dbo.Payment");
            DropTable("dbo.Order");
            DropTable("dbo.Product");
            DropTable("dbo.Image");
            DropTable("dbo.Category");
        }
    }
}
