using HTLegal.Models;
using HTLegal.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLegal.Controllers
{
    public class ContactUsController : Controller
    {
        //
        // GET: /ContactUs/

        public ActionResult Index()
        {
            HTLegalContext db = new HTLegalContext();
            var contact = db.E_CMS_ConTactUs.FirstOrDefault();
            return View(contact);
        }

        public ActionResult Submit()
        {
            try
            {
                var emailStaff = Request["email"];
                var fullName = Request["name"];
                var email = Request["emailclient"];
                var phone = Request["phone"];
                var message = Request["message"];

                var subject = fullName + " vừa mới gửi cho bạn một thông điệp !";
                var body = "<b>Họ tên: </b>" + fullName + "<br/>" +
                           "<b>Email: </b>" + email + "<br/>" + "<b>Phone: </b>" + phone + "<br/>" + "<b>Các vấn đề: </b>" + "<br/>" +
                           message;
                HTLegalContext db = new HTLegalContext();
                SendEmail.Send(emailStaff, subject, body, db.E_WebsiteConfiguration.FirstOrDefault().EmailAdmin);
                TempData["success"] = "success";
            }
            catch (Exception e)
            {
                TempData["error"] = " Lỗi ! " + e.Message;
            }
            return View();

        }

    }
}
