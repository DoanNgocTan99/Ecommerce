using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class SearchController : Controller
    {
        EcommerceContext db = new EcommerceContext();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string key)
        {
            List<Product> products = db.Products.Where(x => x.Name.ToLower().Contains(key.ToLower())).OrderBy(x => x.Id).ToList();

            return View(products);
        }
    }
}