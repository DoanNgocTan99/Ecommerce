namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        public long Id { get; set; }

        public long IdOrder { get; set; }
        [ForeignKey("IdOrder")]
        public virtual Order Order { get; set; }

        public long IdProduct { get; set; }
        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }

        public long IdShop { get; set; }
        [ForeignKey("IdShop")]
        public virtual Shop Shop { get; set; }



        public long IdPayment { get; set; }
        [ForeignKey("IdPayment")]
        public virtual Payment Payment { get; set; }


        public long IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }



        [Required]
        [StringLength(250)]
        public string Messagee { get; set; }

        [Required]
        [StringLength(250)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
