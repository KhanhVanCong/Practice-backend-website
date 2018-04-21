using HTLegal.Models;
using HTLegal.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLegal.Areas.Admin.Controllers
{
    public class ConfigurationController : Controller
    {
        //
        // GET: /Admin/Configuration/

        public ActionResult Index()
        {
            #region check permission
            var p = EAuthority.CheckPermission("info");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["info_view"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Thành viên" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var webInfo = (from w in db.E_WebsiteConfiguration
                           select w).FirstOrDefault();
            return View(webInfo);
        }

        [HttpPost]
        public ActionResult Save(E_WebsiteConfiguration webConfig)
        {
            #region check permission
            var p = EAuthority.CheckPermission("info");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["info_update"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Thành viên" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                var webInfo = db.E_WebsiteConfiguration.FirstOrDefault();
                webInfo.EmailAdmin = webConfig.EmailAdmin;
                webInfo.SupportEmail = webConfig.SupportEmail;
                webInfo.SupportEmailPassword = webConfig.SupportEmailPassword;
                webInfo.AddressCompany = webConfig.AddressCompany;
                webInfo.Phone = webConfig.Phone;
                webInfo.EmailCompany = webConfig.EmailCompany;
                webInfo.Website = webConfig.Website;
                webInfo.AboutUs = webConfig.AboutUs;
                webInfo.CopyRight = webConfig.CopyRight;
                webInfo.FaceBook = webConfig.FaceBook;
                webInfo.LinkedIn = webConfig.LinkedIn;
                webInfo.GooglePlus = webConfig.GooglePlus;
                webInfo.Twitter = webConfig.Twitter;
                webInfo.LegalProblemContext = webConfig.LegalProblemContext;
                webInfo.MetaDescription = webConfig.MetaDescription;
                db.Entry(webInfo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Cập nhật dữ liệu thành công";
            }
            catch (Exception e)
            {
                TempData["error"] = "Xảy ra lỗi! " + e.Message;
                throw;
            }

            return RedirectToAction("Index");

        }

    }
}
