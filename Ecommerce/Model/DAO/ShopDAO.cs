
using Model.EF;
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
        //public List<Shop> ListAll()
        //{
        //    try
        //    {

        //        return db.Shops.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public Image ViewDetail(int Id)
        //{
        //    return db.Images.Find(Id);
        //}
        public long InsertByRegister(long IdUser)
        {
            try
            {
                var Shop = new Shop();
                Shop.IdUser = IdUser;
                var id = db.Shops.Add(Shop).Id;
                db.SaveChanges();
                return id;
                //return db.Shops.Add(Shop).Id;
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
