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
    public class ReviewDAO
    {
        EcommerceContext db = null;
        public ReviewDAO()
        {
            db = new EcommerceContext();
        }
        public Review ViewDetail(int Id)
        {
            try
            {
                return db.Reviews.Find(Id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Review> GetListReviewByIdShop(long idShop)
        {
            var model = (from a in db.Reviews
                         join b in db.Products
                         on a.IdProduct equals b.Id
                         where b.IdShop == idShop
                         select new
                         {
                             IdProduct = b.Id
                         }).AsEnumerable().Select(x => new Review()
                         {
                             IdProduct = x.IdProduct
                         });
            return model;
        }
        public IEnumerable<LinkProgramAndProduct> ListAllPaging(string searchString, int page, int pageSize)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            //IQueryable<Review> model = db.Reviews;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Content.Contains(searchString) );
            //}
            var data = (from a in db.Reviews
                        join b in db.Products
                        on a.IdProduct equals b.Id
                        join c in db.Users
                        on a.IdUser equals c.Id
                        where b.IdShop == session.IdShop
                        select new
                        {
                            Id = a.Id,
                            IdProduct = b.Id,
                            Content = a.Content,
                            IdUser = a.IdUser,
                            NameProduct = b.Name,
                            NameUser = c.Name,
                            CreateDate = a.CreateDate
                        }).AsEnumerable().Select(x => new LinkProgramAndProduct()
                        {
                            Id= x.Id,
                            Content = x.Content,
                            NameProduct = x.NameProduct,
                            NameUser = x.NameUser
                        });


            //return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
            return data.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);

        }
    }
}
