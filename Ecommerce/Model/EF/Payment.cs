using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace Model.EF
{


    [Table("Payment")]
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        //[Required]
        [StringLength(250)]
        public string Type { get; set; }

        public bool Allow { get; set; }

        //[Required]
        [StringLength(250)]
        public string CreatedBy { get; set; }

        //[Required]
        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
