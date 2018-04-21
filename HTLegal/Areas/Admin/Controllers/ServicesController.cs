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
    public class ServicesController : UploadDeleteFileController
    {
        //
        // GET: /Admin/Services/

        #region Services show Icon
        public ActionResult Index()
        {
            #region check permission
            var p = EAuthority.CheckPermission("services");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["services_viewicon"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Dịch vụ" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var services = db.E_Feature_Services.ToList();
            return View(services);
        }

        public ActionResult Create(int? id)
        {
            #region check permission
            var p = EAuthority.CheckPermission("services");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }          
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                if(id > 0 )
                {
                    var services = db.E_Feature_Services.Find(id);
                    return View(services);
                }
                else
                {
                    return View(new E_Feature_Services());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Create(E_Feature_Services services)
        {
            try
            {
                string pic = UploadPicture("picture", DateTime.Now.ToString("hh_mm_ss"), "~/content/HTLegal/img/practice/icon");
                HTLegalContext db = new HTLegalContext();

                if (services.Id > 0)
                {
                    var servicesUpdate = db.E_Feature_Services.Find(services.Id);
                    if (string.IsNullOrEmpty(pic) == false)
                    {
                        servicesUpdate.Icon = pic;
                    }
                    servicesUpdate.Title = services.Title;
                    servicesUpdate.Description = services.Description;
                    db.Entry(servicesUpdate).State = System.Data.Entity.EntityState.Modified;
                    TempData["success"] = "Lưu thành công !";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    var servicesNew = new E_Feature_Services();
                    servicesNew.Icon = pic;
                    servicesNew.Title = services.Title;
                    servicesNew.Description = services.Description;
                    db.E_Feature_Services.Add(servicesNew);
                    ModelState.Clear();
                    TempData["success"] = "Lưu thành công !";
                    db.SaveChanges();
                    return View(new E_Feature_Services());
                }


            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
                return RedirectToAction("Index");
            }


        }

        public ActionResult deletePicture(int id)
        {
            try
            {
                HTLegalContext db = new HTLegalContext();
                E_Feature_Services services = db.E_Feature_Services.Find(id);
                if (string.IsNullOrWhiteSpace(services.Icon) == false)
                {
                    string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/practice/icon"), services.Icon);
                    FileInfo f = new FileInfo(sPath);
                    if (f.Exists)
                    {
                        f.Delete();
                    }
                    services.Icon = null;
                    db.Entry(services).State = EntityState.Modified;
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

        public ActionResult DeleteServices(int id)
        {
            #region check permission
            var p = EAuthority.CheckPermission("services");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["services_deleteicon"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Dịch vụ" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                var services = db.E_Feature_Services.Find(id);
                if (services != null)
                {
                    if (string.IsNullOrWhiteSpace(services.Icon) == false)
                    {
                        string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/practice/icon"), services.Icon);
                        FileInfo f = new FileInfo(sPath);
                        if (f.Exists)
                        {
                            f.Delete();
                        }
                    }
                    db.E_Feature_Services.Remove(services);
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

        #region Bài Viết
        public ActionResult News()
        {
            #region check permission
            var p = EAuthority.CheckPermission("services");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["services_view_news"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Dịch vụ" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var news = db.E_Services.ToList();
            return View(news);
        }
        public ActionResult CreateNew(int? id = 0)
        {
            #region check permission
            var p = EAuthority.CheckPermission("services");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }            
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            if (id > 0)
            {
                var service = db.E_Services.Find(id);
                return View(service);
            }
            else
            {
                return View(new E_Services());
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateNew(E_Services service)
        {

            try
            {
                string pic = UploadPicture("picture", DateTime.Now.ToString("HH_mm_ss"), "~/content/HTLegal/img/practice");
                //string pic_detail = UploadPicture("picture_470x638", DateTime.Now.ToString("HH_mm_ss"), "~/content/HTLegal/img/attorneys/detail");
                HTLegalContext db = new HTLegalContext();

                if (service.Id > 0)
                {
                    var servicesUpdate = db.E_Services.Find(service.Id);
                    if (string.IsNullOrEmpty(pic) == false)
                    {
                        servicesUpdate.Image_374x219 = pic;
                    }
                    //if (string.IsNullOrEmpty(pic_detail) == false)
                    //{
                    //    attorneyEdit.Picture_470x638 = pic_detail;
                    //}
                    servicesUpdate.Title = service.Title;
                    servicesUpdate.Description = service.Description;
                    servicesUpdate.Content = service.Content;
                    servicesUpdate.IsActiveHome = service.IsActiveHome;
                    db.Entry(servicesUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Cập nhật dữ liệu thành công";
                    return RedirectToAction("News");
                }
                else
                {
                    var serviceNew = new E_Services();
                    serviceNew.Title = service.Title;
                    serviceNew.Description = service.Description;
                    serviceNew.Content = service.Content;
                    serviceNew.IsActiveHome = service.IsActiveHome;
                    db.E_Services.Add(serviceNew);
                    db.SaveChanges();
                    ModelState.Clear();
                    TempData["success"] = "Cập nhật dữ liệu thành công";
                    return View(new E_Services());
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Xảy ra lỗi ! " + e.Message;
                return RedirectToAction("News");
            }

        }


        public ActionResult DeletePictureNew(int id)
        {
            try
            {

                HTLegalContext db = new HTLegalContext();
                E_Services ser = db.E_Services.Find(id);
                if (string.IsNullOrWhiteSpace(ser.Image_374x219) == false)
                {
                    string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/practice"), ser.Image_374x219);
                    FileInfo f = new FileInfo(sPath);
                    if (f.Exists)
                    {
                        f.Delete();
                    }
                    ser.Image_374x219 = null;
                    db.Entry(ser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Xóa hình thành công";
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
            }
            return RedirectToAction("CreateNew", new { id = id });

        }


        public ActionResult DeleteNew(int id)
        {

            #region check permission
            var p = EAuthority.CheckPermission("services");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["services_delete_news"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Dịch vụ" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                var ser = db.E_Services.Find(id);
                if (ser != null)
                {
                    if (string.IsNullOrWhiteSpace(ser.Image_374x219) == false)
                    {
                        string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/practice"), ser.Image_374x219);
                        FileInfo f = new FileInfo(sPath);
                        if (f.Exists)
                        {
                            f.Delete();
                        }
                    }

                    db.E_Services.Remove(ser);
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
            return RedirectToAction("News");

        }


        #endregion


        #region Thông tin chung
        public ActionResult Content()
        {
            #region check permission
            var p = EAuthority.CheckPermission("services");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["services_info_view"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Dịch vụ" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var content = db.E_CMS_Services.FirstOrDefault();
            return View(content);
        }

        [HttpPost]
        public ActionResult UpdateContent(E_CMS_Services attorney)
        {

            try
            {
                HTLegalContext db = new HTLegalContext();
                var update = db.E_CMS_Services.Find(attorney.Id);
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
                E_CMS_Services services = db.E_CMS_Services.Find(id);

                if (string.IsNullOrWhiteSpace(services.Logo_1920x400) == false)
                {
                    string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img"), services.Logo_1920x400);
                    FileInfo f = new FileInfo(sPath);
                    if (f.Exists)
                    {
                        f.Delete();
                    }
                    services.Logo_1920x400 = null;
                }
                db.Entry(services).State = EntityState.Modified;
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
