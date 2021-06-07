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
        public IEnumerable<ViewOrderByUser> GetListTransaction(string searchString, int page, int pageSize)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            var model = (from a in db.Transactions
                         join b in db.Users
                         on a.IdUser equals b.Id
                         join c in db.DeliveryStatuses
                         on a.IdDeliveryStatus equals c.id
                         where a.IdShop == session.IdShop && a.IdUser != session.Id
                         select new
                         {
                             id = a.Id,
                             NameUser = b.Name,
                             Address = b.Address,
                             status = c.Status,
                             CheckoutStatus = a.CheckoutStatus,
                             CreateDate = a.CreatedDate
                         }).AsEnumerable().Select(x => new ViewOrderByUser()
                         {
                             Id = x.id,
                             NameUser = x.NameUser,
                             Address = x.Address,
                             Status = x.status,
                             CheckoutStatus = x.CheckoutStatus,
                             CreatedDate = x.CreateDate
                         });
            if (!string.IsNullOrEmpty(searchString))
            {
                return model.Where(x => x.NameUser.Contains(searchString)).ToPagedList(page, pageSize);
            }
            return model.ToPagedList(page, pageSize);
        }
        public List<Order> ViewDetail(long Id)
        {
            try
            {

                return db.Orders.Where(x => x.IdTransaction == Id).ToList();
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<MyOrder> GetAllByIdTransactions(long IdTransaction,string searchString,int page, int pageSize)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
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
                         where a.IdTransaction == IdTransaction
                         select new
                         {
                             Id = a.Id,
                             NameProduct = d.Name,
                             NameUser = e.Name,
                             Amount =b.Amount,
                             Message = a.Message,
                             CheckoutStatus = b.CheckoutStatus,
                             Address = b.Address,
                             DeliveryStatus = c.Status,
                             NamePayment = f.Name,
                             Price = a.Price,
                             Email = e.Email,
                             PhoneNumber = e.Phone,
                             AmountProduct = a.Amount,
                             Discount = d.Discount
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
                             Price = x.Price,
                             Email = x.Email,
                             PhoneNumber  = x.PhoneNumber,
                             AmountProduct = x.AmountProduct,
                             Discount = x.Discount
                         });
            if (!string.IsNullOrEmpty(searchString))
            {
                return model.Where(x => x.NameProduct.Contains(searchString)).ToPagedList(page, pageSize);
            }
            return model.ToPagedList(page, pageSize);
            //return model.ToList();
        }
        public Shop GetShopByIdPoduct(long idproduct)
        {
            var temp = db.Products.Where(x => x.Id == idproduct).Select(x => x.IdShop).FirstOrDefault();
            var result = db.Shops.Find(temp);
            return result;
        }
        public User GetUserBySession()
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            return db.Users.Find(session.Id);
        }
        public Transaction GetTransaction (long id)
        {
            var temp = db.Orders.Where(x => x.Id == id).Select(x => x.IdTransaction).FirstOrDefault();
            return db.Transactions.Find(id);
        }
    }
}
