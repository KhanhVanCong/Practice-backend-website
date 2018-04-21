using HTLegal.Models;
using HTLegal.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLegal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HTLegalContext db = new HTLegalContext();
            var homeInfo = db.E_Home_Content.ToList();
            var homeSlide = db.E_Home_Slide.ToList();
            var services = db.E_Services.Where( s => s.IsActiveHome == true).OrderByDescending(s => s.Id).ToList();
            var featureServices = db.E_Feature_Services.ToList();
            //var client = db.E_Client.ToList();
            var attorney = db.E_Attorneys.Where(a => a.IsActiceHome == true).ToList();
            var blog = db.E_Blog.Where(b => b.IsActive == true).OrderByDescending(b => b.CreatedAt).ToList();
            var feedBack = db.E_Home_FeedbackClient.OrderBy(f => f.Order).ToList();

            List<object> home = new List<object>();
            home.Add(homeInfo);
            home.Add(homeSlide);
            home.Add(services);
            home.Add(featureServices);
            //home.Add(client);
            home.Add(attorney);
            home.Add(blog);
            home.Add(feedBack);
            return View(home);
        }

        public ActionResult Submit()
        {
            try
            {
                var firstName = Request["FirstName"];
                var lastName = Request["LastName"];
                var email = Request["Email"];
                var phone = Request["Phone"];
                var message = Request["Message"];

                var subject = firstName + " " + lastName + " vừa mới gửi cho bạn một thông điệp !";
                var body = "<b>Họ tên: </b>" + firstName + " " + lastName + "<br/>" +
                           "<b>Email: </b>" + email + "<br/>" + "<b>Phone: </b>" + phone + "<br/>" + "<b>Các vấn đề: </b>" + "<br/>" +
                           message;
                HTLegalContext db = new HTLegalContext();
                SendEmail.Send(db.E_WebsiteConfiguration.FirstOrDefault().EmailAdmin, subject, body);
                TempData["success"] = "success";          
            }
            catch (Exception e)
            {
                TempData["error"] = " Lỗi ! " + e.Message;
            }
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        #region Login, Logout
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
                return RedirectToAction("index");
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
            return RedirectToAction("index");
        }
        #endregion
    }
}