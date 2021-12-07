using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using AnhThuBakery.Filters;
using AnhThuBakery.Models;


namespace AnhThuBakery.Models
{
    public class KiemTraDangNhap
    {
        /// <summary>
        /// Kiểm tra xem đã đăng nhập admin hay chưa, nếu chưa chuyển tới trang đăng nhập
        /// </summary>
        public void DangNhapAdmin()
        {
 
        }
        /// <summary>
        /// Kiểm tra xem đã đăng nhập user hay chưa, nếu chưa chuyển tới trang đăng nhập
        /// </summary>
        public void DangNhapUser()
        {
 
        }
    }
}