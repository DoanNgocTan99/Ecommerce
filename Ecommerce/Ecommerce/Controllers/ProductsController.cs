using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ProductsController : Controller
    {
        EcommerceContext db = new EcommerceContext();

        public ActionResult ProductList(int? page, int? pagenumber, int? category, List<Product> listProducts, int? category_id)
        {
            //phan trang pagedlist
            if (page == null) page = 1;
            int pageSize = pagenumber == null ? 9 : 10000;
            //int pageSize = 9;
            int pageNumber = (page ?? 1);
            List<Product> products = new List<Product>();
            ViewBag.categoryId = category_id;
            if (listProducts != null)
            {
                products = listProducts.ToList();
                ViewBag.products = products;
            }
            else
            {
                products = db.Products.OrderBy(x => x.Name).ToList();
                ViewBag.products = products;
            }
            return View(products.ToPagedList(pageNumber, pageSize));

        }

        // trang xem chi tiet
        public ActionResult XemChiTiet(int id)
        {
            Product product = new Product();
            product = db.Products.SingleOrDefault(x => x.Id == id);
            List<Product> productLQ = db.Products.Where(x => x.IdCategory == product.IdCategory).ToList();
            ViewBag.productLQ = productLQ;
            ViewBag.product = product;
            if (product == null)
            {
                return View("ThongBaoLoi");
            }
            ViewBag.category = db.Categories.SingleOrDefault(x => x.Id == product.IdCategory);
            return View(product);
        }
        public ActionResult Flashsale(int? categoryid, List<Product> listProducts, int? page, int? pagenumber)
        {
            //phan trang pagedlist
            if (page == null) page = 1;
            int pageSize = pagenumber == null ? 9 : 10000;
            //int pageSize = 9;
            int pageNumber = (page ?? 1);
            List<Product> products = new List<Product>();
            if (listProducts != null)
            {
                products = listProducts.ToList();
                ViewBag.products = products;
            }
            else
            {
                products = db.Products.Where(x => x.IdProgramme == 1).OrderByDescending(x => x.CreatedDate).ToList();
                ViewBag.products = products;
            }

            return PartialView(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ProductHot(int? categoryid, List<Product> listProducts, int? page, int? pagenumber)
        {
            //phan trang pagedlist
            if (page == null) page = 1;
            int pageSize = pagenumber == null ? 9 : 10000;
            //int pageSize = 9;
            int pageNumber = (page ?? 1);
            List<Product> products = new List<Product>();
            if (listProducts != null)
            {
                products = listProducts.ToList();
                ViewBag.products = products;
            }
            else
            {
                products = db.Products.Where(x => x.Rate > 10).OrderByDescending(x => x.CreatedDate).ToList();
                ViewBag.products = products;
            }

            return PartialView(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ProductNew(int? categoryid, List<Product> listProducts, int? page, int? pagenumber)
        {
            //phan trang pagedlist
            if (page == null) page = 1;
            int pageSize = pagenumber == null ? 9 : 10000;
            //int pageSize = 9;
            int pageNumber = (page ?? 1);
            List<Product> products = new List<Product>();
            DateTime today = DateTime.Now;
            List<Product> listproduct = db.Products.ToList();
            foreach (var item in listproduct)
            {
                if ((int)(today - (DateTime)item.CreatedDate).TotalDays < 2)
                {
                    products.Add(item);
                }
            }


            return PartialView(products.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult SanPhamView(int? categoryid, List<Product> listProducts, int? page, int? pagenumber)
        {
            //phan trang pagedlist
            if (page == null) page = 1;
            int pageSize = pagenumber == null ? 9 : 10000;
            //int pageSize = 9;
            int pageNumber = (page ?? 1);
            List<Product> products = new List<Product>();
            if (listProducts != null)
            {
                products = listProducts.ToList();
                ViewBag.products = products;
            }
            else
            {
                products = db.Products.Where(x => x.IdCategory == categoryid).OrderByDescending(x => x.CreatedDate).ToList();
                ViewBag.products = products;
            }

            return PartialView(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SanPhamViewShop(long? categoryid, long? shopid, List<Product> listProducts, int? page, int? pagenumber)
        {
            //phan trang pagedlist
            if (page == null) page = 1;
            int pageSize = pagenumber == null ? 9 : 10000;
            //int pageSize = 9;
            int pageNumber = (page ?? 1);
            List<Product> products = new List<Product>();
            if (listProducts != null)
            {
                products = listProducts.ToList();
                ViewBag.products = products;
            }
            else
            {
                products = db.Products.Where(x => x.IdCategory == categoryid && x.IdShop == shopid).OrderByDescending(x => x.CreatedDate).ToList();
                ViewBag.products = products;
            }

            return PartialView(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SanPhamViewDel(int? categoryid, List<Product> listProducts, int? page, int? pagenumber)
        {
            //phan trang pagedlist
            if (page == null) page = 1;
            int pageSize = pagenumber == null ? 9 : 10000;
            //int pageSize = 9;
            int pageNumber = (page ?? 1);
            List<Product> products = new List<Product>();
            if (listProducts != null)
            {
                products = listProducts.ToList();
                ViewBag.products = products;
            }
            else
            {
                products = db.Products.Where(x => x.IdCategory == categoryid && x.Discount > 10).OrderByDescending(x => x.CreatedDate).ToList();
                ViewBag.products = products;
            }

            return PartialView(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Search(string key)
        {
            List<Product> products = db.Products.Where(x => x.Name.ToLower().Contains(key.ToLower())).OrderBy(x => x.Id).ToList();

            return View(products);
        }
        public PartialViewResult SendCategoryIdShop(long Id, long shopid)
        {
            ViewBag.categoryId = Id;
            ViewBag.shopId = shopid;
            List<long> temp = (from a in db.Products
                               join b in db.Categories on a.IdCategory equals b.Id
                               where a.IdShop == shopid
                               select a.IdCategory).Distinct().ToList();
            List<Category> categoryList = new List<Category>();
            foreach (long item in temp)
            {
                Category category = db.Categories.Where(x => x.Id == item).FirstOrDefault();
                categoryList.Add(category);
            }
            return PartialView("SendCategoryIdShop", categoryList);
        }
        public PartialViewResult SendCategoryId(int Id)
        {
            ViewBag.categoryId = Id;
            List<Category> cats = db.Categories.ToList();
            return PartialView("SendCategoryId", cats);
        }
        public PartialViewResult SendCategoryIdDel(int Id)
        {
            ViewBag.categoryId = Id;
            List<Category> cats = db.Categories.ToList();
            return PartialView("SendCategoryIdDel", cats);
        }
    }
}