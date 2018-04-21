using HTLegal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTLegal.ViewController
{
    public class EAuthority
    {



        /// <summary>
        /// get login member
        /// </summary>
        /// <returns></returns>
        public static E_Users GetCurrentMember()
        {
            E_Users mem = HttpContext.Current.Session["member"] as E_Users;
            return mem;
        }


        /// <summary>
        /// Member authentication
        /// </summary>
        /// <param name="pageCode"></param>
        /// <returns>result -1: het session/ dictionary<functioncode, access></returns>
        public static Dictionary<string,bool> CheckPermission(string pageCode)
        {
            var session = HttpContext.Current.Session;
            E_Users member = session["member"] as E_Users;
            HTLegalContext db = new HTLegalContext();

            if (member == null)
            {
                if (HttpContext.Current.Request.Cookies["htlegal_email"] != null && HttpContext.Current.Request.Cookies["htlegal_pass"] != null)
                {
                    string email = HttpContext.Current.Request.Cookies["htlegal_email"].Value;
                    string pass = HttpContext.Current.Request.Cookies["htlegal_pass"].Value;
                    E_Users reloginMem = db.E_Users.Where(u => u.Email.Equals(email) == true && u.Password.Equals(pass) == true && u.IsActive == true).FirstOrDefault();
                    if (reloginMem != null)
                    {
                        session["member"] = reloginMem;
                        member = reloginMem;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            var accessRoles = (from a in db.E_AccessFunctionRole
                              where a.E_AccessFunctionInPage.PageCode.Equals(pageCode) && a.RoleId == member.RolesId
                              select a).ToList();

            Dictionary<string, bool> dicAccess = new Dictionary<string, bool>();
            foreach (var item in accessRoles)
            {
                dicAccess.Add(item.FunctionCode, item.Access??false);
            }
            return dicAccess;

        }
        
    }


}