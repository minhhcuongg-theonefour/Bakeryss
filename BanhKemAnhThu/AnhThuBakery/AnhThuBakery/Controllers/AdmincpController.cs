using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnhThuBakery.Models;

namespace AnhThuBakery.Controllers
{
    public class AdmincpController : Controller
    {
        CSDLBanhKemEntities5 db = new CSDLBanhKemEntities5();
        //
        // GET: /Admincp/

        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View();
        }

        #region [Chủ đề]
        public ActionResult CreateChuDe()
        {
            if(Session["admin"]==null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            ViewBag.title="Tạo Mới Chủ Đề";
            return View();
        }
        [HttpPost]
        public ActionResult CreateChuDe(CHUDE _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            db.CHUDEs.Add(_model);
            db.SaveChanges();
            return RedirectToAction("ListChuDe", "Admincp");
        }

        [HttpGet]
        public ActionResult DeleteChuDe(string IdChuDe)
        {
            try
            {
                if (Session["admin"] == null)
                    return RedirectToAction("dangnhapadmin", "Admincp");
                int t = int.Parse(IdChuDe);
                CHUDE model = db.CHUDEs.Where(i => i.MaCD == t).FirstOrDefault();
                if (model != null)
                {
                    db.CHUDEs.Remove(model);
                    db.SaveChanges();
                }
                return RedirectToAction("ListChuDe", "Admincp");
            }
            catch
            {
                return RedirectToAction("ListChuDe", "Admincp");
            }
            
        }
        [HttpGet]
        public ActionResult UpdateChuDe(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            int t = int.Parse(id);
            CHUDE model = db.CHUDEs.Where(i => i.MaCD == t).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateChuDe(CHUDE _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            CHUDE model = db.CHUDEs.Where(i => i.MaCD == _model.MaCD).FirstOrDefault();
            if (model != null)
            {
                model.TenChuDe = _model.TenChuDe;
                db.SaveChanges();
            }
            return RedirectToAction("ListChuDe", "Admincp");
        }

        public ActionResult ListChuDe()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            List<CHUDE> model = db.CHUDEs.ToList();
            return View(model);
        }
        #endregion

        #region [Bánh]
        public ActionResult ListBanh()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            List<BANH> model = db.BANHs.OrderByDescending(i=>i.MaBanh).ToList();
            return View(model);
        }
        public ActionResult CreateBanh()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            BanhViewModel model = new BanhViewModel();
            model.lstChude = new SelectList(db.CHUDEs.AsEnumerable(),"MaCD","TenChuDe",db.CHUDEs.AsEnumerable().FirstOrDefault());
            model.lstLoai = new SelectList(db.LOAIBANHs.AsEnumerable(), "MaLoai", "TenLoaiBanh", db.LOAIBANHs.AsEnumerable().FirstOrDefault());
            return View(model);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateBanh(BanhViewModel _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            int sl = 0;
            if (_model.banhs.SoLuongBan != null)
                sl = int.Parse(_model.banhs.SoLuongBan.ToString());
            BANH model = new BANH() 
            {
                MaCD=_model.banhs.MaCD,
                DonGia = _model.banhs.DonGia,
                HinhMinhHoa = _model.banhs.HinhMinhHoa,
                MaLoai = _model.banhs.MaLoai,
                MoTa = _model.banhs.MoTa,
                TenBanh = _model.banhs.TenBanh,
                SoLuongBan=sl,
            };
            db.BANHs.Add(model);
            db.SaveChanges();
            return RedirectToAction("ListBanh","Admincp");
        }

