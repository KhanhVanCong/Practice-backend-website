using HTLegal.Models;
using HTLegal.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLegal.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: admin/Home
        //
        // GET: /admin/Home/

        #region login-logout-forgot password - forbidden

        public ActionResult Index()
        {
            //HTLegalContext db = new HTLegalContext();
            //var user = db.E_Users.Where(u => u.Email.Equals("sonht10@yahoo.com") && u.Password.Equals("123456") && u.IsActive == true).FirstOrDefault();
            //Session["member"] = user;

            //return RedirectToAction("index", "users");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            string email = Request["email"];
            string password = Request["password"];
            HTLegalContext db = new HTLegalContext();
            var user = db.E_Users.Where(u => u.Email.Equals(email) && u.Password.Equals(password) && u.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                Session["member"] = user;
                if (Request.Cookies["htlegal_email"] == null)
                {
                    HttpCookie ckemail = new HttpCookie("htlegal_email", email);
                    HttpCookie ckpass = new HttpCookie("htlegal_pass", password);
                    ckemail.Expires = DateTime.Now.AddDays(1);
                    ckpass.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(ckemail);
                    Response.Cookies.Add(ckpass);
                }
                
                return RedirectToAction("index", "users", new { area = "admin" });
            }
            else
            {
                TempData["login_err"] = "Login failure";
                return RedirectToAction("index");
            }
        }


        public ActionResult Logout()
        {
            Session.RemoveAll();
            HttpCookie ckemail = Response.Cookies["htlegal_email"];
            ckemail.Value = null;
            HttpCookie ckpass = Response.Cookies["htlegal_pass"];
            ckpass.Value = null;
            return RedirectToAction("index", "home", new { area = "admin" });
        }


        public JsonResult ForgotPassword(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) == true)
                {
                    throw new Exception("Email không hợp lệ");
                }
                HTLegalContext db = new HTLegalContext();
                //employee
                var user = db.E_Users.Where(u => u.Email.Equals(email.Trim(), StringComparison.InvariantCultureIgnoreCase) == true).FirstOrDefault();
                if (user != null)
                {
                    string result = SendEmail.ForgotPassword(user.FirstName, user.Email, user.Password, user.Email, "", user.LangCode);
                    if (string.IsNullOrWhiteSpace(result) == false)
                    {
                        throw new Exception(result);
                    }
                }
                else
                {
                    throw new Exception("Email không được tìm thấy");
                }

                return Json(new object[] { true, "" });
            }
            catch (Exception e)
            {
                return Json(new object[] { false, e.Message });
            }
        }

        public ActionResult Forbidden(string page = "")
        {
            ViewBag.page = page;
            return View();
        }

        #endregion



        #region dashboard

        public ActionResult Dashboard()
        {
            #region check permission
            var p = EAuthority.CheckPermission("dashboard");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["dashboard_view"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Bảng thông tin" });
            }
            ViewBag.p = p;
            #endregion
            return View();
        }


        #endregion



    }
}