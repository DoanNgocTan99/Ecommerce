using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Areas.Seller.Models
{
    public class ViewOrderByUser
    {
        public long Id { get; set; }
        public string NameUser { get; set; }
        public string Address { get; set; }
        public string CheckoutStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Status { get; set; }
    }
}