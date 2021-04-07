namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductAdvertising
    {
        [Key]
        public long Id { get; set; }

        public long IdProduct { get; set; }

        public long IdShop { get; set; }

        public long IdImage { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        [Required]
        [StringLength(250)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? MreatedDate { get; set; }

        public virtual Image Image { get; set; }

        public virtual Product Product { get; set; }

        public virtual Shop Shop { get; set; }
    }
}
