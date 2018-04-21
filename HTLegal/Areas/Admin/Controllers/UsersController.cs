using HTLegal.Models;
using HTLegal.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTLegal.Areas.admin.Controllers
{
    public class UsersController : Controller
    {
        // GET: admin/Users

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">0: them moi/ >0: update</param>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {

            #region check permission
            var p = EAuthority.CheckPermission("user");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if(p["user_view"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Thành viên" });
            }
            ViewBag.p = p;
            #endregion

            ViewBag.id = id;
            E_Users curMem = EAuthority.GetCurrentMember();
            HTLegalContext db = new HTLegalContext();

            E_Users user = new E_Users { IsActive = true};
            if (id > 0)
            {
                var userExist = db.E_Users.Find(id);
                if (userExist == null)
                {
                    TempData["err"] = "Không tìm thấy thành viên có id " + id;
                }
                else
                {
                    user = userExist;
                }
            }
            var mem_role = db.E_Roles.Find(curMem.RolesId);
            var roles = db.E_Roles.ToList();
            ViewBag.roles = new SelectList(roles, "Id", "Name", user.RolesId);
            ViewBag.usersList = db.E_Users.OrderByDescending(u => u.Id).ToList();
            return View(user);
        }


        [HttpPost]
        public ActionResult SaveChanges(E_Users um)
        {
            try
            {
                HTLegalContext db = new HTLegalContext();

                if (ModelState.IsValid)
                {
                    if (um.Id >0)
                    {
                        //update
                        //check email
                        if (db.E_Users.Any(us => us.Email.Equals(um.Email) == true && us.Id != um.Id))
                        {
                            throw new Exception("Email này đã tồn tại");
                        }


                        E_Users u = db.E_Users.Find(um.Id);
                        u.FirstName = um.FirstName;
                        u.LastName = um.LastName;
                        u.FullName = um.FirstName + " " + um.LastName;
                        u.Address = um.Address;
                        u.Gender = um.Gender;
                        u.CellPhone = um.CellPhone;
                        u.Email = um.Email;
                        u.Birthday = ECommon.ConvertDateViToDateEn(Request["Birthday"]);
                        if (u.IsActive != um.IsActive)
                        {
                            if (um.IsActive == false)
                            {
                                //banned
                                E_Users curMem = EAuthority.GetCurrentMember();
                                u.BannedAt = DateTime.Now;
                                u.BannedBy = curMem.FullName;
                            }
                            else
                            {
                                u.BannedBy = string.Empty;
                                u.BannedAt = null;
                            }
                            u.IsActive = um.IsActive;
                        }

                        E_Roles r = db.E_Roles.Find(um.RolesId);
                        u.RolesId = um.RolesId;
                        u.RolesName = r.Name;

                        db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        TempData["succ"] = "Cập nhật thành công";
                        return RedirectToAction("index");

                    }
                    else
                    {
                        //save new
                        if (db.E_Users.Any(us => us.Email.Equals(um.Email) == true))
                        {
                            throw new Exception("Email này đã tồn tại");
                        }

                        E_Users u = new E_Users();
                        u.FirstName = um.FirstName;
                        u.LastName = um.LastName;
                        u.FullName = um.FirstName + " " + um.LastName;
                        u.Password = ECommon.RemoveUnicodeStringAndSymbol(u.FirstName).Replace(" ","") + DateTime.Now.ToString("ffffff");
                        u.Address = um.Address;
                        u.Gender = um.Gender;
                        u.Email = um.Email;
                        u.CellPhone = um.CellPhone;
                        u.Birthday = ECommon.ConvertDateViToDateEn(Request["Birthday"]);
                        u.IsActive = um.IsActive;

                        E_Roles r = db.E_Roles.Find(um.RolesId);
                        u.RolesId = um.RolesId;
                        u.RolesName = r.Name;

                        db.E_Users.Add(u);
                        db.SaveChanges();

                        TempData["succ"] = "Thành viên mới đã được lưu";
                        return RedirectToAction("index",new { id = 0 });

                    }

                }
                else
                {
                    throw new Exception("Các thông tin quan trọng(*) cần được cung cấp");
                }
                
            }
            catch (Exception e)
            {
                TempData["err"] = e.Message;
                return RedirectToAction("index", new { id = um.Id });
            }



        }

        public ActionResult Delete(int id)
        {
            #region check permission
            var p = EAuthority.CheckPermission("user");
            if (p == null)
            {
                return RedirectToAction("index", "home");
            }
            else if (p["user_delete"] == false)
            {
                return RedirectToAction("forbidden", "home", new { page = "Thành viên" });
            }
            ViewBag.p = p;
            #endregion
            try
            {
                HTLegalContext db = new HTLegalContext();
                E_Users user = db.E_Users.Find(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thành viên này");
                }

                if (user.E_Attorneys != null)
                {
                    db.E_Attorneys.Remove(user.E_Attorneys);
                }
                db.E_Users.Remove(user);
                db.SaveChanges();
                TempData["succ"] = "Xóa thành công";
            }
            catch (Exception e)
            {
                TempData["err"] = e.Message;
            }

            return RedirectToAction("index");

        }



    }
}