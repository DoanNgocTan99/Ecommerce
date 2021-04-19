namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Image")]
    public partial class Image
    {
        public Image()
        {
            ProductAdvertisings = new HashSet<ProductAdvertising>();
        }
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name ="Đường Dẫn")]
        public string Path { get; set; }

        [Display(Name = "Loại Sản Phẩm")]
        public long IdProduct { get; set; }
        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }

        [Display(Name = "Loại Doanh Mục")]
        public long IdCategory { get; set; }
        [ForeignKey("IdCategory")]
        public virtual Category Category { get; set; }

        [Display(Name = "Người tạo")]
        public long IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }


        [Required]
        [StringLength(250)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Người chỉnh sửa")]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductAdvertising> ProductAdvertisings { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
