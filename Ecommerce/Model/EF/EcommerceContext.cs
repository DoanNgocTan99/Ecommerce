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
        public virtual DbSet<Transactionn> Transactionns { get; set; }
        public virtual DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>()
        //        .HasMany(e => e.images)
        //        .WithOptional(e => e.Category)
        //        .HasForeignKey(e => e.cate_id_img);

        //    modelBuilder.Entity<Category>()
        //        .HasMany(e => e.Products)
        //        .WithRequired(e => e.Category)
        //        .HasForeignKey(e => e.cate_id_prod);

        //    modelBuilder.Entity<Image>()
        //        .Property(e => e.path)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Image>()
        //        .HasMany(e => e.product_advertising)
        //        .WithRequired(e => e.image)
        //        .HasForeignKey(e => e.img_id_adver)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Order>()
        //        .Property(e => e.total)
        //        .HasPrecision(2, 0);

        //    modelBuilder.Entity<Order>()
        //        .HasMany(e => e.order_detail)
        //        .WithRequired(e => e.Orderr)
        //        .HasForeignKey(e => e.order_id_detail)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Payment>()
        //        .HasMany(e => e.order_detail)
        //        .WithRequired(e => e.payment)
        //        .HasForeignKey(e => e.payment_id_detail)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Product>()
        //        .Property(e => e.prod_price)
        //        .HasPrecision(2, 0);

        //    modelBuilder.Entity<Product>()
        //        .Property(e => e.del_price)
        //        .HasPrecision(2, 0);

        //    modelBuilder.Entity<Product>()
        //        .HasMany(e => e.images)
        //        .WithOptional(e => e.Product)
        //        .HasForeignKey(e => e.prod_id_img);

        //    modelBuilder.Entity<Product>()
        //        .HasMany(e => e.order_detail)
        //        .WithRequired(e => e.Product)
        //        .HasForeignKey(e => e.prod_id_detail)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Product>()
        //        .HasMany(e => e.product_advertising)
        //        .WithRequired(e => e.Product)
        //        .HasForeignKey(e => e.prod_id_adver)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Product>()
        //        .HasMany(e => e.transactionns)
        //        .WithRequired(e => e.Product)
        //        .HasForeignKey(e => e.prod_id_transaction)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<ProductAdvertising>()
        //        .Property(e => e.content)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Role>()
        //        .HasMany(e => e.Userrs)
        //        .WithRequired(e => e.role)
        //        .HasForeignKey(e => e.role_id_user);

        //    modelBuilder.Entity<Shop>()
        //        .HasMany(e => e.order_detail)
        //        .WithRequired(e => e.shop)
        //        .HasForeignKey(e => e.shop_id_detail)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Shop>()
        //        .HasMany(e => e.Orderrs)
        //        .WithRequired(e => e.shop)
        //        .HasForeignKey(e => e.shop_id_order)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Shop>()
        //        .HasMany(e => e.Products)
        //        .WithRequired(e => e.shop)
        //        .HasForeignKey(e => e.shop_id_prod);

        //    modelBuilder.Entity<Shop>()
        //        .HasMany(e => e.product_advertising)
        //        .WithRequired(e => e.shop)
        //        .HasForeignKey(e => e.shop_id_adver)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Shop>()
        //        .HasMany(e => e.transactionns)
        //        .WithRequired(e => e.shop)
        //        .HasForeignKey(e => e.shop_id_transaction)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Shop>()
        //        .HasMany(e => e.Userrs)
        //        .WithRequired(e => e.shop)
        //        .HasForeignKey(e => e.shop_id_user);

        //    modelBuilder.Entity<User>()
        //        .HasMany(e => e.images)
        //        .WithOptional(e => e.Userr)
        //        .HasForeignKey(e => e.user_id_img);

        //    modelBuilder.Entity<User>()
        //        .HasMany(e => e.order_detail)
        //        .WithRequired(e => e.Userr)
        //        .HasForeignKey(e => e.user_id_detail)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<User>()
        //        .HasMany(e => e.Orderrs)
        //        .WithRequired(e => e.Userr)
        //        .HasForeignKey(e => e.user_id_order)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<User>()
        //        .HasMany(e => e.transactionns)
        //        .WithRequired(e => e.Userr)
        //        .HasForeignKey(e => e.user_id_transaction);
        //}
    }
}
