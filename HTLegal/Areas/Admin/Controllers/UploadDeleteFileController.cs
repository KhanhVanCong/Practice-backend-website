using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLegal.Areas.Admin.Controllers
{
    public class UploadDeleteFileController : Controller
    {
        //
        // GET: /Admin/UploadDeleteFile/

        public string UploadPicture(string filename, string empid, string spath)
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

    }
}
