using HTLegal.Models;
using HTLegal.ViewController;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLegal.Areas.Admin.Controllers
{
    public class AboutUsController : UploadDeleteFileController
    {
        //
        // GET: /Admin/AboutUs/

        public ActionResult Index()
        {

            #region check permission
            var p = EAuthority.CheckPermission("aboutus");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["aboutus_view"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Về Chúng Tôi" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var aboutUs = db.E_CMS_AboutUs.FirstOrDefault();
            return View(aboutUs);
        }

        [HttpPost]
        public ActionResult Save(E_CMS_AboutUs aboutUs)
        {
            #region check permission
            var p = EAuthority.CheckPermission("aboutus");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["about_update"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Về Chúng Tôi" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                var update = db.E_CMS_AboutUs.Find(aboutUs.Id);
                string pic = UploadPicture("picture", DateTime.Now.ToString("hh_mm_ss"), "~/content/HTLegal/img");
                string picLeft = UploadPicture("picture_left", DateTime.Now.ToString("hh_mm_ss"), "~/content/HTLegal/img");
                string picRight = UploadPicture("picture_right", DateTime.Now.ToString("hh_mm_ss"), "~/content/HTLegal/img");
                if (string.IsNullOrEmpty(pic) == false)
                {
                    update.Logo_1920x400 = pic;
                }
                if (string.IsNullOrEmpty(picRight) == false)
                {
                    update.Picture_Right = picRight;
                }
                if (string.IsNullOrEmpty(picLeft) == false)
                {
                    update.Picture_Left = picLeft;
                }
                update.Title = aboutUs.Title;
                update.SubTitle = aboutUs.SubTitle;
                update.Introduce = aboutUs.Introduce;
                db.Entry(update).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Lưu thành công !";

            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
            }

            return RedirectToAction("Index");
        }


        public ActionResult DeleteLogo(int id, string position = "")
        {
            try
            {
                HTLegalContext db = new HTLegalContext();
                E_CMS_AboutUs about = db.E_CMS_AboutUs.Find(id);
                if (position == "") // delete Logo_1920x400
                {
                    if (string.IsNullOrWhiteSpace(about.Logo_1920x400) == false)
                    {
                        string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img"), about.Logo_1920x400);
                        FileInfo f = new FileInfo(sPath);
                        if (f.Exists)
                        {
                            f.Delete();
                        }
                        about.Logo_1920x400 = null;
                    }
                }
                else
                {
                    if (position == "right")
                    {
                        if (string.IsNullOrWhiteSpace(about.Picture_Right) == false)
                        {
                            string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img"), about.Picture_Right);
                            FileInfo f = new FileInfo(sPath);
                            if (f.Exists)
                            {
                                f.Delete();
                            }
                            about.Picture_Right = null;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(about.Picture_Left) == false)
                        {
                            string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img"), about.Picture_Left);
                            FileInfo f = new FileInfo(sPath);
                            if (f.Exists)
                            {
                                f.Delete();
                            }
                            about.Picture_Left = null;
                        }
                    }
                }
                db.Entry(about).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Cập nhật thành công !";
            }
            catch (Exception e)
            {
                TempData["error"] = "Xảy ra lỗi ! " + e.Message;

            }
            return RedirectToAction("Index");
        }
    }
}
