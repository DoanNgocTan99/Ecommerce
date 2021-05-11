using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class CartItem
    {
        EcommerceContext db = new EcommerceContext();
        public int _IdProduct { get; set; }
        public string _ProductName { get; set; }
        public string _Image { get; set; }
        // public string _MauSac { get; set; }
        public decimal? _Price { get; set; }
        public int _Amount { get; set; }
        public decimal _Total
        {
            get { return _Amount * _Price.GetValueOrDefault(0); }
        }
        public CartItem(int __idproduct)
        {
            _IdProduct = __idproduct;
            Product sp = db.Products.Single(n => n.Id == __idproduct);
            _ProductName = sp.Name;
            Image img = new Image();
            img = db.Images.Where(x => x.IdProduct == __idproduct).FirstOrDefault();
            _Image = img.Path;
            _Price = sp.DelPrice;
            _Amount = 1;
        }
    }
}