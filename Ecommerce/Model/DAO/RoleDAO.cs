using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class RoleDAO
    {
        EcommerceContext db = null;
        public RoleDAO()
        {
            db = new EcommerceContext();
        }
        public List<Role> ListAll()
        {
            return db.Roles.ToList();
        }
    }
}
