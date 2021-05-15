using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        EcommerceContext db = new EcommerceContext();
        public ActionResult Index()
        {
            Product product = new Product();
            //list sản phẩm giảm giá
            List<Product> sanPhamGiamGia = db.Products.Where(x => x.IdCategory == product.IdCategory && x.Discount > 10 == true).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
            ViewBag.sanPhamGiamGia = sanPhamGiamGia;
            // hiển thị quảng cáo
            List<Product> advertising = db.Products.OrderBy(x => x.IdProgramme == 2).Take(3).ToList();
            ViewBag.advertising = advertising;
            //List<ProductAdvertising> advertising2 = db.ProductAdvertisings.OrderByDescending(x => x.Id).Take(1).ToList();
            //ViewBag.advertising1 = advertising2;

            //list flash sale
            List<Product> flashsale = db.Products.Where(x => x.IdProgramme == 1).OrderBy(x => Guid.NewGuid()).Take(3).ToList();
            ViewBag.flashsale = flashsale;

            //list sản phẩm bán chạy
            List<Product> hotproduct = db.Products.Where(x => x.Rate > 10 == true).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
            ViewBag.hotproduct = hotproduct;
            //list sản phẩm mới
            DateTime today = DateTime.Now;
            List<Product> listproduct = db.Products.ToList();
            List<Product> newproduct = new List<Product>();
            foreach (var item in listproduct)
            {
                if ((int)(today - (DateTime)item.CreatedDate).TotalDays < 2)
                {
                    newproduct.Add(item);
                }
            }
            //foreach(var item in listproduct)
            //{
            //    if((int)(today-pro)
            //}
            ViewBag.newproduct = newproduct;

            //Cart

            return View();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Security()
        {
            ViewBag.Message = "Your security page.";

            return View();
        }
    }
}