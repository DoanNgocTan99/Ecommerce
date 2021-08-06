
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
        #region List ALl
        public IEnumerable<Shop> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Shop> model = db.Shops;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        #endregion
        #region ListProductByIdShopPaging
        public IEnumerable<Product> ListProductByIdShopPaging(long idShop, string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.IdShop == idShop && x.Name.Contains(searchString));
            }
            return model.Where(x => x.IdShop == idShop).OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
        #endregion
        #region ListCategoryByIdShopPaging
        public List<String> ListCategoryByIdShopPaging(long id)
        {
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
        #endregion
        #region InsertByRegister
        public long InsertByRegister(long IdUser)
        {
            try
            {
                var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
                var user = db.Users.Where(x => x.Id == IdUser).FirstOrDefault();
                var Shop = new Shop();
                Shop.Name = "Shop của " + user.Name;
                Shop.IdUser = IdUser;
                Shop.Address = user.Address;
                Shop.Description = "Mua bán các loại thiết bị điện tử";
                Shop.CreatedDate = DateTime.Now;
                if (session == null)
                {
                    Shop.CreatedBy = user.Name;
                }
                else
                {
                    Shop.CreatedBy = session.UserName;
                }
                Shop.Phone = user.Phone;
                var id = db.Shops.Add(Shop).Id;
                db.SaveChanges();
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ViewDetail
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

        #endregion
        #region Delete
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
        #endregion
        #region DeleteListShopByIdUser
        public bool DeleteListShopByIdUser(long id)
        {
            try
            {
                var data = db.Shops.Where(x => x.IdUser == id).ToList();
                db.Shops.RemoveRange(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        #endregion
        #region Update
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
        #endregion
        #region Check Name Shop
        public bool CheckNameShop(string Name, long id)
        {
            try
            {
                var check = db.Shops.Where(x => x.Name == Name).FirstOrDefault();
                if (check != null && check.Id == id)
                {
                    return false;
                }
                else if (check != null && check.Id != id)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
