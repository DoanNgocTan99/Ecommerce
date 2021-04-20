using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    public class AdminController : Controller
    {
        // GET: Seller/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}