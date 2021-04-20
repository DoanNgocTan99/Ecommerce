namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            Images = new HashSet<Image>();
            OrderDetails = new HashSet<OrderDetail>();
            Orders = new HashSet<Order>();
            Transactions = new HashSet<Transaction>();
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

        //[Required]
        [StringLength(250)]
        public string Email { get; set; }

        //[Required]
        [StringLength(250)]
        public string Phone { get; set; }

        public bool Gender { get; set; }

        public DateTime? DOB { get; set; }

        //[Required]
        [StringLength(250)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        //[Required]
        [StringLength(250)]
        public string CreatedBy { get; set; }

        //[Required]
        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public long IdRole { get; set; }
        [ForeignKey("IdRole")]
        public virtual Role Role { get; set; }

        public long IdShop { get; set; }
        [ForeignKey("IdShop")]
        public virtual Shop Shop { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
