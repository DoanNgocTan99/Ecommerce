using Ecommerce.Areas.Seller.Controllers;
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
    public class CategoryController : BaseController
    {
        // GET: Seller/Category
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new CategoryDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [HasCredential(RoleID = "ADMIN")]
        [ValidateInput(false)]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dao = new CategoryDAO();
                    if (category.ImageFile != null)
                    {
                        //return file name 
                        string fileName = Path.GetFileNameWithoutExtension(category.ImageFile.FileName);
                        //return type file 
                        string extension = Path.GetExtension(category.ImageFile.FileName);

                        //return file name 
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        //category.Path = "~/Image/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Image/Category"), fileName);
                        category.ImageFile.SaveAs(fileName);

                        long id = dao.Insert(category);

                        var ImageDao = new ImageDAO();

                        Image image = new Image();
                        image.IdCategory = id;
                        image.Path = fileName;

                        long ID = ImageDao.Insert(image);
                        //bool check = ImageDao.Insert(id, fileName);
                        if (ID > 0)
                        {
                            SetAlert("Thêm mới doanh mục thành công!!", "success");
                            return RedirectToAction("Index", "Category");
                        }
                        else
                        {

                            ModelState.AddModelError("", "Thêm doanh mục không thành công");
                        }
                    }
                    else
                    {
                        long id = dao.Insert(category);
                        if (id > 0)
                        {
                            SetAlert("Thêm mới doanh mục thành công!!", "success");
                            return RedirectToAction("Index", "Category");
                        }
                        else
                        {

                            ModelState.AddModelError("", "Thêm doanh mục không thành công");
                        }
                    }


                }
                catch (Exception ex)
                {
                    return View("Index");
                }


            }
            return View("Index");
        }
        [HttpGet]
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Edit(int id)
        {
            var user = new CategoryDAO().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        [HasCredential(RoleID = "ADMIN")]
        [ValidateInput(false)]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                if (category.ImageFile != null)
                {
                    //return file name
                    string fileName = Path.GetFileNameWithoutExtension(category.ImageFile.FileName);
                    //return type file
                    string extension = Path.GetExtension(category.ImageFile.FileName);

                    //return file name 
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    //category.Path = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/Category"), fileName);
                    category.ImageFile.SaveAs(fileName);

                    var result = dao.Update(category);

                    var ImageDao = new ImageDAO();

                    Image image = new Image();
                    image.IdCategory = category.Id;
                    image.Path = fileName;

                    bool ID = ImageDao.UpdateByIdCategory(image);
                    if (ID)
                    {
                        SetAlert("Cập nhập doanh mục thành công!!", "success");

                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhập Doanh mục không thành công");
                    }
                }
                else
                {
                    var result = dao.Update(category);
                    if (result)
                    {
                        SetAlert("Cập nhập doanh mục thành công!!", "success");

                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhập Doanh mục không thành công");
                    }
                }


            }
            return View("Index");
        }
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Delete(int Id)
        {
            var dao = new CategoryDAO();
            var check = dao.Delete(Id);
            if (check)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new ViewResult
                {
                    ViewName = "~/Areas/Seller/Views/Login/Index.cshtml"
                };
            }
        }
        [HttpPost]
        [HasCredential(RoleID = "ADMIN")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HasCredential(RoleID = "ADMIN")]
        public void SetViewBag(long? selectIdCategory = null, long? selectIdProduct = null, long? selectIdUser = null)
        {
            var dao_Category = new CategoryDAO();
            ViewBag.IdCategory = new SelectList(dao_Category.ListAll(), "Id", "Name", selectIdCategory);

            var dao_User = new UserDAO();
            ViewBag.IdUser = new SelectList(dao_User.ListAll(), "Id", "Name", selectIdUser);

            var dao_Product = new ProductDAO();
            ViewBag.IdProduct = new SelectList(dao_Product.ListAll(), "Id", "Name", selectIdProduct);
        }
    }
}