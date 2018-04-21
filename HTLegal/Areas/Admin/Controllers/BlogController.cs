using HTLegal.Models;
using HTLegal.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTLegal.ViewController;
using System.IO;
using System.Data.Entity;

namespace HTLegal.Areas.Admin.Controllers
{
    public class BlogController : UploadDeleteFileController
    {
        //
        // GET: /Admin/Blog/

        #region Banner
        public ActionResult Index()
        {
            #region check permission
            var p = EAuthority.CheckPermission("blog");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["blog_changebanner"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Banner Blog" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var blog = db.E_CMS_Blog.FirstOrDefault();
            return View(blog);
        }

        [HttpPost]
        public ActionResult Save()
        {          
            #region check permission
            var p = EAuthority.CheckPermission("blog");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["blog_changebanner"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Banner Blog" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                var blog = db.E_CMS_Blog.FirstOrDefault();
                string pic = UploadPicture("picture", DateTime.Now.ToString("hh_mm_ss"), "~/content/HTLegal/img");
                if (string.IsNullOrEmpty(pic) == false)
                {
                    blog.Logo_1920x400 = pic;
                }
                db.Entry(blog).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Lưu thành công !";
            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Blog
        public ActionResult Blog(int? page, int? rpp)
        {
            #region check permission
            var p = EAuthority.CheckPermission("blog");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["blog_view"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Blog" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var searchInfo = Request["searchInfo"] ?? "";
            ViewBag.Search = Request["searchInfo"];
            var blog = db.E_Blog.ToList().Where(b => b.Title.Replace(" ","").ToLower().Contains(searchInfo.Replace(" ","").ToLower()) == true || searchInfo == "" || b.CreatedBy.Replace(" ","").ToLower().Contains(searchInfo.Replace(" ","").ToLower())==true).OrderByDescending(b => b.IsActive).ThenByDescending(b=>b.CreatedAt).ToList();
            int _page, _rpp, take, skip;
            int totalRecords = blog.Count();
            ECommon.PagedList(page, rpp, totalRecords, out _page, out _rpp, out skip, out take);
            TempData["totalRecords"] = totalRecords;
            TempData["rpp"] = _rpp;
            TempData["page"] = _page;
            blog = blog.Skip(skip).Take(take).ToList();
            return View(blog);

        }

        public ActionResult CreateBlog(int? Id)
        {
            #region check permission
            var p = EAuthority.CheckPermission("blog");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["blog_update"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Thành viên" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var detail = db.E_Blog.Find(Id);
            return View(detail);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult CreateBlog()
        {
            try
            {
                HTLegalContext db = new HTLegalContext();
                string pic = UploadPicture("picture", DateTime.Now.ToString("hh_mm_ss"), "~/content/HTLegal/img/blog");
                var Id = int.Parse(Request["Id"]);
                if (Id > 0)
                {
                    var update = db.E_Blog.Find(Id);
                    if (string.IsNullOrEmpty(pic) == false)
                    {
                        update.Picture_810x305 = pic;
                    }
                    update.Title = Request["title"];
                    update.Description = Request["Description"];
                    update.Content = Request.Unvalidated["Content"];
                    update.UpdateAt = DateTime.Now;
                    update.UpdateBy = Request["fullname"];

                    db.Entry(update).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Lưu thành công !";
                }
                else
                {
                    var blogNew = new E_Blog();
                    blogNew.Picture_810x305 = pic;
                    blogNew.Title = Request["title"];
                    blogNew.Description = Request["mota"];
                    blogNew.Content = Request.Unvalidated["message"];
                    blogNew.CreatedAt = DateTime.Now;
                    blogNew.CreatedBy = Request["fullname"];
                    blogNew.IsActive = true;
                    db.E_Blog.Add(blogNew);
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
            }

            return RedirectToAction("Blog");
        }

        public ActionResult DeleteBlog(int? id = 0, string fullname = "")
        {
            #region check permission
            var p = EAuthority.CheckPermission("blog");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["blog_delete"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Thành viên" });
            }
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var blog = db.E_Blog.Find(id);
            blog.IsActive = false;
            blog.DisabledBy = fullname;
            blog.DisabledAt = DateTime.Now;
            db.Entry(blog).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["success"] = "Xóa Thành Công !";
            return RedirectToAction("Blog");
        }

        public ActionResult DeletePicture(int id)
        {
            try
            {
                HTLegalContext db = new HTLegalContext();
                E_Blog blog = db.E_Blog.Find(id);
                if (string.IsNullOrWhiteSpace(blog.Picture_810x305) == false)
                {
                    string sPath = Path.Combine(Server.MapPath("~/content/HTLegal/img/blog"), blog.Picture_810x305);
                    FileInfo f = new FileInfo(sPath);
                    if (f.Exists)
                    {
                        f.Delete();
                    }
                    blog.Picture_810x305 = null;
                    db.Entry(blog).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Cập nhật thành công !";
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Xảy ra lỗi ! " + e.Message;

            }
            return RedirectToAction("CreateBlog", new { id = id });
        }

        public ActionResult LockComment(int? id = 0, string fullname = "")
        {
            #region check permission
            var p = EAuthority.CheckPermission("blog");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["blog_lockcomment"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Khóa bình luận" });
            }
            ViewBag.p = p;
            #endregion
            int? blogId = 0;
            try
            {
                HTLegalContext db = new HTLegalContext();
                var comm = db.E_Blog_Reply.Find(id);
                blogId = comm.BlogId;
                comm.DisabledAt = DateTime.Now;
                comm.IsActive = false;
                comm.DisabledBy = fullname;
                db.Entry(comm).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Lưu thành công !";
            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;               
            }

            return RedirectToAction("CreateBlog", new { Id = blogId });
        }

        public ActionResult ActiveComment(int? id = 0, string fullname = "")
        {
            #region check permission
            var p = EAuthority.CheckPermission("blog");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["blog_reactivecomment"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Kích hoạt bình luận" });
            }
            ViewBag.p = p;
            #endregion
            int? blogId = 0;
            try
            {
                HTLegalContext db = new HTLegalContext();
                var comm = db.E_Blog_Reply.Find(id);
                blogId = comm.BlogId;
                comm.ReActiveAt = DateTime.Now;
                comm.IsActive = true;
                comm.ReActiveBy = fullname;
                db.Entry(comm).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Lưu thành công !";
            }
            catch (Exception e)
            {
                TempData["error"] = "Lỗi ! " + e.Message;
            }

            return RedirectToAction("CreateBlog", new { Id = blogId });
        }

        #endregion

    }
}
