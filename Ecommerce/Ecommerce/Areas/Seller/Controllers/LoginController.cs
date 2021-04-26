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
                //if(model == null)
                //{
                //    return View("Index");
                //}
                var dao = new UserDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetByUserName(model.UserName);
                    var UserSession = new UserLogin();
                    UserSession.UserName = user.UserName;
                    UserSession.UserID = user.Id;
                    UserSession.Name = user.Name;
                    Session.Add(CommonConstants.USER_SESSION, UserSession);
                    return RedirectToAction("Index", "Home");
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
                    var check = dao.Insert(user);
                    if (check > 1 )
                    {
                        //SetAlert("Cập nhập người dùng thành công!!", "success");

                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhập Người dùng không thành công");
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
            return View("Index");
        }
    }
}