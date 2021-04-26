using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    [Table("DeliveryStatus")]
    public class DeliveryStatus
    {
        public DeliveryStatus()
        {
                Transactions = new HashSet<Transaction>();
        } 
        [Key]
        public long id { get; set; }

        [Required]
        [StringLength(250)]
        public string Status { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
