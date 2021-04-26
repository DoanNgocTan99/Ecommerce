using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    [Authorize]

    public class ProductController : BaseController
    {
        // GET: Seller/Products
        
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                var dao = new ProductDAO();


                long id = dao.Insert(product);
                if (id > 0)
                {
                    SetAlert("Thêm mới sản phẩm thành công!!", "success");
                    return RedirectToAction("Index", "Product");

                }
                else
                {

                    ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                }

            }
            SetViewBag();
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = new ProductDAO().ViewDetail(id);
            SetViewBag(product.IdCategory, product.IdShop);
            return View(product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();


                var result = dao.Update(product);
                if (result)
                {
                    SetAlert("Cập nhập sản phẩm thành công!!", "success");

                    return RedirectToAction("Index", "Product");

                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập sản phẩm không thành công");
                }

            }
            SetViewBag(product.IdCategory, product.IdShop);
            return View("Index");
        }
        public ActionResult Delete(int Id)
        {
            new CategoryDAO().Delete(Id);

            return RedirectToAction("Index");
        }

        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
               
            });
        }
        public void SetViewBag(long? selectIdCategory = null, long? selectIdShop = null)
        {
            var dao_Category = new CategoryDAO();
            ViewBag.IdCategory = new SelectList(dao_Category.ListAll(), "Id", "Name", selectIdCategory);

            //var dao_Shop = new ShopDAO();
            //ViewBag.IdShop = new SelectList(dao_Shop.ListAll(), "Id", "Name", selectIdShop);

        }
    }
}