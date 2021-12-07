using AnhThuBakery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Net.Mail;

namespace AnhThuBakery.Controllers
{
    public class HomeController : Controller
    {
        CSDLBanhKemEntities5 db = new CSDLBanhKemEntities5();


        [HttpPost]
        public ActionResult Search(string id)
        {
            List<BANH> banh = db.BANHs.ToList();
            List<BANH> model = new List<BANH>();
            foreach (var i in banh)
            {
                if (i.TenBanh.IndexOf(id)>=0)
                {
                    model.Add(i);
                }
            }
            return View("Index", model);
        }

        public static string ConvertVN(string chucodau)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrChar = FindText.ToCharArray();
            while ((index = chucodau.IndexOfAny(arrChar)) != -1)
            {
                int index2 = FindText.IndexOf(chucodau[index]);
                chucodau = chucodau.Replace(chucodau[index], ReplText[index2]);
            }
            return chucodau;
        } 

        [HttpGet]
        public ActionResult Index(int? page, string search)
        {
            if (search != null)
            {
                List<BANH> banh = db.BANHs.ToList();
                List<BANH> model = new List<BANH>();
                foreach (var i in banh)
                {
                    if(ConvertVN(i.TenBanh).ToLower().IndexOf(ConvertVN(search).ToLower())>=0)
                    {
                        model.Add(i);
                    }
                }
                int sotrang = (page ?? 1);
                var view = model.ToPagedList(sotrang, 12);
                return View(view);
            }
            else
            {
               // List<BANH> model = (from b in db.BANHs orderby b.MaBanh descending select b).ToList();
                List<BANH> model = db.BANHs.OrderByDescending(m=>m.MaBanh).ToList();
                int sotrang = (page ?? 1);
                var banh = model.ToPagedList(sotrang, 12);
                return View(banh);
            }
           
        }




        [HttpGet]
        public ActionResult LoaiChuDe(int id = 0, int name = 0)
        {
            List<BANH> model = db.BANHs.ToList();
            if (id == 0 && name == 0)
            {
                return View(model);
            }
            if (id != 0)
                model = model.Where(m => m.MaCD == id).ToList();
            if (name != 0)
                model = model.Where(m => m.MaLoai == name).ToList();
            return View(model);
        }




