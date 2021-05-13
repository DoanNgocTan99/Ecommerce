using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Areas.Seller.Models
{
    public class MyOrder
    {
        public long Id { get; set; }
        public string NameProduct { get; set; }
        public string NameUser { get; set; }
        public string Amount { get; set; }
        public string Message { get; set; }
        public string CheckoutStatus { get; set; }
        public string Address { get; set; }
        public string DeliveryStatus { get; set; }
        public string NamePayment { get; set; }
        public decimal Price { get; set; }
    }
}