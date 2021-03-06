using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web;
namespace Model.EF
{


    [Table("Shop")]
    public partial class Shop
    {
        public Shop()
        {
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
            Images = new HashSet<Image>();
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        public long Id { get; set; }


        
        [StringLength(250)]
        [Display(Name = "Tên Shop")]
        public string Name { get; set; }


        [StringLength(250)]
        [Display(Name = "Mô tả")]

        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Địa chỉ")]

        public string Address { get; set; }

        [StringLength(250)]
        [Display(Name = "Số điện thoại")]

        public string Phone { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required]
        public long? IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        //public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
