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
            ViewBag.total = Total();
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

        public ActionResult LoadCart(List<CartItem> lstCart)
        {
            List<CartItem> cart = new List<CartItem>();
            if (lstCart == null)
            {
                cart = lstCart.ToList();
                ViewBag.cart = cart;
            }
            return PartialView();
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
        public decimal Total()
        {
            decimal _Total = 0;
            List<CartItem> lstCart = Session["Cart"] as List<CartItem>;
            if (lstCart != null)
            {
                _Total = lstCart.Sum(n => n._Total);
            }
            return _Total;
        }
        public ActionResult UpdateAmount(int id, int type)
        {
            Product product = db.Products.SingleOrDefault(n => n.Id == id);
            List<CartItem> lstCart = GetCart();
            CartItem _cart = lstCart.Find(n => n._IdProduct == id);
            if (_cart == null)
            {
                _cart = new CartItem(id);
                lstCart.Add(_cart);
                return RedirectToAction("cart", "cart");
            }
            else
            {
                if (type == 1)
                {
                    _cart._Amount++;
                }
                if (_cart._Amount > 0 && type == 2)
                {
                    _cart._Amount--;
                }
                return RedirectToAction("cart", "cart");
            }
        }
        [HttpPost]
        public ActionResult Transaction()
        {
            if ((Session["IdUser"] == null) || (Session["IdUser"].ToString() == ""))
            {
                return RedirectToAction("login", "login", new { alert = "Bạn cần đăng nhập khi đặt hàng !" });
            }
            if (Session["Cart"] == null)
            {
                RedirectToAction("cart", "cart");
            }
            Transaction trans = new Transaction();
            List<CartItem> cart = GetCart();
            //User user = (User)Session["IdUser"];
            trans.IdUser = (int)Session["IdUser"];
            trans.Amount = Convert.ToInt32(Total());

            trans.CheckoutStatus = "Chua thanh toan";
            trans.CreatedBy = (string)Session["Name"];
            trans.CreatedDate = DateTime.Now;
            db.Transactions.Add(trans);
            db.SaveChanges();
            foreach (var item in cart)
            {
                Order order = new Order();
                order.IdTransaction = trans.Id;
                order.IdProduct = item._IdProduct;
                order.IdShop = item._IdShop;
                order.CreatedDate = DateTime.Now;
                order.ModifiedDate = DateTime.Now;
                db.Orders.Add(order);
            }
            db.SaveChanges();
            Session["cart"] = null;
            return RedirectToAction("cart", "cart", new { status = "Đặt hàng thành công!!!" });
        }
        public ActionResult Checkout()
        {
            return View();
        }
    }
}