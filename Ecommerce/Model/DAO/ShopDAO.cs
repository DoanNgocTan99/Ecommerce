
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
    public class ShopDAO
    {
        EcommerceContext db = null;
        public ShopDAO()
        {
            db = new EcommerceContext();
        }
        public IEnumerable<Shop> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Shop> model = db.Shops;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }
        public IEnumerable<Product> ListProductByIdShopPaging(long idShop, string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.IdShop == idShop && x.Name.Contains(searchString));

            }
            return model.Where(x => x.IdShop == idShop).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        //public IEnumerable<Product> ListCategoryByIdShopPaging(long idShop, string searchString, int page, int pageSize)
        //{
        //    IQueryable<Product> model = db.Products;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.IdShop == idShop && x.Name.Contains(searchString));

        //    }
        //    return model.Where(x => x.IdShop == idShop).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        //}
        //public IEnumerable<Category> ListCategoryByIdShopPaging(long id)
        //{
        //    //var user = db.Users.Single(x => x.UserName == UserName);
        //    //var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
        //    var data = (from a in db.Products
        //                join b in db.Categories 
        //                on a.IdCategory equals b.Id
        //                join c in db.Shops
        //                on a.IdShop equals c.Id

        //                where a.IdShop == id
        //                select new
        //                {
        //                    Name = b.Name
        //                }).AsEnumerable().Select(x => new Category()
        //                {
        //                    Name = x.Name
        //                });

        //    return data.OrderByDescending(x => x.CreatedDate).Distinct();
        //}
        public List<String> ListCategoryByIdShopPaging(long id)
        {
            //var user = db.Users.Single(x => x.UserName == UserName);
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            var data = (from a in db.Products
                        join b in db.Categories on a.IdCategory equals b.Id

                        where a.IdShop == id
                        select new
                        {
                            Id = a.IdShop,
                            Name = b.Name
                        }).AsEnumerable().Select(x => new Category()
                        {

                            Name = x.Name
                        });

            return data.Select(x => x.Name).Distinct().ToList();
        }
        public long InsertByRegister(long IdUser)
        {
            try
            {
                var Shop = new Shop();
                Shop.IdUser = IdUser;

                var id = db.Shops.Add(Shop).Id;
                var Name = Shop.Name;
                var Description = Shop.Description;
                var Date = Shop.CreatedDate;

                db.SaveChanges();
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Shop ViewDetail(int Id)
        {
            try
            {
                return db.Shops.Find(Id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(int Id)
        {
            try
            {
                var shop = db.Shops.Find(Id);
                db.Shops.Remove(shop);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(Shop entity)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            try
            {
                var shop = db.Shops.Find(entity.Id);

                shop.Name = entity.Name;
                shop.Description = entity.Description;
                shop.Address = entity.Address;
                shop.Phone = entity.Phone;
                shop.IdUser = session.Id;
                shop.ModifiedBy = session.UserName;

                shop.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
