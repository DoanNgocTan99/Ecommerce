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
            List<Product> sanPhamGiamGia = db.Products.Where(x => x.IdCategory == product.IdCategory && x.Discount > 20 == true).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
            ViewBag.sanPhamGiamGia = sanPhamGiamGia;
            // hiển thị quảng cáo
            List<ProductAdvertising> advertising = db.ProductAdvertisings.OrderBy(x => x.Id).Take(1).ToList();
            ViewBag.advertising = advertising;
            //List<ProductAdvertising> advertising2 = db.ProductAdvertisings.OrderByDescending(x => x.Id).Take(1).ToList();
            //ViewBag.advertising1 = advertising2;

            //list flash sale
            List<Product> flashsale = db.Products.Where(x => x.IdCategory == 3 == true).OrderBy(x => Guid.NewGuid()).Take(5).ToList();
            ViewBag.flashsale = flashsale;

            //list sản phẩm bán chạy
            List<Product> hotproduct = db.Products.Where(x => x.IdCategory == 4 == true).OrderBy(x => Guid.NewGuid()).Take(6).ToList();
            ViewBag.hotproduct = hotproduct;
            //list sản phẩm mới
            List<Product> newproduct = db.Products.Where(x => x.IdCategory == 5 == true).OrderBy(x => Guid.NewGuid()).Take(6).ToList();
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