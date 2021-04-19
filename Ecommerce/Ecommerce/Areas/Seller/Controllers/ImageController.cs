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
    public class ImageController : BaseController
    {
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            try
            {

                var dao = new ImageDAO();
                var model = dao.ListAllPaging(searchString, page, pageSize);

                ViewBag.SearchString = searchString;
                return View(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Image image)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                    string extension = Path.GetExtension(image.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    image.Path = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    image.ImageFile.SaveAs(fileName);


                    var dao = new ImageDAO();

                    long id = dao.Insert(image);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới ảnh thành công!!", "success");
                        return RedirectToAction("Index", "Image");
                    }
                    else
                    {

                        ModelState.AddModelError("", "Thêm ảnh không thành công");
                    }
                    //ModelState.Clear();
                }

                SetViewBag();
                return View("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var image = new ImageDAO().ViewDetail(id);
            SetViewBag(image.IdCategory, image.IdProduct, image.IdUser);
            return View(image);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Image image)
        {
            if (!ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                string extension = Path.GetExtension(image.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                image.Path = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                image.ImageFile.SaveAs(fileName);

                var dao = new ImageDAO();


                var result = dao.Update(image);
                if (result)
                {
                    SetAlert("Cập nhập người dùng thành công!!", "success");

                    return RedirectToAction("Index", "Image");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập Người dùng không thành công");
                }

            }
            SetViewBag(image.IdCategory, image.IdProduct, image.IdUser);
            return View("Index");
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
        public ActionResult Delete(int Id)
        {
            new ImageDAO().Delete(Id);

            return RedirectToAction("Index");
        }
    }
}