        [HttpGet]
        public ActionResult Detail(int Id)
        {
            BANH model = db.BANHs.Where(m => m.MaBanh == Id).FirstOrDefault();
            ViewBag.banh = db.BANHs.Where(m => m.LOAIBANH.MaLoai == model.LOAIBANH.MaLoai && m.CHUDE.MaCD == model.CHUDE.MaCD).ToList();
            return View(model);
        }
        public ActionResult GioHang()
        {
            try
            {
                if (Session["giohang"] == null)
                    return RedirectToAction("Index");
                ViewBag.httt = db.HTTTs.ToList();
                List<GioHang> model = (List<GioHang>)Session["GioHang"];
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }
        public ActionResult DangKy()
        {
            if (Session["user"] != null)
                return RedirectToAction("Index");
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(KHACHHANG _model,FormCollection _form)
        {
            try
            {
                if (_form["repassword"].ToString() != _model.MatKhau)
                {
                    TempData["error"] = "Mật khẩu không trùng khớp, nhập lại mật khẩu !";
                    return View(_model);
                }
                try
                {
                    if (db.KHACHHANGs.Where(m => m.TenDN == _model.TenDN).FirstOrDefault() != null)
                    {
                        TempData["error"] = "Tên đăng nhập đã tồn tại, xin chọn tên đăng nhập khác !";
                        return View(_model);
                    }
                }
                catch
                {
                    TempData["error"] = "Email đã tồn tại !";
                    return View(_model);
                }
               

                _model.KHTT = false;
                db.KHACHHANGs.Add(_model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Lỗi không thể Đăng Ký: Tên đăng nhập tối đa 16 ký tự, mật khẩu tối đa 16 ký tự hoặc Email đã có người sử dụng !";
                return View(_model);
            }


        }
        public ActionResult DangNhap()
        {
            if (Session["user"] != null)
                return RedirectToAction("Index");
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(KHACHHANG _model)
        {
            if (db.KHACHHANGs.Where(m => m.TenDN == _model.TenDN && m.MatKhau == _model.MatKhau).FirstOrDefault() != null)
            {
                Session["user"] = _model.TenDN;
                return RedirectToAction("Index");
            }
            return View();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult AddGioHang(FormCollection model)
        {

            int id = int.Parse(model["idbanh"].ToString());
            int sl = int.Parse(model["soluong"].ToString());
            GioHang _model = new Models.GioHang();
            BANH banh = db.BANHs.Where(m => m.MaBanh == id).FirstOrDefault();
            _model.IdBanh = id;
            _model.Hinh = banh.HinhMinhHoa;
            _model.DonGia = (int)banh.DonGia;
            _model.TenBanh = banh.TenBanh;
            _model.Tongtien = sl * (int)banh.DonGia;
            _model.Soluong = sl;
            if (Session["GioHang"] == null)
            {
                List<GioHang> giohang = new List<Models.GioHang>();
                giohang.Add(_model);
                Session["GioHang"] = giohang;
            }
            else
            {
                int dem = 0;
                List<GioHang> giohang = (List<GioHang>)Session["GioHang"];
                foreach (var i in giohang)
                {
                    if (i.IdBanh == id)
                    {
                        i.Soluong += sl;
                        i.Tongtien = i.Soluong * i.DonGia;
                        dem++;
                        break;
                    }
                }
                if (dem == 0)
                {
                    giohang.Add(_model);
                }
                Session["GioHang"] = giohang;
            }
            return RedirectToAction("GioHang");
        }
        [HttpPost]
        public ActionResult ThanhToan(FormCollection form)
        {
            int httt = int.Parse(form["httt"].ToString());
            if (Session["user"] == null)
                return RedirectToAction("Dangnhap", "Home");
            string tendn = Session["user"].ToString();
            KHACHHANG kh = db.KHACHHANGs.Where(m => m.TenDN == tendn).FirstOrDefault();
            List<GioHang> giohang = (List<GioHang>)Session["giohang"];
            DONDH order = new DONDH();
            order.TriGia = giohang.Sum(m => m.Tongtien);
            order.NgayDH = DateTime.Now;
            order.MaKH = kh.MaKH;
            order.MaHTTT = httt;
            order.DaGiao = false;
            db.DONDHs.Add(order);
            db.SaveChanges();
            int idhd = db.DONDHs.OrderByDescending(m => m.MaDH).ToList()[0].MaDH;
            foreach (var i in giohang)
            {
                CTDH ctdh = new CTDH();
                ctdh.DonGia = i.DonGia;
                ctdh.MaBanh = i.IdBanh;
                ctdh.MaDH = idhd;
                ctdh.SoLuong = i.Soluong;
                ctdh.ThanhTien = i.Tongtien;
                db.CTDHs.Add(ctdh);          
                db.SaveChanges();
            }
            Session["giohang"] = null;

            #region Gởi Email
            MailMessage mail = new MailMessage();
            mail.To.Add(kh.Email);
            mail.From = new MailAddress(kh.Email);
            mail.Subject = "Đặt Hàng Thành Công !";
            string Body = "Chào bạn đã đến với Anh Thư Bakery!! Chúc mừng bạn đã đặt hàng thành công. Đơn hàng của bạn với số tiền là " + giohang.Sum(m => m.Tongtien).ToString() + " Anh Thư Bakery sẻ sớm liên hệ với bạn để xác nhận !";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("mathu141293@gmail.com", "141293thu");// Enter seders User name and password
            smtp.EnableSsl = true;
           // smtp.Send(mail);
            #endregion

            return Redirect("/Home/MuaThanhCong");

           
        }
        public ActionResult MuaThanhCong()
        {
            return View();
        }

        [HttpGet]
        public ActionResult XoaGioHang(string id)
        {
            int idgiohang = int.Parse(id);
            List<GioHang> model = (List<GioHang>)Session["giohang"];
            foreach (var i in model)
            {
                if (i.IdBanh == idgiohang)
                {
                    model.Remove(i); break;
                }
            }
            if (model.Count <= 0)
                Session["giohang"] = null;
            else
                Session["giohang"] = model;
            return RedirectToAction("GioHang", "Home");
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(FormCollection _form)
        {
            int id = int.Parse(_form["idbanh"].ToString());
            int sl = int.Parse(_form["soluong"].ToString());
            List<GioHang> model = (List<GioHang>)Session["giohang"];
            foreach (var i in model)
            {
                if (i.IdBanh == id)
                {
                    i.Soluong = sl;
                    i.Tongtien = i.Soluong * i.DonGia;
                    break;
                }
            }
            Session["giohang"] = model;
            return RedirectToAction("GioHang", "Home");
        }


        [HttpGet]
        public ActionResult DangXuat()
        {
            Session["user"] = null;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult TinTuc(int id)
        {
            TINTUC tintuc = db.TINTUCs.Where(m => m.MaTinTuc == id).FirstOrDefault();
            return View(tintuc);
        }

        public ActionResult ListTintuc()
        { return View(db.TINTUCs.ToList()); }
        public ActionResult LienHe()
        { return View(); }

        [HttpPost]
        public ActionResult LienHe(LIENHE _model)
        {
            try
            {
                db.LIENHEs.Add(_model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(_model);
            }
        }
        [HttpPost]

        public JsonResult AddToCard(int idBanh = 0, int sl = 1)
        {
            #region Luu vao gio hang
            List<GioHang> giohang = (List<GioHang>)Session["giohang"];

                if (giohang != null)
                {
                    if (idBanh != 0)
                    {
                        if (giohang.Where(m => m.IdBanh == idBanh).FirstOrDefault() != null)
                        {
                            GioHang item = giohang.Where(m => m.IdBanh == idBanh).FirstOrDefault();
                            item.Soluong += sl;
                            item.Tongtien = item.DonGia * item.Soluong;
                            Session["giohang"] = giohang;
                        }
                        else
                        {
                            GioHang item = new GioHang();
                            BANH banh = db.BANHs.Where(m => m.MaBanh == idBanh).FirstOrDefault();
                            item.DonGia = (int)banh.DonGia;
                            item.Hinh = banh.HinhMinhHoa;
                            item.Soluong = sl;
                            item.IdBanh = banh.MaBanh;
                            item.TenBanh = banh.TenBanh;
                            item.Tongtien = item.Soluong * item.DonGia;
                            giohang.Add(item);
                            Session["giohang"] = giohang;
                        }
                    }
                }
                else
                {
                    if (idBanh != 0)
                    {
                        giohang = new List<GioHang>();
                        GioHang item = new GioHang();
                        BANH banh = db.BANHs.Where(m => m.MaBanh == idBanh).FirstOrDefault();
                        item.DonGia = (int)banh.DonGia;
                        item.Hinh = banh.HinhMinhHoa;
                        item.Soluong = sl;
                        item.IdBanh = idBanh;
                        item.TenBanh = banh.TenBanh;
                        item.Tongtien = item.Soluong * item.DonGia;
                        giohang.Add(item);
                        Session["giohang"] = giohang;
                    }
                }
           
               
            #endregion

            string str = "";
            if (giohang != null)
            {
                foreach (var i in giohang)
                {
                    str += "  <tr><td class=\"name\"><a href=\"\">" + i.TenBanh + "</a>   <div>   </div></td>  <td class=\"quantity\">x&nbsp;" + i.Soluong + "</td> <td class=\"total\">" + String.Format("{0:0,0}", i.Tongtien) + "đ</td> <td class=\"remove\"><img class=\"removegiohang\" data-id=\""+i.IdBanh+"\" src=\"/image/remove.png\" alt=\"Loại bỏ\" title=\"Loại bỏ\"></td></tr>";
                }
            }
            
            return Json(new { success = true, str = str, tongtien = String.Format("{0:0,0}", giohang.Sum(m => m.Tongtien)), tongsl = giohang.Sum(m => m.Soluong) });
        }
         [HttpPost]
        public JsonResult RemoveToCard(int idbanh)
        {
            List<GioHang> giohang = (List<GioHang>)Session["giohang"];
            var t = giohang.Single(m => m.IdBanh == idbanh);
            giohang.Remove(t);
            return Json(new {success=true,tt=giohang.Sum(m=>m.Tongtien),sl=giohang.Count });
        }
    }
}
