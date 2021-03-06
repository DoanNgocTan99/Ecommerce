using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web;

namespace Model.EF
{

    [Table("Product")]
    public partial class Product
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Images = new HashSet<Image>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            IsActive = true;
            FlaseSale = false;
            Advertisement = false;

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
        public int Price { get; set; }

        [Display(Name = "Giá giảm")]
        public int? DelPrice { get; set; }


        public DateTime? WarrantyDate { get; set; }
        public int Warranty { get; set; }

        public int? Stock { get; set; }

        [Display(Name = "Giá giảm của SHOP")]
        public int? Discount { get; set; }
        [Display(Name = "Lượt xem")]

        public int? Views { get; set; }

        public int? Rate { get; set; }

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

        public DateTime? ModifiedDate { get; set; }
        public DateTime? ModifiedPriceDate { get; set; }
        public int? PriceAfterChange { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateAddFlaseSale { get; set; }
        public DateTime? DateAddAdvertisement { get; set; }

        [Display(Name = "Loại Doanh Mục")]
        public long IdCategory { get; set; }
        [ForeignKey("IdCategory")]
        public virtual Category Category { get; set; }

        [Display(Name = "Thuộc SHOP")]
        public long? IdShop { get; set; }
        [ForeignKey("IdShop")]
        public virtual Shop Shop { get; set; }

        public long? IdProgramme { get; set; }
        [ForeignKey("IdProgramme")]
        public virtual Programme Programme { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        public bool FlaseSale { get; set; }
        public bool Advertisement { get; set; }

    }
}
