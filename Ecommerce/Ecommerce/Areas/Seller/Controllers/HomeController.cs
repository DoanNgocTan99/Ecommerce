using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Seller/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}