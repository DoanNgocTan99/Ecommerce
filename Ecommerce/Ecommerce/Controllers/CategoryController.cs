using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        EcommerceContext db = new EcommerceContext();


        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public PartialViewResult GetCategory()
        {
            List<Category> cats = db.Categories.ToList();
            return PartialView("GetCategory", cats);
        }
    }
}