        [HttpGet]
        public ActionResult UpdateBanh(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            int idbanh = int.Parse(id);
            BanhViewModel model = new BanhViewModel();
            model.banhs = db.BANHs.Where(i => i.MaBanh == idbanh).FirstOrDefault();
            model.lstChude = new SelectList(db.CHUDEs.AsEnumerable(), "MaCD", "TenChuDe", db.CHUDEs.AsEnumerable().FirstOrDefault());
            model.lstLoai = new SelectList(db.LOAIBANHs.AsEnumerable(), "MaLoai", "TenLoaiBanh", db.LOAIBANHs.AsEnumerable().FirstOrDefault());
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateBanh(BanhViewModel _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            int sl = 0;
            if (_model.banhs.SoLuongBan != null)
                sl = int.Parse(_model.banhs.SoLuongBan.ToString());
            BANH model = db.BANHs.Where(i => i.MaBanh == _model.banhs.MaBanh).FirstOrDefault();
                model.MaCD = _model.banhs.MaCD;
                model.DonGia = _model.banhs.DonGia;
                model.HinhMinhHoa = _model.banhs.HinhMinhHoa;
                model.MaLoai = _model.banhs.MaLoai;
                model.MoTa = _model.banhs.MoTa;
                model.TenBanh = _model.banhs.TenBanh;
                model.SoLuongBan = sl;
            db.SaveChanges();
            return RedirectToAction("ListBanh", "Admincp");
        }

        [HttpGet]
        public ActionResult DeleteBanh(string id)
        {
            try
            {
                if (Session["admin"] == null)
                    return RedirectToAction("dangnhapadmin", "Admincp");
                int t = int.Parse(id);
                BANH model = db.BANHs.Where(i => i.MaBanh == t).FirstOrDefault();
                db.BANHs.Remove(model);
                db.SaveChanges();
                return RedirectToAction("ListBanh", "Admincp");
            }
            catch
            {
                return RedirectToAction("ListBanh", "Admincp");
            }
            
        }
        #endregion

        #region [Loại Bánh]
        public ActionResult CreateLoai()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            ViewBag.title = "Tạo Mới Loại";
            return View();
        }
        [HttpPost]
        public ActionResult CreateLoai(LOAIBANH _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            db.LOAIBANHs.Add(_model);
            db.SaveChanges();
            return RedirectToAction("ListLoai");
        }

        [HttpGet]
        public ActionResult DeleteLoai(string IdLoai)
        {
            try
            {
                if (Session["admin"] == null)
                    return RedirectToAction("dangnhapadmin", "Admincp");
                int t = int.Parse(IdLoai);
                LOAIBANH model = db.LOAIBANHs.Where(i => i.MaLoai == t).FirstOrDefault();
                if (model != null)
                {
                    db.LOAIBANHs.Remove(model);
                    db.SaveChanges();
                }
                return RedirectToAction("ListLoai", "Admincp");
            }
            catch
            { return RedirectToAction("ListLoai", "Admincp"); }
          
        }
        [HttpGet]
        public ActionResult UpdateLoai(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            int t = int.Parse(id);
            LOAIBANH model = db.LOAIBANHs.Where(i => i.MaLoai == t).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateLoai(LOAIBANH _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            LOAIBANH model = db.LOAIBANHs.Where(i => i.MaLoai == _model.MaLoai).FirstOrDefault();
            if (model != null)
            {
                model.TenLoaiBanh = _model.TenLoaiBanh;
                db.SaveChanges();
            }
            return RedirectToAction("ListLoai", "Admincp");
        }

        public ActionResult ListLoai()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            List<LOAIBANH> model = db.LOAIBANHs.OrderByDescending(m=>m.MaLoai).ToList();
            return View(model);
        }
        #endregion

