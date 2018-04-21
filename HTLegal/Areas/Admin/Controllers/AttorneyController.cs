using HTLegal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTLegal.Areas.Admin.Controllers;
using System.IO;
using HTLegal.ViewController;
using System.Data.Entity;

namespace HTLegal.Areas.Admin.Controllers
{
    public class AttorneyController : UploadDeleteFileController
    {
        //
        // GET: /Admin/Attorney/
        #region Luật sư
        public ActionResult Index()
        {
            #region check permission
            var p = EAuthority.CheckPermission("attorney");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["attorney_view"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Luật sư" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var attorneyList = db.E_Attorneys.ToList();
            return View(attorneyList);
        }

        public ActionResult Create(int? id = 0)
        {
            #region check permission
            var p = EAuthority.CheckPermission("attorney");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            #endregion
            HTLegalContext db = new HTLegalContext();
            if (id > 0)
            {
                var attorney = db.E_Attorneys.Find(id);
                return View(attorney);
            }
            else if(TempData["attorneyInput"] != null)
            {
                E_Attorneys att = TempData["attorneyInput"] as E_Attorneys;
                return View(att);
            }
            else
            {              
                return View(new E_Attorneys());
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(E_Attorneys attorney)
        {
            try
            {
                string pic = UploadPicture("picture", DateTime.Now.ToString("HH_mm_ss"), "~/content/HTLegal/img/attorneys");
                HTLegalContext db = new HTLegalContext();

                if (attorney.UserId > 0)
                {
                    var user = db.E_Users.Find(attorney.UserId);
                    user.FirstName = attorney.E_Users.FirstName;
                    user.LastName = attorney.E_Users.LastName;
                    user.FullName = user.FirstName + " " + user.LastName;
                    user.Email = attorney.E_Users.Email;
                    user.CellPhone = attorney.E_Users.CellPhone;

                    var attorneyEdit = db.E_Attorneys.Find(attorney.UserId);
                    if (string.IsNullOrEmpty(pic) == false)
                    {
                        attorneyEdit.Picture_270x288 = pic;
                    }
                    attorneyEdit.FullName = user.FullName;
                    attorneyEdit.Position = attorney.Position;
                    attorneyEdit.PractiveAreas = attorney.PractiveAreas;
                    attorneyEdit.Facebook = attorney.Facebook;
                    attorneyEdit.Google = attorney.Google;
                    attorneyEdit.Twifter = attorney.Twifter;
                    attorneyEdit.ProfessionalExperiences = Request.Unvalidated["ProfessionalExperiences"];
                    attorneyEdit.IsActiceHome = attorney.IsActiceHome;
                    db.Entry(attorneyEdit).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Cập nhật dữ liệu thành công";
                    return RedirectToAction("Index");
                }
                else
                {

                    var maxID = db.E_Users.Max(e => e.Id);
                    var user = new E_Users();
                    user.FirstName = attorney.E_Users.FirstName;
                    user.LastName = attorney.E_Users.LastName;
                    user.FullName = user.FirstName + " " + user.LastName;
                    user.Email = attorney.E_Users.Email;
                    user.CellPhone = attorney.E_Users.CellPhone;
                    user.Password = user.Email + "_HTLegal";
                    user.RolesId = 4;
                    E_Roles r = db.E_Roles.Find(user.RolesId);
                    user.RolesName = r.Name;
                    user.IsActive = true;
                    user.RegisterAt = DateTime.Now;
                    db.E_Users.Add(user);
                    db.SaveChanges();

                    var attorneyNew = new E_Attorneys();
                    attorneyNew.Picture_270x288 = pic;
                    attorneyNew.FullName = user.FullName;
                    attorneyNew.Position = attorney.Position;
                    attorneyNew.PractiveAreas = attorney.PractiveAreas;
                    attorneyNew.Facebook = attorney.Facebook;
                    attorneyNew.Google = attorney.Google;
                    attorneyNew.Twifter = attorney.Twifter;
                    attorneyNew.ProfessionalExperiences = Request.Unvalidated["ProfessionalExperiences"];
                    attorneyNew.IsActiceHome = attorney.IsActiceHome;
                    attorneyNew.UserId = maxID + 1;                    
                    db.E_Attorneys.Add(attorneyNew);
                    db.SaveChanges();
                    ModelState.Clear();
                    TempData["success"] = "Cập nhật dữ liệu thành công";
                    return View(new E_Attorneys());
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Xảy ra lỗi ! " + e.Message;
                TempData["attorneyInput"] = attorney;
                return RedirectToAction("Create");
            }

        }


        public ActionResult DeleteYourPicture(int id)
        {
            try
            {

                HTLegalContext db = new HTLegalContext();
                E_Attorneys att = db.E_Attorneys.Find(id);
                if (string.IsNullOrWhiteSpace(att.Picture_270x288) == false)
                {
                    string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/attorneys"), att.Picture_270x288);
                    FileInfo f = new FileInfo(sPath);
                    if (f.Exists)
                    {
                        f.Delete();
                    }
                    att.Picture_270x288 = null;
                    db.Entry(att).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Xóa hình thành công";
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
            }
            return RedirectToAction("Create", new { id = id });

        }


        public ActionResult DeleteAttorney(int id)
        {

            #region check permission
            var p = EAuthority.CheckPermission("attorney");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["attorney_delete"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Luật sư" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                var attorney = db.E_Attorneys.Find(id);
                var user = attorney.E_Users;
                if (attorney != null)
                {
                    if (string.IsNullOrWhiteSpace(attorney.Picture_270x288) == false)
                    {
                        string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/attorneys"), attorney.Picture_270x288);
                        FileInfo f = new FileInfo(sPath);
                        if (f.Exists)
                        {
                            f.Delete();
                        }
                    }
                    user.IsActive = false;
                    user.BannedAt = DateTime.Now;
                    user.BannedBy = EAuthority.GetCurrentMember().FullName;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.E_Attorneys.Remove(attorney);
                    db.SaveChanges();
                    TempData["success"] = "Xóa Thành công";
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
            }
            return RedirectToAction("Index");

        }

        #endregion


        #region Thông tin chung
        public ActionResult Content()
        {
            #region check permission
            var p = EAuthority.CheckPermission("attorney");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["attorney_view_content"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Luật sư" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var content = db.E_CMS_Attorney.FirstOrDefault();
            return View(content);
        }

        [HttpPost]
        public ActionResult UpdateContent(E_CMS_Attorney attorney)
        {
            try
            {
                HTLegalContext db = new HTLegalContext();
                var update = db.E_CMS_Attorney.Find(attorney.Id);
                string pic = UploadPicture("picture", DateTime.Now.ToString("hh_mm_ss"), "~/content/HTLegal/img");
                if (string.IsNullOrEmpty(pic) == false)
                {
                    update.Logo_1920x400 = pic;
                }
                update.Title = attorney.Title;
                update.SubTitle = attorney.SubTitle;
                update.Content = attorney.Content;
                db.Entry(update).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Lưu thành công !";

            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
            }

            return RedirectToAction("Content");
        }

        public ActionResult DeleteLogo(int id)
        {
            try
            {
                HTLegalContext db = new HTLegalContext();
                E_CMS_Attorney attorney = db.E_CMS_Attorney.Find(id);
                
                    if (string.IsNullOrWhiteSpace(attorney.Logo_1920x400) == false)
                    {
                        string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img"), attorney.Logo_1920x400);
                        FileInfo f = new FileInfo(sPath);
                        if (f.Exists)
                        {
                            f.Delete();
                        }
                        attorney.Logo_1920x400 = null;
                    }                               
                db.Entry(attorney).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Cập nhật thành công !";
            }
            catch (Exception e)
            {
                TempData["error"] = "Xảy ra lỗi ! " + e.Message;

            }
            return RedirectToAction("Content");
        }
        #endregion




    }
}
