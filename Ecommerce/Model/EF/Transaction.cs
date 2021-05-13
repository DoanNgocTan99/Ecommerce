using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace Model.EF
{


    [Table("Transaction")]
    public partial class Transaction
    {
        public Transaction()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public long Id { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public decimal Amount { get; set; }

        [StringLength(250)]
        public string CheckoutStatus { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }

        public long IdDeliveryStatus { get; set; }
        [ForeignKey("IdDeliveryStatus")]
        public virtual DeliveryStatus DeliveryStatus { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
