using Ecommerce.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class UserManageController : Seller.Controllers.BaseController
    {
        // GET: Admin/UserManage
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [HasCredential(RoleID = "ADMIN")]

        [ValidateInput(false)]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var entity = dao.SearchByUser(user);
                if (entity)
                {
                    ModelState.AddModelError("", "Người dùng đã tồn tại");
                    return RedirectToAction("Index", "UserManage");

                }
                //var UserDao = new UserDAO();
                string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                //return type file
                string extension = Path.GetExtension(user.ImageFile.FileName);

                //return file name 
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string temp = fileName;
                //category.Path = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/User"), fileName);
                string filename = fileName.Substring(fileName.Length - (12 + temp.Length), (12 + temp.Length));

                user.ImageFile.SaveAs(fileName);
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;

                }
                long id = dao.Insert(user);

                var ImageDao = new ImageDAO();

                Image image = new Image();

                image.IdUser = id;
                image.Path = filename;

                long ID = ImageDao.Insert(image);

                var shop = new ShopDAO();

                var IdShop = shop.InsertByRegister(user.Id);

                if (IdShop != 0)
                {
                    ModelState.AddModelError("", "Người dùng chưa được tạo");
                    return RedirectToAction("Index", "UserManage");

                }
                else
                {
                    SetAlert("Tạo thành công người dùng!!", "success");
                    return RedirectToAction("Index", "UserManage");

                }
                //if (ID > 0)
                //{
                //    SetAlert("Tạo thành công người dùng!!", "success");
                //    return RedirectToAction("Index", "UserManage");

                //}
                //else
                //{
                //    ModelState.AddModelError("", "Người dùng chưa được tạo");
                //}

            }
            SetViewBag();
            return View("Index");
        }
        [HttpGet]
        [HasCredential(RoleID = "ADMIN")]

        public ActionResult Edit(int id)
        {
            var user = new UserDAO().ViewDetail(id);
            SetViewBag(user.IdRole);
            return View(user);
        }
        [HttpPost]
        [HasCredential(RoleID = "ADMIN")]

        [ValidateInput(false)]
        public ActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                var dao = new UserDAO();
                var ImageDao = new ImageDAO();
                if (user.ImageFile != null)
                {
                    //var UserDao = new UserDAO();
                    string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                    //return type file
                    string extension = Path.GetExtension(user.ImageFile.FileName);

                    //return file name 
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    //category.Path = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/User"), fileName);
                    user.ImageFile.SaveAs(fileName);

                    //var session = (UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];

                    //var idShop = UserDao.GetIdShopByIdUser(session.Id);
                    //product.IdShop = idShop;

                    var result = dao.UpdateByAdmin(user);


                    Image image = new Image();
                    image.IdUser = user.Id;
                    image.Path = fileName;
                    var ID = ImageDao.UpdateByIdUser(image);
                    if (ID)
                    {
                        SetAlert("Cập nhập thành công người dùng!!", "success");

                        return RedirectToAction("Index", "UserManage");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhập không thành công người dùng!");
                    }
                }
                else
                {
                    var result = dao.UpdateByAdmin(user);
                    if (result)
                    {
                        SetAlert("Cập nhập thành công người dùng!!", "success");

                        return RedirectToAction("Index", "UserManage");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhập không thành công người dùng!");
                    }
                }
            }
            SetViewBag(user.IdRole);
            return View("Index");
        }

        [HasCredential(RoleID = "ADMIN")]

        public ActionResult Delete(int Id)
        {
            new UserDAO().Delete(Id);

            return RedirectToAction("Index");
        }

        [HasCredential(RoleID = "ADMIN")]

        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDAO().ChangeStatus(id);
            return Json(new
            {
                status = result

            });
        }
        [HasCredential(RoleID = "ADMIN")]

        public void SetViewBag(long? selectIdRole = null)
        {
            var dao = new RoleDAO();
            ViewBag.IdRole = new SelectList(dao.ListAll(), "Id", "Name", selectIdRole);

        }
    }
}