using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Model.EF
{

    [Table("Order")]
    public partial class Order
    {


        [Key]
        public long Id { get; set; }
        public int Amount { get; set; }
        public long IdTransaction { get; set; }
        [ForeignKey("IdTransaction")]
        public virtual Transaction Transaction { get; set; }

        public long? IdProduct { get; set; }
        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }
        public long? IdShop { get; set; }
        [ForeignKey("IdShop")]
        public virtual Shop Shop { get; set; }

        public long? IdPayment { get; set; }
        [ForeignKey("IdPayment")]
        public virtual Payment Payment { get; set; }

        [StringLength(250)]
        public string Message { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }
        public decimal Price { get; set; }

        //[Required]
        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }



    }
}
