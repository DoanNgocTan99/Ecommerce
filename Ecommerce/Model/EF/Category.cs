namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Images = new HashSet<Image>();
            Products = new HashSet<Product>();
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
        public bool IsActive { get; set; }

        //[Required]
        [StringLength(250)]
        public string CategoryCol { get; set; }

        //[Required]
        [StringLength(250)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }

        //[Required]
        [StringLength(250)]
        [Display(Name = "Người chỉnh sửa")]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
