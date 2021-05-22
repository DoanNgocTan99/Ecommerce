using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace Model.EF
{

    [Table("Programme")]
    public partial class Programme
    {
        public Programme()
        {
            Products = new HashSet<Product>();
            IsActive = false;
        }

        [Key]
        public long Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? DateEnd { get; set; }
        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime MreatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }


    }
}
