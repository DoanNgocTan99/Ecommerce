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
    public class LoginController : Controller
    {
        // GET: Seller/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDAO();
                //var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord),true);
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));

                if (result == 1)
                {
                    var user = dao.GetByUserName(model.UserName);
                    var UserSession = new UserLogin();
                    UserSession.UserName = user.UserName;
                    UserSession.IdRole = user.IdRole;
                    UserSession.Name = user.Name;
                    UserSession.Id = user.Id;
                    //UserSession.IdShop = user.
                    UserSession.IdShop = dao.GetIdShopByIdUser(user.Id);
                    //UserSession.IdShop = user.Is
                    var listCredentials = dao.GetListCredential(model.UserName);
                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(CommonConstants.USER_SESSION, UserSession);
                    if (UserSession.IdRole == 1)
                    {
                         return new ViewResult
                        {
                            ViewName = "~/Areas/Admin/Views/Home/Index.cshtml"
                        };
                    }
                    else if (UserSession.IdRole == 2)
                    {

                        return RedirectToAction("Index", "Home");

                    }
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài Khoản Không Tồn Tại!! Hihi ^^! ");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài Khoản Bị Khoá Rồi Nhé :> Hehe");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai Mật Khẩu Rồi :< Hic_hic");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập:< Hic_hic");

                }
            }
            //return RedirectToAction("Index", "Login");
            //return RedirectToAction("Index");
            return View("Index");
            //return View();
            //return View("Login");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new UserDAO();
                    var entity = dao.SearchByUser(user);
                    if (entity)
                    {
                        ModelState.AddModelError("", "Người dùng đã tồn tại");
                        return View("Index");

                    }
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                        user.Password = encryptedMd5Pas;

                    }
                    var Id = dao.Insert(user);

                    var shop = new ShopDAO();

                    var IdShop = shop.InsertByRegister(Id);

                    if (IdShop != 0)
                    {
                        ModelState.AddModelError("", "Chúc mừng!! Bạn đã tạo thành công tài khoản :>");
                        return View("Index");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Xin lỗi!!! Bạn cần kiểm tra lại thông tin");
                        return View("Index");

                    }


                }

                return View("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            //return View("Index");
            
            return new ViewResult
            {
                ViewName = "~/Areas/Seller/Views/Login/Index.cshtml"
            };

        }
    }
}