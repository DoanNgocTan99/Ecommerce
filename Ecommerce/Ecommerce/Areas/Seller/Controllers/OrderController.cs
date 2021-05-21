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
    public class OrderController : BaseController
    {
        [HttpGet]
        // GET: Seller/Order
        [HasCredential(RoleID = "USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new OrderDAO();
            var model = dao.GetListTransaction(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        //[HttpGet]
        //[HasCredential(RoleID = "USER")]
        //public ActionResult ViewDetail(long id)
        //{
        //    var dao = new OrderDAO();
        //    var model = dao.ViewDetail(id);
        //    return View(model);
        //}
        [HttpGet]
        [HasCredential(RoleID = "USER")]
        public ActionResult ViewDetail(long id, string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new OrderDAO();
            var model = dao.GetAllByIdTransactions(id,searchString,page,pageSize);
            return View(model);
        }
    }
}