using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnhThuBakery.Models;

namespace AnhThuBakery.Controllers
{
    public class SharedController : Controller
    {
        //
        // GET: /Shared/
        CSDLBanhKemEntities5 db = new CSDLBanhKemEntities5();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialMenu()
        {
            ViewBag.chude = db.CHUDEs.ToList();
            ViewBag.loai = db.LOAIBANHs.ToList();
            return View();
        }

        public ActionResult PartialQuangCao()
        {
            ViewBag.quangcao = db.QUANGCAOs.ToList();
            return View();
        }

        public ActionResult ViewCard()
        {
            return View();
        }

    }
}
