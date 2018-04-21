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
    public class ContactUsController : UploadDeleteFileController
    {
        //
        // GET: /Admin/ContactUs/

        public ActionResult Index()
        {
            #region check permission
            var p = EAuthority.CheckPermission("contact");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["contact_view"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Thành viên" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var contact = db.E_CMS_ConTactUs.FirstOrDefault();
            return View(contact);
        }

        [HttpPost]
        public ActionResult Save(E_CMS_ConTactUs contact)
        {
            #region check permission
            var p = EAuthority.CheckPermission("contact");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["contact_update"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Thành viên" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                var update = db.E_CMS_ConTactUs.Find(contact.Id);
                string pic = UploadPicture("picture", DateTime.Now.ToString("hh_mm_ss"), "~/content/HTLegal/img");
                string picRight = UploadPicture("picture_right", DateTime.Now.ToString("hh_mm_ss"), "~/content/HTLegal/img");
                if (string.IsNullOrEmpty(pic) == false)
                {
                    update.Logo_1920x400 = pic;
                }
                if (string.IsNullOrEmpty(picRight) == false)
                {
                    update.Picture_141x141 = picRight;
                }
                update.Title = contact.Title;
                update.SubTitle = contact.SubTitle;
                update.Content = contact.Content;
                update.Map = contact.Map;
                update.FullName = contact.FullName;
                update.Position = contact.Position;
                update.Phone = contact.Phone;
                update.Email = contact.Email;
                update.Facebook = contact.Facebook;
                update.Twifter = contact.Twifter;
                update.Google = contact.Google;
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
            #region check permission
            var p = EAuthority.CheckPermission("user");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["user_view"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Thành viên" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                E_CMS_ConTactUs contact = db.E_CMS_ConTactUs.Find(id);
                if (position == "") // delete Logo_1920x400
                {
                    if (string.IsNullOrWhiteSpace(contact.Logo_1920x400) == false)
                    {
                        string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img"), contact.Logo_1920x400);
                        FileInfo f = new FileInfo(sPath);
                        if (f.Exists)
                        {
                            f.Delete();
                        }
                        contact.Logo_1920x400 = null;
                    }
                }
                else
                {
                    if (position == "right")
                    {
                        if (string.IsNullOrWhiteSpace(contact.Picture_141x141) == false)
                        {
                            string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img"), contact.Picture_141x141);
                            FileInfo f = new FileInfo(sPath);
                            if (f.Exists)
                            {
                                f.Delete();
                            }
                            contact.Picture_141x141 = null;
                        }
                    }                   
                }
                db.Entry(contact).State = EntityState.Modified;
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
