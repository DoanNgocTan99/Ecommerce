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
        public PartialViewResult GetCategoryShop(int id)
        {
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

            return PartialView("GetCategoryShop", categoryList);
        }

    }
}