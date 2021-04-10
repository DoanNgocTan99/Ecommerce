using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    public class ShopController : Controller
    {
        // GET: Seller/Shop
        public ActionResult Index()
        {
            return View();
        }
    }
}