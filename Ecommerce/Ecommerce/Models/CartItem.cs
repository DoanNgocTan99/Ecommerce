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
        public decimal? _Price { get; set; }
        public decimal? _DelPrice { get; set; }
        public decimal? _PriceFirst { get; set; }
        public int? _Discount { get; set; }
        public int _Amount { get; set; }
        public long? _IdShop { get; set; }
        public DateTime? _ModifiedPriceDate { get; set; }
        public DateTime? _CreateDate { get; set; }
        public decimal _Total
        {
            //get { return _Amount * _DelPrice.GetValueOrDefault(0); }
            get { return _Amount * (decimal)(_Price - _Price * _Discount); }
        }
        public CartItem(int __idproduct)
        {
            var temp = (from a in db.Users
                        join b in db.Transactions
                        on a.Id equals b.IdUser
                        join c in db.Orders
                        on b.Id equals c.IdTransaction
                        where c.IdProduct == __idproduct
                        select c).FirstOrDefault();

            _IdProduct = __idproduct;
            Product sp = db.Products.Single(n => n.Id == __idproduct);
            _ModifiedPriceDate = sp.ModifiedPriceDate;
            _CreateDate = temp.CreatedDate;
            var check = DateTime.Compare((DateTime)_ModifiedPriceDate,(DateTime)_CreateDate);
            if(check < 0)
            {
                _Price = sp.Price;
            }
            else if( check > 0)
            {
                _Price = sp.PriceAfterChange;
            }
            _ProductName = sp.Name;
            Image img = new Image();
            img = db.Images.Where(x => x.IdProduct == __idproduct).FirstOrDefault();
            _Image = img.Path;
            _DelPrice = sp.DelPrice;
            _Amount = 1;
            _IdShop = sp.IdShop;
            _Discount = sp.Discount;
        }
    }
}