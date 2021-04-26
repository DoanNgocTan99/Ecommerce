using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Common
{
    [Serializable]

    public class UserLogin
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        public bool Gender { get; set; }

        public DateTime DOB { get; set; }

        public string Address { get; set; }
    }
}