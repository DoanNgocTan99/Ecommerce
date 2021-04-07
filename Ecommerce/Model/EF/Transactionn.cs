namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transactionn")]
    public partial class Transactionn
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [StringLength(250)]
        public string Amount { get; set; }

        [Required]
        [StringLength(250)]
        public string Status { get; set; }

        [Required]
        [StringLength(250)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long IdUser { get; set; }

        public long IdProduct { get; set; }

        public long IdShop { get; set; }

        public virtual Product Product { get; set; }

        public virtual Shop Shop { get; set; }

        public virtual User User { get; set; }
    }
}
