using Ecommerce.Areas.Seller.Models;
using Ecommerce.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    public class HomeController : Controller
    {
        // GET: Seller/Home
        [HttpGet]
        [HasCredential(RoleID = "USER")]
        public ActionResult Index()
        {
            if (Session != null)
            {
                var product = new ProductDAO().GetProducts();
                ViewBag.Data = product;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        
    }
}