        #region [Khách Hàng]
        public ActionResult ListKhachHang()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            List<KHACHHANG> model = db.KHACHHANGs.OrderByDescending(i => i.MaKH).ToList();
            return View(model);
        }
        public ActionResult CreateKhachHang()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View();
        }
        [HttpPost]
        public ActionResult CreateKhachHang(KHACHHANG _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            if (db.KHACHHANGs.Where(i => i.TenDN == _model.TenDN).FirstOrDefault() != null)
            {
                ModelState.AddModelError("TenDN","Tên Đăng Nhập Đã Tồn Tại");
                return View(_model);
            }
            db.KHACHHANGs.Add(_model);
            db.SaveChanges();
            return RedirectToAction("ListKhachHang", "Admincp");
        }

        [HttpGet]
        public ActionResult UpdateKhachHang(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            int idkh=int.Parse(id);
            KHACHHANG model = db.KHACHHANGs.Where(i => i.MaKH == idkh).FirstOrDefault();
            if (model != null)
                return View(model);
            return RedirectToAction("ListKhachHang","Admincp");
        }

        [HttpPost]
        public ActionResult UpdateKhachHang(KHACHHANG _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            if (db.KHACHHANGs.Where(i => i.TenDN == _model.TenDN).FirstOrDefault() != null
                && db.KHACHHANGs.Where(i => i.TenDN == _model.TenDN).Count()>=2)
            {
                ModelState.AddModelError("TenDN", "Tên Đăng Nhập Đã Tồn Tại");
                return View(_model);
            }
            KHACHHANG model = db.KHACHHANGs.Where(i => i.MaKH == _model.MaKH).FirstOrDefault();
            model.HoTenKH = _model.HoTenKH;
            model.MatKhau = _model.MatKhau;
            model.NgaySinh = _model.NgaySinh;
            model.TenDN = _model.TenDN;
            model.GioiTinh = _model.GioiTinh;
            model.Email = _model.Email;
            model.DiaChi = _model.DiaChi;
            db.SaveChanges();
            return RedirectToAction("ListKhachHang", "Admincp");
        }

        [HttpGet]
        public ActionResult DeleteKhachHang(string id)
        {
            try
            {
                if (Session["admin"] == null)
                    return RedirectToAction("dangnhapadmin", "Admincp");
                int idkh = int.Parse(id);
                KHACHHANG model = db.KHACHHANGs.Where(i => i.MaKH == idkh).FirstOrDefault();
                if (model != null)
                {
                    db.KHACHHANGs.Remove(model);
                    db.SaveChanges();
                }
                TempData["title"] = "Xoá Thành Công";
                return RedirectToAction("ListKhachHang", "Admincp");
            }
            catch {
                TempData["title"] = "Không Thể Xoá";
                return RedirectToAction("ListKhachHang", "Admincp");
            }
            
        }
        #endregion

        #region [Hình thức thanh toán]
        public ActionResult CreateHTTT()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            ViewBag.title = "Tạo Mới Chủ Đề";
            return View();
        }
        [HttpPost]
        public ActionResult CreateHTTT(HTTT _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Thêm mới thành công !";
            db.HTTTs.Add(_model);
            db.SaveChanges();
            return RedirectToAction("ListHTTT", "Admincp");
        }

        [HttpGet]
        public ActionResult DeleteHTTT(string IDHTTT)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Xóa thành công !";
            int t = int.Parse(IDHTTT);
            HTTT model = db.HTTTs.Where(i => i.MaHTTT == t).FirstOrDefault();
            if (model != null)
            {
                db.HTTTs.Remove(model);
                db.SaveChanges();
            }
            return RedirectToAction("ListHTTT", "Admincp");
        }
        [HttpGet]
        public ActionResult UpdateHTTT(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            int t = int.Parse(id);
            HTTT model = db.HTTTs.Where(i => i.MaHTTT == t).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateHTTT(HTTT _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Cập nhật thành công !";
            HTTT model = db.HTTTs.Where(i => i.MaHTTT == _model.MaHTTT).FirstOrDefault();
            if (model != null)
            {
                model.TenHTTT = _model.TenHTTT;
                db.SaveChanges();
            }
            return RedirectToAction("ListHTTT", "Admincp");
        }

        public ActionResult ListHTTT()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            List<HTTT> model = db.HTTTs.ToList();
            return View(model);
        }
        #endregion

        #region [Khuyến Mãi]
        public ActionResult CreateKhuyenMai()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            ViewBag.loai = db.BANHs.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateKhuyenMai(KHUYENMAI _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Thêm mới thành công !";
            db.KHUYENMAIs.Add(_model);
            db.SaveChanges();
            return RedirectToAction("ListKhuyenMai", "Admincp");
        }

        [HttpGet]
        public ActionResult DeleteKhuyenMai(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Xóa thành công !";
            int t = int.Parse(id);
            KHUYENMAI model = db.KHUYENMAIs.Where(i => i.MaKM == t).FirstOrDefault();
            if (model != null)
            {
                db.KHUYENMAIs.Remove(model);
                db.SaveChanges();
            }
            return RedirectToAction("ListKhuyenMai", "Admincp");
        }
        [HttpGet]
        public ActionResult UpdateKhuyenMai(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            ViewBag.loai = db.BANHs.ToList();
            int t = int.Parse(id);
            KHUYENMAI model = db.KHUYENMAIs.Where(i => i.MaKM == t).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateKhuyenMai(KHUYENMAI _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Cập nhật thành công !";
            KHUYENMAI model = db.KHUYENMAIs.Where(i => i.MaKM == _model.MaKM).FirstOrDefault();
            if (model != null)
            {
                model.MaBanh = _model.MaBanh;
                model.GiaGiam = _model.GiaGiam;
                db.SaveChanges();
            }
            return RedirectToAction("ListKhuyenMai", "Admincp");
        }

        public ActionResult ListKhuyenMai()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            List<KHUYENMAI> model = db.KHUYENMAIs.OrderByDescending(m=>m.MaKM).ToList();
            return View(model);
        }
        #endregion

        #region [Liên Hệ]
        public ActionResult CreateLienHe()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View();
        }
        [HttpPost]
        public ActionResult CreateLienHe(LIENHE _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Thêm mới thành công !";
            db.LIENHEs.Add(_model);
            db.SaveChanges();
            return RedirectToAction("ListLienHe", "Admincp");
        }

        [HttpGet]
        public ActionResult DeleteLienHe(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Xóa thành công !";
            int t = int.Parse(id);
            LIENHE model = db.LIENHEs.Where(i => i.MaLienHe == t).FirstOrDefault();
            if (model != null)
            {
                db.LIENHEs.Remove(model);
                db.SaveChanges();
            }
            return RedirectToAction("ListLienHe", "Admincp");
        }
        [HttpGet]
        public ActionResult UpdateLienHe(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            ViewBag.loai = db.BANHs.ToList();
            int t = int.Parse(id);
            LIENHE model = db.LIENHEs.Where(i => i.MaLienHe == t).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateLienHe(LIENHE _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Cập nhật thành công !";
            LIENHE model = db.LIENHEs.Where(i => i.MaLienHe == _model.MaLienHe).FirstOrDefault();
            if (model != null)
            {
                model.HoTen = _model.HoTen;
                model.DienThoai = _model.DienThoai;
                model.Email = _model.Email;
                model.Diachi = _model.Diachi;
                model.NoiDung = _model.NoiDung;
                db.SaveChanges();
            }
            return RedirectToAction("ListLienHe", "Admincp");
        }

        public ActionResult ListLienHe()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            List<LIENHE> model = db.LIENHEs.OrderByDescending(m=>m.MaLienHe).ToList();
            return View(model);
        }
        #endregion

        #region [Quảng Cáo]
        public ActionResult CreateQuangCao()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View();
        }
        [HttpPost]
        public ActionResult CreateQuangCao(QUANGCAO _model)
        {
            try
            {

                if (Session["admin"] == null)
                    return RedirectToAction("dangnhapadmin", "Admincp");
                TempData["title"] = "Thêm mới thành công !";
                db.QUANGCAOs.Add(_model);
                db.SaveChanges();
                return RedirectToAction("ListQuangCao", "Admincp");
            }
            catch (Exception ex)
            {
                return View(_model);
            }
        }

        [HttpGet]
        public ActionResult DeleteQuangCao(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Xóa thành công !";
            int t = int.Parse(id);
            QUANGCAO model = db.QUANGCAOs.Where(i => i.STT == t).FirstOrDefault();
            if (model != null)
            {
                db.QUANGCAOs.Remove(model);
                db.SaveChanges();
            }
            return RedirectToAction("ListQuangCao", "Admincp");
        }
        [HttpGet]
        public ActionResult UpdateQuangCao(string id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            ViewBag.loai = db.BANHs.ToList();
            int t = int.Parse(id);
            QUANGCAO model = db.QUANGCAOs.Where(i => i.STT == t).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateQuangCao(QUANGCAO _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TempData["title"] = "Cập nhật thành công !";
            QUANGCAO model = db.QUANGCAOs.Where(i => i.STT == _model.STT).FirstOrDefault();
            if (model != null)
            {
                model.TenCTY = _model.TenCTY;
                model.HinhMinhHoa = _model.HinhMinhHoa;
                model.Href = _model.Href;
                model.NgayBatDau = _model.NgayBatDau;
                model.NgayKetThuc = _model.NgayKetThuc;
                db.SaveChanges();
            }
            return RedirectToAction("ListQuangCao", "Admincp");
        }

        public ActionResult ListQuangCao()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            List<QUANGCAO> model = db.QUANGCAOs.OrderByDescending(m=>m.STT).ToList();
            return View(model);
        }
        #endregion

        #region [Quản Trị]
        public ActionResult ListQuanTri()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View(db.QUANTRIs.ToList());
        }
        public ActionResult CreateQuanTri()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View();
        }
        [HttpPost]
        public ActionResult CreateQuanTri(QUANTRI _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            db.QUANTRIs.Add(_model);
            db.SaveChanges();
            TempData["title"] = "Thêm mới thành công !";
            return RedirectToAction("ListQuanTri");
        }
        [HttpGet]
        public ActionResult UpdateQuanTri(int Id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            QUANTRI model = db.QUANTRIs.Where(m => m.MaQT == Id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateQuanTri(QUANTRI _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            QUANTRI model = db.QUANTRIs.Where(m => m.MaQT == _model.MaQT).FirstOrDefault();
            model.TenDN = _model.TenDN;
            model.MatKhau = _model.MatKhau;
            db.SaveChanges();
            TempData["title"] = "Cập nhật thành công !";
            return RedirectToAction("ListQuanTri");
        }
        [HttpGet]
        public ActionResult DeleteQuanTri(int Id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            QUANTRI model = db.QUANTRIs.Where(m => m.MaQT == Id).FirstOrDefault();
            db.QUANTRIs.Remove(model);
            db.SaveChanges();
            TempData["title"] = "Xóa thành công !";
            return RedirectToAction("ListQuanTri");
        }
        #endregion

        #region [Tin Tức]
        public ActionResult ListTinTuc()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View(db.TINTUCs.OrderByDescending(m=>m.MaTinTuc).ToList());
        }
        public ActionResult CreateTinTuc()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateTinTuc(TINTUC _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            string t = Session["admin"].ToString();
            QUANTRI qt = db.QUANTRIs.Where(m => m.TenDN == t).FirstOrDefault();
            _model.MaQT = qt.MaQT;
            db.TINTUCs.Add(_model);
            db.SaveChanges();
            TempData["title"] = "Thêm mới thành công !";
            return RedirectToAction("ListTinTuc");
        }
        [HttpGet]
        public ActionResult UpdateTinTuc(int Id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TINTUC model = db.TINTUCs.Where(m => m.MaTinTuc == Id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateTinTuc(TINTUC _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TINTUC model = db.TINTUCs.Where(m => m.MaTinTuc == _model.MaTinTuc).FirstOrDefault();
            string t=Session["admin"].ToString();
            QUANTRI qt = db.QUANTRIs.Where(m => m.TenDN == t).FirstOrDefault();
            model.MaQT = qt.MaQT;
            model.NgayDang = _model.NgayDang;
            model.NoiDung = _model.NoiDung;
            db.SaveChanges();
            TempData["title"] = "Cập nhật thành công !";
            return RedirectToAction("ListTinTuc");
        }
        [HttpGet]
        public ActionResult DeleteTinTuc(int Id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            TINTUC model = db.TINTUCs.Where(m => m.MaTinTuc == Id).FirstOrDefault();
            db.TINTUCs.Remove(model);
            db.SaveChanges();
            TempData["title"] = "Xóa thành công !";
            return RedirectToAction("ListTinTuc");
        }
        #endregion

        #region [Ý Kiến]
        public ActionResult ListYKien()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View(db.YKIENs.OrderByDescending(m=>m.NgayYKien).ToList());
        }
        public ActionResult CreateYKien()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View();
        }
        [HttpPost]
        public ActionResult CreateYKien(YKIEN _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            _model.NgayYKien = DateTime.Now;
            db.YKIENs.Add(_model);
            db.SaveChanges();
            TempData["title"] = "Thêm mới thành công !";
            return RedirectToAction("ListYKien");
        }
        [HttpGet]
        public ActionResult UpdateYKien(int Id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            YKIEN model = db.YKIENs.Where(m => m.MaYKien == Id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateYKien(YKIEN _model)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            YKIEN model = db.YKIENs.Where(m => m.MaYKien == _model.MaYKien).FirstOrDefault();
            model.MaBanh = _model.MaBanh;
            model.HoTen = _model.HoTen;
            model.NgayYKien = _model.NgayYKien;
            model.NoiDung = _model.NoiDung;
            db.SaveChanges();
            TempData["title"] = "Cập nhật thành công !";
            return RedirectToAction("ListYKien");
        }
        [HttpGet]
        public ActionResult DeleteYKien(int Id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            YKIEN model = db.YKIENs.Where(m => m.MaYKien == Id).FirstOrDefault();
            db.YKIENs.Remove(model);
            db.SaveChanges();
            TempData["title"] = "Xóa thành công !";
            return RedirectToAction("ListYKien");
        }
        #endregion

        #region [Hóa Đơn]
        public ActionResult ListHoaDon()
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            return View(db.DONDHs.OrderByDescending(m => m.NgayDH).ToList());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ActiveGiaoHang(string idHD)
        {
            if (Session["admin"] == null)
                return RedirectToAction("dangnhapadmin", "Admincp");
            int id = int.Parse(idHD);
            DONDH model = db.DONDHs.Where(m => m.MaDH == id).FirstOrDefault();
            if(model!=null)
            {
                model.DaGiao = !model.DaGiao;
                model.NgayGiaoHang = DateTime.Now;
                db.SaveChanges();
            }
           
            return RedirectToAction("ListHoaDon");
        }
        #endregion

        public ActionResult Dangnhapadmin()
        {
            if (Session["admin"] != null)
                return RedirectToAction("Index","Admincp");
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhapadmin(QUANTRI _model)
        {
            if (db.QUANTRIs.Where(m => m.TenDN == _model.TenDN && m.MatKhau == _model.MatKhau).FirstOrDefault() != null)
            {
                Session["admin"] = _model.TenDN;
                return RedirectToAction("Index", "Admincp");
            }
            else
            {
                return View(_model);
            }
        }
        [HttpGet]
        public ActionResult Dangxuat()
        {
            Session["admin"] = null;
            return RedirectToAction("Index", "Admincp");
        }
        

    }
}
