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
    public class HomePageController : Controller
    {
        //
        // GET: /Admin/HomePage/
        // Banner
        #region Banner
        public ActionResult Index()
        {
            #region check permission
            var p = EAuthority.CheckPermission("home");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["home_view_banner"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Trang chủ" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var home_Config = (from h in db.E_Home_Slide
                               select h).ToList();
            return View(home_Config);
        }

        private string UploadPicture(string filename, string empid, string spath)
        {
            HttpPostedFileBase file = HttpContext.Request.Files[filename];

            if (file.FileName != "")
            {
                if (file.ContentLength > 20971520)
                {
                    throw new Exception("File upload content length larger than allowed is 20 MB");
                }
                else if (file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/bmp")
                {
                    string fName = file.FileName.Replace(" ", "_");
                    fName = empid + "_" + Path.GetFileName(fName);
                    string fullPath = Path.Combine(Server.MapPath(spath), fName);
                    file.SaveAs(fullPath);
                    return fName;
                }

            }
            return string.Empty;
        }

        public ActionResult DeleteYourPicture(int id)
        {
            try
            {
                HTLegalContext db = new HTLegalContext();
                E_Home_Slide home = db.E_Home_Slide.Find(id);
                if (string.IsNullOrWhiteSpace(home.Logo_1920x940_Slide) == false)
                {
                    string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/slider"), home.Logo_1920x940_Slide);
                    FileInfo f = new FileInfo(sPath);
                    if (f.Exists)
                    {
                        f.Delete();
                    }
                    home.Logo_1920x940_Slide = null;
                    db.Entry(home).State = EntityState.Modified;
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

        public ActionResult Create(int? id = 0)
        {
            #region check permission
            var p = EAuthority.CheckPermission("home");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }          
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            if (id > 0)
            {
                var homePage = db.E_Home_Slide.Find(id);
                return View(homePage);
            }
            else
            {
                //var homePagenNew = new E_CMS_Home();
                return View(new E_Home_Slide());
            }
        }

        [HttpPost]
        public ActionResult Create(E_Home_Slide homePage)
        {
            try
            {
                string pic = UploadPicture("picture", DateTime.Now.ToString("HH_mm_ss"), "~/content/HTLegal/img/slider");
                HTLegalContext db = new HTLegalContext();

                if (homePage.Id > 0)
                {
                    var homePageEdit = db.E_Home_Slide.Find(homePage.Id);
                    if (string.IsNullOrEmpty(pic) == false)
                    {
                        homePageEdit.Logo_1920x940_Slide = pic;
                    }
                    homePageEdit.Title_Slide = homePage.Title_Slide;
                    homePageEdit.SubTitle_Slide = homePage.SubTitle_Slide;
                    db.Entry(homePageEdit).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Cập nhật dữ liệu thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    var homePageNew = new E_Home_Slide();
                    homePageNew.Logo_1920x940_Slide = pic;
                    homePageNew.Title_Slide = homePage.Title_Slide;
                    homePageNew.SubTitle_Slide = homePage.SubTitle_Slide;
                    db.E_Home_Slide.Add(homePageNew);
                    db.SaveChanges();
                    ModelState.Clear();
                    TempData["success"] = "Cập nhật dữ liệu thành công";
                    return View(new E_Home_Slide());
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Xảy ra lỗi ! " + e.Message;
                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(int id)
        {
            #region check permission
            var p = EAuthority.CheckPermission("home");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["home_delete_banner"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Trang chủ" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                var homeSlide = db.E_Home_Slide.Find(id);
                if (homeSlide != null)
                {
                    if (string.IsNullOrWhiteSpace(homeSlide.Logo_1920x940_Slide) == false)
                    {
                        string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/slider"), homeSlide.Logo_1920x940_Slide);
                        FileInfo f = new FileInfo(sPath);
                        if (f.Exists)
                        {
                            f.Delete();
                        }
                    }
                    db.E_Home_Slide.Remove(homeSlide);
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

        //Nội dung trang home
        #region ContentHome
        public ActionResult Content()
        {
            #region check permission
            var p = EAuthority.CheckPermission("home");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["home_view_content"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Trang chủ" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var homeContent = db.E_Home_Content.FirstOrDefault();
            return View(homeContent);
        }

        [HttpPost]
        public ActionResult SaveContent(E_Home_Content homeContent)
        {
           
            try
            {
                string pic = UploadPicture("picture", DateTime.Now.ToString("hh_mm_ss"), "~/content/HTLegal/img");
                HTLegalContext db = new HTLegalContext();
                var homeUpdateContent = db.E_Home_Content.Find(homeContent.Id);
                homeUpdateContent.Welcome = homeContent.Welcome;
                homeUpdateContent.Description = homeContent.Description;
                homeUpdateContent.Info_1 = homeContent.Info_1;
                homeUpdateContent.Info_2 = homeContent.Info_2;
                if (string.IsNullOrEmpty(pic) == false)
                {
                    homeUpdateContent.Image_875x510_RightSubmit = pic;
                }
                homeUpdateContent.Title_FeatureServices = homeContent.Title_FeatureServices;
                homeUpdateContent.Title_Attorney_Area = homeContent.Title_Attorney_Area;
                homeUpdateContent.Title_Blog_Area = homeContent.Title_Blog_Area;
                homeUpdateContent.Title_Form_Submit = homeContent.Title_Form_Submit;
                db.Entry(homeUpdateContent).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Lưu thành công !";
            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;

            }

            return RedirectToAction("Content");
        }

        public ActionResult DeletePictureContent(int id)
        {            
            try
            {
                HTLegalContext db = new HTLegalContext();
                E_Home_Content home = db.E_Home_Content.Find(id);
                if (string.IsNullOrWhiteSpace(home.Image_875x510_RightSubmit) == false)
                {
                    string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img"), home.Image_875x510_RightSubmit);
                    FileInfo f = new FileInfo(sPath);
                    if (f.Exists)
                    {
                        f.Delete();
                    }
                    home.Image_875x510_RightSubmit = null;
                    db.Entry(home).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Cập nhật thành công !";

                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Xảy ra lỗi ! " + e.Message;

            }
            return RedirectToAction("Content");
        }
        #endregion

        //Feedback Khách hàng
        #region FeedBack Client
        public ActionResult Client()
        {
            #region check permission
            var p = EAuthority.CheckPermission("home");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["home_view_client"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Trang chủ" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var client = db.E_Home_FeedbackClient.ToList();

            return View(client);
        }

        public ActionResult CreateClient(int? id)
        {
            #region check permission
            var p = EAuthority.CheckPermission("home");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                if (id > 0)
                {
                    var client = db.E_Home_FeedbackClient.Find(id);
                    return View(client);
                }
                else
                {
                    return View(new E_Home_FeedbackClient());
                }

            }
            catch (Exception )
            {

                throw ;
            }
            
        }

        [HttpPost]
        public ActionResult CreateClient(E_Home_FeedbackClient client)
        {
            try
            {
                string pic = UploadPicture("picture", DateTime.Now.ToString("hh_mm_ss"), "~/Content/HTLegal/img/testimonial");
                HTLegalContext db = new HTLegalContext();

                if (client.Id > 0)
                {
                    var clientUpdate = db.E_Home_FeedbackClient.Find(client.Id);
                    if (string.IsNullOrEmpty(pic) == false)
                    {
                        clientUpdate.Icon_108x108 = pic;
                    }
                    clientUpdate.Content = client.Content;
                    clientUpdate.Author = client.Author;
                    clientUpdate.Order = client.Order;
                    db.Entry(clientUpdate).State = System.Data.Entity.EntityState.Modified;
                    TempData["success"] = "Lưu thành công !";
                    db.SaveChanges();
                    return RedirectToAction("Client");
                }
                else
                {
                    var clienNew = new E_Home_FeedbackClient();
                    clienNew.Icon_108x108 = pic;
                    clienNew.Content = client.Content;
                    clienNew.Author = client.Author;
                    clienNew.Order = client.Order;
                    db.E_Home_FeedbackClient.Add(clienNew);
                    ModelState.Clear();
                    TempData["success"] = "Lưu thành công !";
                    db.SaveChanges();
                    return View(new E_Home_FeedbackClient());
                }
              
                
            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
                return RedirectToAction("Client");
            }

         
        }


        public ActionResult deleteLogoClient(int id)
        {
            try
            {
                HTLegalContext db = new HTLegalContext();
                E_Home_FeedbackClient client = db.E_Home_FeedbackClient.Find(id);
                if (string.IsNullOrWhiteSpace(client.Icon_108x108) == false)
                {
                    string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/testimonial"), client.Icon_108x108);
                    FileInfo f = new FileInfo(sPath);
                    if (f.Exists)
                    {
                        f.Delete();
                    }
                    client.Icon_108x108 = null;
                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Xóa hình thành công";
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
            }
            return RedirectToAction("CreateClient", new { id = id });
        }

        public ActionResult DeleteClient(int id)
        {
            #region check permission
            var p = EAuthority.CheckPermission("home");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["home_delete_client"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Trang chủ" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                var client = db.E_Home_FeedbackClient.Find(id);
                if (client != null)
                {
                    if (string.IsNullOrWhiteSpace(client.Icon_108x108) == false)
                    {
                        string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/testimonial"), client.Icon_108x108);
                        FileInfo f = new FileInfo(sPath);
                        if (f.Exists)
                        {
                            f.Delete();
                        }
                    }
                    db.E_Home_FeedbackClient.Remove(client);
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
            return RedirectToAction("Client");

        }

        #endregion



    }


}
