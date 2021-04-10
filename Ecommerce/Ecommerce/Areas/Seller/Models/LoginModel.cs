using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Areas.Seller.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời Bạn Nhập Vào Tài Khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mời Bạn Nhập Vào Mật Khẩu")]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}