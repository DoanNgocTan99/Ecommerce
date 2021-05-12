using Ecommerce.Common;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class ProductManageController : Controller
    {
        // GET: Admin/ProductManage
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDAO();
            var model = dao.ListAllPagingByAdmin(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult ShowProductInFlaseSale(string searchString, int page =1 , int pageSize = 5)
        {
            var dao = new ProgrammeDAO();
            var model = dao.ListAllPagingFlaseSale(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult ShowProductInAdvertisement(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProgrammeDAO();
            var model = dao.ListAllPagingAdvertisement(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Delete(int Id)
        {
            new ProductDAO().Delete(Id);

            return RedirectToAction("Index");
        }


        [HasCredential(RoleID = "ADMIN")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result

            });
        }
        [HasCredential(RoleID = "ADMIN")]
        public JsonResult JoinFlaseSale(long id)
        {
            var result = new ProductDAO().ChangeStatusFalseSale(id);
            return Json(new
            {
                status = result

            });
        }
        [HasCredential(RoleID = "ADMIN")]
        public JsonResult JoinAdvertisement(long id)
        {
            var result = new ProductDAO().ChangeStatusAdvertisement(id);
            return Json(new
            {
                status = result

            });
        }
        [HasCredential(RoleID = "ADMIN")]

        public void SetViewBag(long? selectIdCategory = null, long? selectIdShop = null)
        {
            var dao_Category = new CategoryDAO();
            ViewBag.IdCategory = new SelectList(dao_Category.ListAll(), "Id", "Name", selectIdCategory);

            //var dao_Shop = new ShopDAO();
            //ViewBag.IdShop = new SelectList(dao_Shop.ListAll(), "Id", "Name", selectIdShop);

        }
    }
}