using Ecommerce.Models;
using Ecommerce.Common;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace Ecommerce.Controllers
{
    public class ShopController : Controller
    {
        EcommerceContext db = new EcommerceContext();
        // GET: Seller/Loginq
        public ActionResult Index(int id)
        {
            Shop shop = new Shop();
            shop = db.Shops.SingleOrDefault(x => x.Id == id);
            ViewBag.shop = shop;
            ViewBag.shopId = id;
            if (shop == null)
            {
                return View("ThongBaoLoi");
            }
            List<long> temp = (from a in db.Products
                               join b in db.Categories on a.IdCategory equals b.Id
                               where a.IdShop == id
                               select a.IdCategory).Distinct().ToList();
            List<Category> categoryList = new List<Category>();
            foreach (long item in temp)
            {
                Category category = db.Categories.Where(x => x.Id == item).FirstOrDefault();
                categoryList.Add(category);
            }
            ViewBag.categoryFirst = categoryList[0].Id;
            List<Product> productList = db.Products.Where(e => e.IdShop == id).ToList();
            ViewBag.productList = productList;   
            return View(shop);
        }
    }
}