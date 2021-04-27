namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            Images = new HashSet<Image>();
            Transactions = new HashSet<Transaction>();
            Reviews = new HashSet<Review>();
            IsActive = true;
            IdRole = 2;
            
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Phone { get; set; }

        public bool? Gender { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [DefaultValue("True")]
        public bool? IsActive { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        //[Required]
        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }



        public long? IdRole { get; set; }
        [ForeignKey("IdRole")]
        public virtual Role Role { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
