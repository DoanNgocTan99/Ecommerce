namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Images = new HashSet<Image>();
            Products = new HashSet<Product>();
            IsActive = true;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Tên Doanh Mục")]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Mô Tả")]
        public string Description { get; set; }

        [Display(Name = "Trạng Thái")]
        [DefaultValue("true")]
        public bool IsActive { get; set; }


        [StringLength(250)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        [Display(Name = "Người chỉnh sửa")]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }


        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
