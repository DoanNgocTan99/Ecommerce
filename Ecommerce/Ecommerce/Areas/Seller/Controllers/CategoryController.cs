using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Seller/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new CategoryDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {

                var dao = new CategoryDAO();
                //return file name
                string fileName = Path.GetFileNameWithoutExtension(category.ImageFile.FileName);
                //return type file
                string extension = Path.GetExtension(category.ImageFile.FileName);

                //return file name 
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //category.Path = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                category.ImageFile.SaveAs(fileName);

                long id = dao.Insert(category);

                var ImageDao = new ImageDAO();
                bool check = ImageDao.Insert(id, fileName);
                if (check)
                {
                    SetAlert("Thêm mới doanh mục thành công!!", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {

                    ModelState.AddModelError("", "Thêm doanh mục không thành công");
                }

            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new CategoryDAO().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();


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
            return View("Index");
        }
        public ActionResult Delete(int Id)
        {
            new CategoryDAO().Delete(Id);

            return RedirectToAction("Index");
        }
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
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