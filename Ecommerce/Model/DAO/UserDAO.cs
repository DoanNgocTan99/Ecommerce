using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class UserDAO
    {
        EcommerceContext db = null;
        public UserDAO()
        {
            db = new EcommerceContext();
        }

        
    }
}
