namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                        Path = c.String(nullable: false, maxLength: 250),
                        IdProduct = c.Long(),
                        IdCategory = c.Long(),
                        IdUser = c.Long(),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        Category_Id = c.Long(),
                        Product_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.User_Id);
            
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
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DelPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WarrantyDate = c.DateTime(),
                        Stock = c.Int(),
                        Discount = c.Int(),
                        Views = c.Int(),
                        Rate = c.Int(),
                        IsActive = c.Boolean(),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        IdCategory = c.Long(nullable: false),
                        IdShop = c.Long(nullable: false),
                        Category_Id = c.Long(),
                        Shop_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .ForeignKey("dbo.Shop", t => t.Shop_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Shop_Id);
            
            CreateTable(
                "dbo.OrderDetails",
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
                        Order_Id = c.Long(),
                        Shop_Id = c.Long(),
                        User_Id = c.Long(),
                        Payment_Id = c.Long(),
                        Product_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Order_Id)
                .ForeignKey("dbo.Shop", t => t.Shop_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Payment", t => t.Payment_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Shop_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Payment_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Total = c.Decimal(precision: 18, scale: 2),
                        IsDelivery = c.Boolean(),
                        OrderCol = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        IdShop = c.Long(nullable: false),
                        IdUser = c.Long(nullable: false),
                        Shop_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.Shop_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Shop_Id)
                .Index(t => t.User_Id);
            
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
                "dbo.ProductAdvertisings",
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
                        Image_Id = c.Long(),
                        Product_Id = c.Long(),
                        Shop_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Image", t => t.Image_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .ForeignKey("dbo.Shop", t => t.Shop_Id)
                .Index(t => t.Image_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Shop_Id);
            
            CreateTable(
                "dbo.Transactionn",
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
                        Product_Id = c.Long(),
                        Shop_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .ForeignKey("dbo.Shop", t => t.Shop_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Shop_Id)
                .Index(t => t.User_Id);
            
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
                        Gender = c.Boolean(),
                        DOB = c.DateTime(),
                        Address = c.String(nullable: false, maxLength: 250),
                        IsActive = c.Boolean(),
                        CreatedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedBy = c.String(nullable: false, maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        IdRole = c.Long(nullable: false),
                        IdShop = c.Long(nullable: false),
                        Role_Id = c.Long(),
                        Shop_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.Role_Id)
                .ForeignKey("dbo.Shop", t => t.Shop_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.Shop_Id);
            
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
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.OrderDetails", "Payment_Id", "dbo.Payment");
            DropForeignKey("dbo.Transactionn", "User_Id", "dbo.User");
            DropForeignKey("dbo.User", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.User", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.Order", "User_Id", "dbo.User");
            DropForeignKey("dbo.OrderDetails", "User_Id", "dbo.User");
            DropForeignKey("dbo.Image", "User_Id", "dbo.User");
            DropForeignKey("dbo.Transactionn", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.Transactionn", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.ProductAdvertisings", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.ProductAdvertisings", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.ProductAdvertisings", "Image_Id", "dbo.Image");
            DropForeignKey("dbo.Product", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.OrderDetails", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.Order", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Order");
            DropForeignKey("dbo.Image", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Product", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.Image", "Category_Id", "dbo.Category");
            DropIndex("dbo.User", new[] { "Shop_Id" });
            DropIndex("dbo.User", new[] { "Role_Id" });
            DropIndex("dbo.Transactionn", new[] { "User_Id" });
            DropIndex("dbo.Transactionn", new[] { "Shop_Id" });
            DropIndex("dbo.Transactionn", new[] { "Product_Id" });
            DropIndex("dbo.ProductAdvertisings", new[] { "Shop_Id" });
            DropIndex("dbo.ProductAdvertisings", new[] { "Product_Id" });
            DropIndex("dbo.ProductAdvertisings", new[] { "Image_Id" });
            DropIndex("dbo.Order", new[] { "User_Id" });
            DropIndex("dbo.Order", new[] { "Shop_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Payment_Id" });
            DropIndex("dbo.OrderDetails", new[] { "User_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Shop_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.Product", new[] { "Shop_Id" });
            DropIndex("dbo.Product", new[] { "Category_Id" });
            DropIndex("dbo.Image", new[] { "User_Id" });
            DropIndex("dbo.Image", new[] { "Product_Id" });
            DropIndex("dbo.Image", new[] { "Category_Id" });
            DropTable("dbo.Payment");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Transactionn");
            DropTable("dbo.ProductAdvertisings");
            DropTable("dbo.Shop");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Product");
            DropTable("dbo.Image");
            DropTable("dbo.Category");
        }
    }
}
