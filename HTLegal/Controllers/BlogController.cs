using HTLegal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTLegal.Areas.Admin.Controllers;
using HTLegal.ViewController;
using System.IO;
using System.Data.Entity;
using HTLegal.ViewController;

namespace HTLegal.Controllers
{
    public class BlogController : UploadDeleteFileController
    {
        //
        // GET: /Blog/

        public ActionResult Index(int? page)
        {
            #region check permission
            var p = EAuthority.CheckPermission("blog");           
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var blog = db.E_Blog.Where(b => b.IsActive == true).OrderByDescending( b => b.CreatedAt).ToList();
            int rpp = 8;
            int _page, _rpp, take, skip;
            int totalRecords = blog.Count();
            ECommon.PagedList(page, rpp, totalRecords, out _page, out _rpp, out skip, out take);
            TempData["totalRecords"] = totalRecords;
            TempData["page"] = _page;
            blog = blog.Skip(skip).Take(take).ToList();
            return View(blog);
        }

        public ActionResult Detail(int? Id)
        {
            #region check permission
            var p = EAuthority.CheckPermission("blog");
            ViewBag.p = p;
            #endregion
            HTLegalContext db = new HTLegalContext();
            var detail = db.E_Blog.Find(Id);
            return View(detail);

        }

        [HttpPost]
        public ActionResult SubmitComment()
        {
            HTLegalContext db = new HTLegalContext();
            var reply = new E_Blog_Reply();
            reply.BlogId = int.Parse(Request["Id"]);
            reply.CreatedBy = Request["name"];
            reply.Email = Request["email"];
            reply.Phone = Request["phone"] ?? "";
            reply.CreatedAt = DateTime.Now;
            reply.Content = Request["comment"];
            reply.IsActive = true;
            db.E_Blog_Reply.Add(reply);
            db.SaveChanges();
            return RedirectToAction("Detail", new { id = reply.BlogId });
        }

        public ActionResult CreateBlog(int? id = 0)
        {
            HTLegalContext db = new HTLegalContext();
            if (id > 0)
            {
                var blog = db.E_Blog.Find(id);
                return View(blog);
            }
            else
            {
                return View(new E_Blog());
            }
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
                    update.Description = Request["mota"];
                    update.Content = Request.Unvalidated["message"];
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

            return RedirectToAction("Index");
        }




        public ActionResult DeleteBlog(int? id = 0, string fullname = "")
        {
            HTLegalContext db = new HTLegalContext();
            var blog = db.E_Blog.Find(id);
            blog.IsActive = false;
            blog.DisabledBy = fullname;
            blog.DisabledAt = DateTime.Now;
            db.Entry(blog).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
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
            return RedirectToAction("CreateBlog", new {id= id });
        }

        public ActionResult LockComment(int? id = 0, string fullname = "")
        {
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

            return RedirectToAction("Detail", new { Id = blogId });
        }

    }
}
