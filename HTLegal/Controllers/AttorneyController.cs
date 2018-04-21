using HTLegal.Models;
using HTLegal.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLegal.Controllers
{
    public class AttorneyController : Controller
    {
        //
        // GET: /Attorney/

        public ActionResult Index()
        {
            HTLegalContext db = new HTLegalContext();
            var attorney = db.E_Attorneys.ToList();
            return View(attorney);
        }

        public ActionResult Detail(int id)
        {
            HTLegalContext db = new HTLegalContext();
            var detail = db.E_Attorneys.Find(id);
            return View(detail);
        }

        public ActionResult Submit()
        {
            try
            {
                var emailAttorney = Request["E_Users.Email"];
                var fullName = Request["FullName"];
                var email = Request["EmailClient"];
                var phone = Request["Phone"];
                var message = Request["Message"];

                var subject = fullName + " vừa mới gửi cho bạn một thông điệp !";
                var body = "<b>Họ tên: </b>" + fullName + "<br/>" +
                           "<b>Email: </b>" + email + "<br/>" + "<b>Phone: </b>" + phone + "<br/>" + "<b>Các vấn đề: </b>" + "<br/>" +
                           message;
                HTLegalContext db = new HTLegalContext();
                SendEmail.Send(emailAttorney, subject, body, db.E_WebsiteConfiguration.FirstOrDefault().EmailAdmin);
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
