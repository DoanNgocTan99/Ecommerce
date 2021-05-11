using Model.EF;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CartController : Controller
    {
        EcommerceContext db = new EcommerceContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult cart(string status = "")
        {

            List<CartItem> lstCart = GetCart();
            if (lstCart.Count() == 0)
            {
                ViewBag.cart = "Chưa có sản phẩm nào trong giỏ hàng!";
            }
            if (!String.IsNullOrEmpty(status))
            {
                ViewBag.cart = status; //thông báo mua hàng thành công
            }

            return View(lstCart);
        }
        public List<CartItem> GetCart()
        {
            List<CartItem> lstCart = Session["Cart"] as List<CartItem>;
            if (lstCart == null)
            {
                lstCart = new List<CartItem>();
                Session["Cart"] = lstCart;
            }
            return lstCart;
        }

        public int Cart_partial()
        {
            if (Session["Cart"] == null)
            {
                return 0;
            }
            List<CartItem> lstCart = GetCart();
            return lstCart.Count();
        }

        public ActionResult AddCart(int id)
        {
            Product product = db.Products.SingleOrDefault(n => n.Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<CartItem> lstCart = GetCart();
            CartItem _cartitem = lstCart.Find(n => n._IdProduct == id);
            if (_cartitem == null)
            {
                _cartitem = new CartItem(id);
                lstCart.Add(_cartitem);
                return RedirectToAction("cart", "cart");
            }
            else
            {
                _cartitem._Amount++;
                return RedirectToAction("cart", "cart");
            }
        }

        public ActionResult DeleteCart(int id)
        {
            Product product = db.Products.SingleOrDefault(n => n.Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<CartItem> lstCart = GetCart();
            CartItem _cart = lstCart.Find(n => n._IdProduct == id);
            if (_cart == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                lstCart.Remove(_cart);
                return RedirectToAction("cart", "cart");
            }
        }
    }
}