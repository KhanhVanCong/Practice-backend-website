using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTLegal.Models;

namespace HTLegal.Controllers
{
    public class AboutUsController : Controller
    {
        //
        // GET: /AboutUs/

        public ActionResult Index()
        {
            HTLegalContext db = new HTLegalContext();
            var aboutUs = db.E_CMS_AboutUs.ToList();
            return View(aboutUs);
        }

    }
}
