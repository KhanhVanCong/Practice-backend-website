using HTLegal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLegal.Controllers
{
    public class ServicesController : Controller
    {
        //
        // GET: /Services/

        public ActionResult Index()
        {
            HTLegalContext db = new HTLegalContext();
            var services = db.E_Services.ToList();
            return View(services);
        }

        public ActionResult Detail(int? id)
        {
            HTLegalContext db = new HTLegalContext();
            var detail = db.E_Services.Find(id);
            return View(detail);
        }

    }
}
