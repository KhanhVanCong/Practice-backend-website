using HTLegal.Models;
using HTLegal.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLegal.Areas.Admin.Controllers
{
    public class AccessManagementController : Controller
    {
        //
        // GET: /Admin/AccessManagement/

        public ActionResult Index(int? id, int? showAddRole)
        {

      
            #region check permission
            var p = EAuthority.CheckPermission("system");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                E_Users currmem = EAuthority.GetCurrentMember();
                ViewBag.CurrRole = id = currmem.RolesId;
                if (id != 2 && p["system_view"] == false)
                {
                    return RedirectToAction("forbidden", "home", new { page = "Quyền truy cập" });
                }
            }
            
            ViewBag.p = p;
            #endregion

            HTLegalContext db = new HTLegalContext();
            List<E_Roles> rolesList = db.E_Roles.ToList();
            ViewBag.rolesList = rolesList;
            ViewBag.showAddRole = showAddRole;
            if (id == null)
            {
                E_Roles rolesInfo = rolesList.FirstOrDefault();
                ViewBag.rolesInfo = rolesInfo;
            }
            else
            {
                E_Roles rolesInfo = rolesList.Where(r => r.Id == id).FirstOrDefault();
                ViewBag.rolesInfo = rolesInfo;
            }
            List<E_AccessPage> accessPages = db.E_AccessPage.OrderBy(a => a.Order).ToList();
            ViewBag.accessPages = accessPages;
            return View();
        }


        [HttpPost]
        public ActionResult Save()
        {
            int roleId = int.Parse(Request["hd_role_id"]);
            string roleName = Request["hd_role_name"];
            try
            {
                HTLegalContext db = new HTLegalContext();
                var accessFuncs = db.E_AccessFunctionInPage.OrderBy(f => f.PageCode).ToList();
                foreach (var item in accessFuncs)
                {
                    var accessReq = Request["chk-func-" + item.FunctionCode];
                    bool access = false;
                    bool.TryParse(accessReq, out access);

                    E_AccessFunctionRole funcRole = db.E_AccessFunctionRole.Where(fr => fr.FunctionCode.Equals(item.FunctionCode) == true && fr.RoleId == roleId).FirstOrDefault();
                    if (funcRole == null)
                    {
                        funcRole = new E_AccessFunctionRole { Access = access, FunctionCode = item.FunctionCode, RoleId = roleId };
                        db.E_AccessFunctionRole.Add(funcRole);
                    }
                    else
                    {
                        funcRole.Access = access;
                        db.Entry(funcRole).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();

                }

                TempData["success"] = "Save successfully";
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction("index", new { id = roleId });
        }






        /// <summary>
        /// ajax call
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeRoles()
        {
            #region check permission
            var p = EAuthority.CheckPermission("system");
            E_Users currmem = EAuthority.GetCurrentMember();
            ViewBag.CurrRole = currmem.RolesId;
            ViewBag.p = p;
            #endregion
            try
            {
                int roleId = int.Parse(Request["RoleId"]);
                HTLegalContext db = new HTLegalContext();
                var rolesInfo = db.E_Roles.Find(roleId);
                if (rolesInfo != null)
                {
                    ViewBag.rolesInfo = rolesInfo;                    
                    List<E_AccessPage> accessPages = db.E_AccessPage.OrderBy(a => a.Order).ToList();
                    ViewBag.accessPages = accessPages;

                    return PartialView("_RolesAccessDetail");

                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }

        //Create New Role
        [HttpPost]
        public ActionResult CreateRole()
        {
            try
            {
                HTLegalContext db = new HTLegalContext();
                var roleName = Request["rolesName"];
                var newRole = new E_Roles();
                newRole.Name = roleName;
                db.E_Roles.Add(newRole);
                db.SaveChanges();
                TempData["success"] = "Tạo mới nhóm quyền thành công ! Vui lòng kiêm tra Roles Access để phân quyền.";
            }
            catch (Exception e)
            {

                TempData["error"] = "Lỗi ! " + e.Message;
            }
            
            return RedirectToAction("index");
        }


    }
}
