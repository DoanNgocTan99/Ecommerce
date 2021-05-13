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
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Programme> Programmes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<DeliveryStatus> DeliveryStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //table Category
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Images)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.IdCategory);
            modelBuilder.Entity<Image>().Property(p => p.IdCategory).IsOptional();

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.IdCategory);


            //Table Image
            modelBuilder.Entity<Image>()
                .Property(e => e.Path)
                .IsUnicode(false);
            modelBuilder.Entity<Image>().Property(p => p.IdUser).IsOptional();
            modelBuilder.Entity<Image>().Property(p => p.IdShop).IsOptional();
            modelBuilder.Entity<Image>().Property(p => p.IdProduct).IsOptional();
            modelBuilder.Entity<Image>().Property(p => p.IdCategory).IsOptional();

            //Table Product
            //modelBuilder.Entity<Product>()
            //    .Property(e => e.Price)
            //    .HasPrecision(2, 0);
            modelBuilder.Entity<Transaction>()
                .Property(e => e.Amount)
                .HasPrecision(2, 0);
            //modelBuilder.Entity<Product>()
            //    .Property(e => e.DelPrice)
            //    .HasPrecision(2,);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Images)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.IdProduct);

            modelBuilder.Entity<Programme>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Programme)
                .HasForeignKey(e => e.IdProgramme);
            //.WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct);






            //Table Role
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.IdRole);


            //Table SHOP
            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.IdShop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.IdShop);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Shops)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Images)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.IdShop);

            //Transaction
            modelBuilder.Entity<Transaction>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Transaction)
                .HasForeignKey(e => e.IdTransaction);


            //USER
            modelBuilder.Entity<User>()
                .HasMany(e => e.Images)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser).WillCascadeOnDelete(true);
            //modelBuilder.Entity<U ser>().Property(p => p.IdShop).IsOptional();

        }
    }
}
