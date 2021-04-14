using Ecommerce.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    public class BaseController : Controller
    {
        // GET: Seller/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Login", action = "Index", Area = "Seller" }));
            }
            base.OnActionExecuting(filterContext);
        }

        //Set Thong bao
        protected void SetAlert(string message, string type)
        {
            //TempData: Lấy thông tin từ server về tương tự ViewBag
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";

            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";

            }
            else if (type == "erro")
            {
                TempData["AlertType"] = "alert-danger";

            }

        }
    }
}