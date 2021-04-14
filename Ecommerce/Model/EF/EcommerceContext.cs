using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EF
{
    public partial class EcommerceContext : DbContext
    {
        public EcommerceContext()
            : base("name=ECommerceContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAdvertising> ProductAdvertisings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Images)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.IdCategory);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.IdCategory);

            modelBuilder.Entity<Image>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<Image>()
                .HasMany(e => e.ProductAdvertisings)
                .WithRequired(e => e.Image)
                .HasForeignKey(e => e.IdImage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Total)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.IdOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Payment)
                .HasForeignKey(e => e.IdPayment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.DelPrice)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Images)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.IdProduct);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductAdvertisings)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductAdvertising>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.IdRole);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.IdShop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.IdShop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.IdShop);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.ProductAdvertisings)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.IdShop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.IdShop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.IdShop);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Images)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<User>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser);
        }
    }
}
