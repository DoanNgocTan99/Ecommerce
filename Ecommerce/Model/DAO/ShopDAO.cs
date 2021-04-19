using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ShopDAO
    {
        EcommerceContext db = null;
        public ShopDAO()
        {
            db = new EcommerceContext();
        }
        public List<Shop> ListAll()
        {
            try
            {

                return db.Shops.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Image ViewDetail(int Id)
        {
            return db.Images.Find(Id);
        }
    }
}
