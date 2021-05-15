using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web;
namespace Model.EF
{


    [Table("Image")]
    public partial class Image
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Đường Dẫn")]
        public string Path { get; set; }

        [Display(Name = "Loại Sản Phẩm")]
        public long? IdProduct { get; set; }
        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }

        [Display(Name = "Loại Doanh Mục")]
        public long? IdCategory { get; set; }
        [ForeignKey("IdCategory")]
        public virtual Category Category { get; set; }

        [Display(Name = "Người tạo")]
        public long? IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }

        [Display(Name = "Người tạo")]
        public long? IdShop { get; set; }
        [ForeignKey("IdShop")]
        public virtual Shop Shop { get; set; }


        [StringLength(250)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        [Display(Name = "Người chỉnh sửa")]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
