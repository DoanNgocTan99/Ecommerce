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
        #region Index
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new CategoryDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }
        #endregion

        #region Create
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
                        #region Add Image
                        string fileName = Path.GetFileNameWithoutExtension(category.ImageFile.FileName);
                        //return type file 
                        string extension = Path.GetExtension(category.ImageFile.FileName);
                        //return file name 
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        var temp = fileName;
                        //category.Path = "~/Image/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Image/Category"), fileName);
                        string filename = fileName.Substring(fileName.Length - (16 + temp.Length), (16 + temp.Length));
                        category.ImageFile.SaveAs(fileName);
                        #endregion
                        //return file name 
                        bool check = dao.CheckCategory(category.Name);
                        if (check)
                        {
                            long id = dao.Insert(category);
                            var ImageDao = new ImageDAO();
                            Image image = new Image();
                            image.IdCategory = id;
                            image.Path = filename;
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
                            SetAlert("Đã có loại danh mục trong Cơ sở dữ liệu!!", "erro");
                            return RedirectToAction("Index", "Category");
                        }
                    }
                    else
                    {
                        bool check = dao.CheckCategory(category.Name);
                        if (check)
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
                        else
                        {
                            SetAlert("Đã có loại danh mục trong Cơ sở dữ liệu!!", "erro");
                            return RedirectToAction("Index", "Category");
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
        #endregion

        #region Edit
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
                    var temp = fileName;
                    //category.Path = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/Category"), fileName);
                    string filename = fileName.Substring(fileName.Length - (16 + temp.Length), (16 + temp.Length));
                    category.ImageFile.SaveAs(fileName);
                    bool check = dao.CheckCategory(category.Name);
                    if (check)
                    {
                        var result = dao.Update(category);
                        if (!result)
                        {
                            SetAlert("Câp nhập chưa thành công, kiểm tra lại thông tin.", "erro");
                            return RedirectToAction("Index", "Category");
                        }
                        var ImageDao = new ImageDAO();
                        Image image = new Image();
                        image.IdCategory = category.Id;
                        image.Path = filename;
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
                        SetAlert("Tên danh mục tồn tại trong Cơ sở dữ liệu!!", "erro");
                        return RedirectToAction("Index", "Category");
                    }
                }
                else
                {
                    bool check = dao.CheckCategory(category.Name);
                    if (check)
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
                    else
                    {
                        SetAlert("Tên danh mục tồn tại trong Cơ sở dữ liệu!!", "erro");
                        return RedirectToAction("Index", "Category");
                    }
                }
            }
            return View("Index");
        }
        #endregion

        #region Delete
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
        #endregion

        #region ChangeStatus
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
        #endregion

        #region SerViewBag
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
        #endregion

    }
}