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
            List<Product> productList = db.Products.Where(e => e.IdShop == id).ToList();
            ViewBag.productList = productList;
            return View(shop);
        }
    }
}