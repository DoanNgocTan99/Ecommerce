namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstCreateDb : DbMigration
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
                        IsActive = c.Boolean(nullable: false),
                        CategoryCol = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Path = c.String(nullable: false, maxLength: 250, unicode: false),
                        IdProduct = c.Long(),
                        IdCategory = c.Long(),
                        IdUser = c.Long(),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.IdProduct)
                .ForeignKey("dbo.User", t => t.IdUser)
                .ForeignKey("dbo.Category", t => t.IdCategory)
                .Index(t => t.IdProduct)
                .Index(t => t.IdCategory)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false, maxLength: 250),
                        Brand = c.String(nullable: false, maxLength: 250),
                        Material = c.String(nullable: false, maxLength: 250),
                        Origin = c.String(nullable: false, maxLength: 250),
                        Price = c.Decimal(nullable: false, precision: 2, scale: 0),
                        DelPrice = c.Decimal(nullable: false, precision: 2, scale: 0),
                        WarrantyDate = c.DateTime(),
                        Stock = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        Views = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        IdCategory = c.Long(nullable: false),
                        IdShop = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.IdShop, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.IdCategory, cascadeDelete: true)
                .Index(t => t.IdCategory)
                .Index(t => t.IdShop);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdOrder = c.Long(nullable: false),
                        IdProduct = c.Long(nullable: false),
                        IdShop = c.Long(nullable: false),
                        IdPayment = c.Long(nullable: false),
                        IdUser = c.Long(nullable: false),
                        Messagee = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.IdOrder)
                .ForeignKey("dbo.Shop", t => t.IdShop)
                .ForeignKey("dbo.User", t => t.IdUser)
                .ForeignKey("dbo.Payment", t => t.IdPayment)
                .ForeignKey("dbo.Product", t => t.IdProduct)
                .Index(t => t.IdOrder)
                .Index(t => t.IdProduct)
                .Index(t => t.IdShop)
                .Index(t => t.IdPayment)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Total = c.Decimal(precision: 2, scale: 0),
                        IsDelivery = c.Boolean(),
                        OrderCol = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        IdShop = c.Long(nullable: false),
                        IdUser = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.IdShop)
                .ForeignKey("dbo.User", t => t.IdUser)
                .Index(t => t.IdShop)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Shop",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false, maxLength: 250),
                        Address = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductAdvertising",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdProduct = c.Long(nullable: false),
                        IdShop = c.Long(nullable: false),
                        IdImage = c.Long(nullable: false),
                        Content = c.String(unicode: false, storeType: "text"),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        MreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.IdShop)
                .ForeignKey("dbo.Product", t => t.IdProduct)
                .ForeignKey("dbo.Image", t => t.IdImage)
                .Index(t => t.IdProduct)
                .Index(t => t.IdShop)
                .Index(t => t.IdImage);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 250),
                        Amount = c.String(nullable: false, maxLength: 250),
                        Status = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        IdUser = c.Long(nullable: false),
                        IdProduct = c.Long(nullable: false),
                        IdShop = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdUser, cascadeDelete: true)
                .ForeignKey("dbo.Shop", t => t.IdShop)
                .ForeignKey("dbo.Product", t => t.IdProduct)
                .Index(t => t.IdUser)
                .Index(t => t.IdProduct)
                .Index(t => t.IdShop);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        UserName = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false, maxLength: 250),
                        Gender = c.Boolean(nullable: false),
                        DOB = c.DateTime(),
                        Address = c.String(nullable: false, maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        IdRole = c.Long(nullable: false),
                        IdShop = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.IdRole, cascadeDelete: true)
                .ForeignKey("dbo.Shop", t => t.IdShop, cascadeDelete: true)
                .Index(t => t.IdRole)
                .Index(t => t.IdShop);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nam = c.String(nullable: false, maxLength: 250),
                        Type = c.String(nullable: false, maxLength: 250),
                        Allow = c.Boolean(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "IdCategory", "dbo.Category");
            DropForeignKey("dbo.Image", "IdCategory", "dbo.Category");
            DropForeignKey("dbo.ProductAdvertising", "IdImage", "dbo.Image");
            DropForeignKey("dbo.Transaction", "IdProduct", "dbo.Product");
            DropForeignKey("dbo.ProductAdvertising", "IdProduct", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "IdProduct", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "IdPayment", "dbo.Payment");
            DropForeignKey("dbo.User", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.Transaction", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.Transaction", "IdUser", "dbo.User");
            DropForeignKey("dbo.User", "IdRole", "dbo.Role");
            DropForeignKey("dbo.Order", "IdUser", "dbo.User");
            DropForeignKey("dbo.OrderDetail", "IdUser", "dbo.User");
            DropForeignKey("dbo.Image", "IdUser", "dbo.User");
            DropForeignKey("dbo.Product", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.ProductAdvertising", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.Order", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.OrderDetail", "IdShop", "dbo.Shop");
            DropForeignKey("dbo.OrderDetail", "IdOrder", "dbo.Order");
            DropForeignKey("dbo.Image", "IdProduct", "dbo.Product");
            DropIndex("dbo.User", new[] { "IdShop" });
            DropIndex("dbo.User", new[] { "IdRole" });
            DropIndex("dbo.Transaction", new[] { "IdShop" });
            DropIndex("dbo.Transaction", new[] { "IdProduct" });
            DropIndex("dbo.Transaction", new[] { "IdUser" });
            DropIndex("dbo.ProductAdvertising", new[] { "IdImage" });
            DropIndex("dbo.ProductAdvertising", new[] { "IdShop" });
            DropIndex("dbo.ProductAdvertising", new[] { "IdProduct" });
            DropIndex("dbo.Order", new[] { "IdUser" });
            DropIndex("dbo.Order", new[] { "IdShop" });
            DropIndex("dbo.OrderDetail", new[] { "IdUser" });
            DropIndex("dbo.OrderDetail", new[] { "IdPayment" });
            DropIndex("dbo.OrderDetail", new[] { "IdShop" });
            DropIndex("dbo.OrderDetail", new[] { "IdProduct" });
            DropIndex("dbo.OrderDetail", new[] { "IdOrder" });
            DropIndex("dbo.Product", new[] { "IdShop" });
            DropIndex("dbo.Product", new[] { "IdCategory" });
            DropIndex("dbo.Image", new[] { "IdUser" });
            DropIndex("dbo.Image", new[] { "IdCategory" });
            DropIndex("dbo.Image", new[] { "IdProduct" });
            DropTable("dbo.Payment");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Transaction");
            DropTable("dbo.ProductAdvertising");
            DropTable("dbo.Shop");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Product");
            DropTable("dbo.Image");
            DropTable("dbo.Category");
        }
    }
}
