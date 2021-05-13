using Ecommerce.Areas.Seller.Models;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.DAO
{
    public class OrderDAO
    {
        EcommerceContext db = null;
        public OrderDAO()
        {
            db = new EcommerceContext();
        }
        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Message.Contains(searchString) && x.IdShop == session.IdShop || x.Message.Contains(searchString) && x.IdShop == session.IdShop);
            }
            return model.Where(x => x.IdShop == session.IdShop).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }
        public IEnumerable<MyOrder> GetAllByIdShop(string searchString, int page, int pageSize)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            //IQueryable<Order> model = db.Orders;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Message.Contains(searchString) && x.IdShop == session.IdShop || x.Message.Contains(searchString) && x.IdShop == session.IdShop);
            //}
            //return model.Where(x => x.IdShop == session.IdShop).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            var model = (from a in db.Orders
                         join b in db.Transactions
                         on a.IdTransaction equals b.Id
                         join c in db.DeliveryStatuses
                         on b.IdDeliveryStatus equals c.id
                         join d in db.Products
                         on a.IdProduct equals d.Id
                         join e in db.Users
                         on b.IdUser equals e.Id
                         join f in db.Payments
                         on a.IdPayment equals f.Id
                         where a.IdShop == session.IdShop
                         select new
                         {
                             Id = a.Id,
                             NameProduct = d.Name,
                             NameUser = e.Name,
                             Amount = b.Amount,
                             Message = a.Message,
                             CheckoutStatus = b.CheckoutStatus,
                             Address = b.Address,
                             DeliveryStatus = c.Status,
                             NamePayment = f.Name,
                             Price = d.Price
                         }).AsEnumerable().Select(x => new MyOrder()
                         {
                             Id = x.Id,
                             NameProduct = x.NameProduct,
                             NameUser = x.NameUser,
                             Address = x.Address,
                             Message = x.Message,
                             Amount = x.Amount,
                             CheckoutStatus = x.CheckoutStatus,
                             DeliveryStatus = x.DeliveryStatus,
                             NamePayment = x.NamePayment,
                             Price = x.Price
                         });
            if (!string.IsNullOrEmpty(searchString))
            {
                return model.Where(x => x.NameProduct.Contains(searchString)).ToPagedList(page, pageSize);
            }
            return model.OrderByDescending(x => x.Amount).ToPagedList(page,pageSize);
        }
    }
}
