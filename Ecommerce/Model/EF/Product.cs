﻿namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Images = new HashSet<Image>();
            ProductAdvertisings = new HashSet<ProductAdvertising>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Thương hiệu")]
        [Required]
        [StringLength(250)]
        public string Brand { get; set; }

        [Display(Name = "Chất liệu")]
        //[Required]
        [StringLength(250)]
        public string Material { get; set; }

        [Display(Name = "Nơi Sản Xuất")]
        [Required]
        [StringLength(250)]
        public string Origin { get; set; }

        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Display(Name = "Giá giảm")]
        public decimal DelPrice { get; set; }


        public DateTime WarrantyDate { get; set; }


        public int Stock { get; set; }

        [Display(Name = "Giá giảm của SHOP")]
        public int Discount { get; set; }
        [Display(Name = "Lượt xem")]

        public int Views { get; set; }

        public int Rate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }

        //[Required]
        [StringLength(250)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }

        //[Required]
        [StringLength(250)]
        [Display(Name = "Người chỉnh sửa")]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Loại Doanh Mục")]
        public long IdCategory { get; set; }
        [ForeignKey("IdCategory")]
        public virtual Category Category { get; set; }

        [Display(Name = "Thuộc SHOP")]
        public long? IdShop { get; set; }
        [ForeignKey("IdShop")]
        public virtual Shop Shop { get; set; }


        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductAdvertising> ProductAdvertisings { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}