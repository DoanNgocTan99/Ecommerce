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
                        Description = c.String(nullable: false, maxLength: 250),
                        IsActive = c.Boolean(nullable: false, defaultValue:true),
                        CreatedBy = c.String(maxLength: 250),
                        ModifiedBy = c.String(maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Image",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Path = c.String(nullable: false, maxLength: 500, unicode: false),
                    IdProduct = c.Long(nullable: true),
                    IdCategory = c.Long(nullable: true),
                    IdUser = c.Long(nullable: true),
                    IdShop = c.Long(nullable: true),
                    CreatedBy = c.String(maxLength: 250),
                    ModifiedBy = c.String(maxLength: 250),
                    ModifiedDate = c.DateTime(),
                    CreatedDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.IdProduct, cascadeDelete: true)
                .ForeignKey("dbo.Shop", t => t.IdShop, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.IdUser,cascadeDelete: true)
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
                        Description = c.String(nullable: false, maxLength: 250),
                        Brand = c.String(nullable: false, maxLength: 250),
                        Material = c.String(maxLength: 250),
                        Origin = c.String(nullable: false, maxLength: 250),
                        Price = c.Decimal(nullable: false, precision: 2, scale: 0),
                        DelPrice = c.Decimal(nullable: false, precision: 2, scale: 0),
                        WarrantyDate = c.DateTime(nullable: false),
                        Stock = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        Views = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 250),
                        ModifiedBy = c.String(maxLength: 250),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IdCategory = c.Long(nullable: false),
                        IdShop = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.IdShop)
                .ForeignKey("dbo.Category", t => t.IdCategory)
                .Index(t => t.IdCategory)
                .Index(t => t.IdShop);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdTransaction = c.Long(nullable: false),
                        IdProduct = c.Long(nullable: false),
                        IdShop = c.Long(nullable: false),
                        IdPayment = c.Long(nullable: false),
                        Message = c.String(maxLength: 250),
                        CreatedBy = c.String(maxLength: 250),
                        ModifiedBy = c.String(maxLength: 250),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payment", t => t.IdPayment, cascadeDelete: true)
                .ForeignKey("dbo.Shop", t => t.IdShop, cascadeDelete: true)
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
                        Name = c.String(nullable: false, maxLength: 250),
                        Type = c.String(maxLength: 250),
                        Allow = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 250),
                        ModifiedBy = c.String(maxLength: 250),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shop",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false, maxLength: 250),
                        Address = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(maxLength: 250),
                        ModifiedBy = c.String(maxLength: 250),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        UserName = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false, maxLength: 250),
                        Email = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 250),
                        Gender = c.Boolean(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Address = c.String(maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 250),
                        ModifiedBy = c.String(maxLength: 250),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IdRole = c.Long(nullable: false),
                        IdShop = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.IdRole, cascadeDelete: true)
                .ForeignKey("dbo.Shop", t => t.IdShop)
                .Index(t => t.IdRole)
                .Index(t => t.IdShop);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(maxLength: 250),
                        ModifiedBy = c.String(maxLength: 250),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Address = c.String(maxLength: 250),
                        Amount = c.String(maxLength: 250),
                        CheckoutStatus = c.String(maxLength: 250),
                        CreatedBy = c.String(maxLength: 250),
                        ModifiedBy = c.String(maxLength: 250),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IdUser = c.Long(nullable: false),
                        IdDeliveryStatus = c.Long(nullable: false),
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
                        Status = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ProductAdvertising",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdProduct = c.Long(nullable: false),
                        Content = c.String(unicode: false, storeType: "text"),
                        CreatedBy = c.String(maxLength: 250),
                        ModifiedBy = c.String(maxLength: 250),
                        ModifiedDate = c.DateTime(nullable: false),
                        MreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.IdProduct, cascadeDelete: true)
                .Index(t => t.IdProduct);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "IdCategory", "dbo.Category");
            DropForeignKey("dbo.Image", "IdCategory", "dbo.Category");
            DropForeignKey("dbo.ProductAdvertising", "IdProduct", "dbo.Product");
            DropForeignKey("dbo.Order", "IdProduct", "dbo.Product");
            DropForeignKey("dbo.User", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.Transaction", "IdUser", "dbo.User");
            DropForeignKey("dbo.Order", "IdTransaction", "dbo.Transaction");
            DropForeignKey("dbo.Transaction", "IdDeliveryStatus", "dbo.DeliveryStatus");
            DropForeignKey("dbo.User", "IdRole", "dbo.Role");
            DropForeignKey("dbo.Image", "IdUser", "dbo.User");
            DropForeignKey("dbo.Product", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.Order", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.Image", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.Order", "IdPayment", "dbo.Payment");
            DropForeignKey("dbo.Image", "IdProduct", "dbo.Product");
            DropIndex("dbo.ProductAdvertising", new[] { "IdProduct" });
            DropIndex("dbo.Transaction", new[] { "IdDeliveryStatus" });
            DropIndex("dbo.Transaction", new[] { "IdUser" });
            DropIndex("dbo.User", new[] { "IdShop" });
            DropIndex("dbo.User", new[] { "IdRole" });
            DropIndex("dbo.Order", new[] { "IdPayment" });
            DropIndex("dbo.Order", new[] { "IdShop" });
            DropIndex("dbo.Order", new[] { "IdProduct" });
            DropIndex("dbo.Order", new[] { "IdTransaction" });
            DropIndex("dbo.Product", new[] { "IdShop" });
            DropIndex("dbo.Product", new[] { "IdCategory" });
            DropIndex("dbo.Image", new[] { "IdShop" });
            DropIndex("dbo.Image", new[] { "IdUser" });
            DropIndex("dbo.Image", new[] { "IdCategory" });
            DropIndex("dbo.Image", new[] { "IdProduct" });
            DropTable("dbo.ProductAdvertising");
            DropTable("dbo.DeliveryStatus");
            DropTable("dbo.Transaction");
            DropTable("dbo.Role");
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
