using Ecommerce.Common;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class ProductManageController : Controller
    {
        #region Index
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDAO();
            var DAO = new ProgrammeDAO();
            dynamic MyModel = new ExpandoObject();

            var modelIndex = dao.ListAllPagingByAdmin(searchString, page, pageSize);
            var modelFlaseSale = DAO.ListAllPagingFlaseSale(searchString, page, pageSize);
            var modelAdvertisement = DAO.ListAllPagingAdvertisement(searchString, page, pageSize);

            MyModel.Flas = modelFlaseSale;
            MyModel.index = modelIndex;
            MyModel.Adver = modelAdvertisement;

            ViewBag.SearchString = searchString;
            return View(MyModel);
        }
        #endregion

        #region Show Product FlaseSale
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult ShowProductInFlaseSale(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProgrammeDAO();
            var model = dao.ListAllPagingFlaseSale(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        #endregion

        #region Show Product Advertisement
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult ShowProductInAdvertisement(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProgrammeDAO();
            var model = dao.ListAllPagingAdvertisement(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        #endregion

        #region Delete
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Delete(int Id)
        {
            new ProductDAO().Delete(Id);

            return RedirectToAction("Index");
        }
        #endregion

        #region Change Status
        [HasCredential(RoleID = "ADMIN")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result

            });
        }
        #endregion

        #region Join Flase Sale
        [HasCredential(RoleID = "ADMIN")]
        public JsonResult JoinFlaseSale(long id)
        {
            var result = new ProductDAO().ChangeStatusFalseSale(id);
            return Json(new
            {
                status = result

            });
        }
        #endregion

        #region Join Advertisement
        [HasCredential(RoleID = "ADMIN")]
        public JsonResult JoinAdvertisement(long id)
        {
            var result = new ProductDAO().ChangeStatusAdvertisement(id);
            return Json(new
            {
                status = result

            });
        }
        #endregion

        #region Set View bag
        [HasCredential(RoleID = "ADMIN")]
        public void SetViewBag(long? selectIdCategory = null, long? selectIdShop = null)
        {
            var dao_Category = new CategoryDAO();
            ViewBag.IdCategory = new SelectList(dao_Category.ListAll(), "Id", "Name", selectIdCategory);
        }
        #endregion
    }
}