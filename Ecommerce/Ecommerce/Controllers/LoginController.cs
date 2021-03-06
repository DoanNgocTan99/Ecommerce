using Ecommerce.Models;
using Ecommerce.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class LoginController : Controller
    {
        // GET: Seller/Loginq
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetByUserName(model.UserName);
                    var UserSession = new UserLogin();
                    UserSession.UserName = user.UserName;
                    UserSession.Id = user.Id;
                    UserSession.Name = user.Name;
                    UserSession.IdRole = user.IdRole;
                    UserSession.IdShop = dao.GetIdShopByIdUser(user.Id);
                    Session.Add(CommonConstants.USER_SESSION, UserSession);
                    var listCredentials = dao.GetListCredential(model.UserName);
                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                    Response.Cookies["IdUser"].Value = user.Id.ToString();
                    Session["Name"] = user.Name;
                    Session["IdUser"] = user.Id;
                    //Session[""] = 
                    if (UserSession.IdRole != 1)
                    {

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return new ViewResult
                        {
                            ViewName = "~/Areas/Admin/Views/Home/Index.cshtml"
                        };
                    }
                }
                else if (result == 0)
                {
                    ModelState.AddModelError(" ", "Tài Khoản Không Tồn Tại!! Hihi ^^! ");
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
            return View("Index");
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
                        ModelState.AddModelError("", "Xin lỗi!!! Bạn cần kiểm tra lại thông tin");
                        return View("Index");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Chúc mừng!! Bạn đã tạo thành công tài khoản :>");
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
            Session["Name"] = null;
            Session["IdUser"] = null;

            return RedirectToAction("index", "home");
        }
        //[HttpGet]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Register(User user)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var dao = new UserDAO();
        //            var entity = dao.SearchByUser(user);
        //            if (entity)
        //            {
        //                ModelState.AddModelError("", "Người dùng đã tồn tại");
        //                return View("Index");

        //            }
        //            if (!string.IsNullOrEmpty(user.Password))
        //            {
        //                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
        //                user.Password = encryptedMd5Pas;

        //            }
        //            var Id = dao.Insert(user);

        //            var shop = new ShopDAO();

        //            var IdShop = shop.InsertByRegister(Id);

        //            if (IdShop != 0)
        //            {
        //                ModelState.AddModelError("", "Xin lỗi!!! Bạn cần kiểm tra lại thông tin");
        //                return View("Index");

        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Chúc mừng!! Bạn đã tạo thành công tài khoản :>");
        //                return View("Index");

        //            }


        //        }

        //        return View("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
    }
}