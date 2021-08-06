using Model.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class OrderController : Controller
    {
        EcommerceContext db = new EcommerceContext();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int? id)
        {
            ViewBag.Idtrans = id;
            Transaction trans = db.Transactions.Find(id);
            return View(trans);
        }

    }
}