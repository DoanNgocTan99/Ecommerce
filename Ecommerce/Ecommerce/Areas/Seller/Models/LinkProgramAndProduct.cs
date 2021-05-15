using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Areas.Seller.Models
{
    public class LinkProgramAndProduct
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public string NameProduct { get; set; }
        public string NameUser { get; set; }
        public DateTime CreateDate { get; set; }

    }
}