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
using System.ComponentModel.DataAnnotations;


namespace AnhThuBakery.Models
{
    public class BanhViewModel
    {
        public BANH banhs { get; set; }
        public SelectList lstLoai { get; set; }
        public SelectList lstChude { get; set; }
    }

    public class GioHang
    {
        public int IdBanh { get; set; }
        public string TenBanh { get; set; }
        public string Hinh { get; set; }
        public int DonGia { get; set; }
         [RegularExpression(@"\d+")]
        public int Soluong { get; set; }

        public int Tongtien { get; set; }
    }


}