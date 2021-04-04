using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web;
using HRM.DataAccess.Entity;
using Newtonsoft.Json;
using HRM.Common;
using System.Diagnostics;

namespace HRM.Helper
{
    public class Security
    {
        public static Sec_UserLogin GetInforFormCookie(string name, string key)
        {
            Sec_UserLogin ses = new Sec_UserLogin();
            try
            {
                string token = HttpContext.Current.Request.Cookies[name].Value;
                if (token != null)
                {

                    string infor = DisplayUtils.Decrypt(token,key);
                    var rs = JsonConvert.DeserializeObject<Sec_UserLogin>(infor);
                    if (rs != null)
                    {
                        ses.UserID = rs.UserID;
                        ses.UserName = rs.UserName;
                        ses.RoleId = rs.RoleId;
                        ses.DisplayName = rs.DisplayName;
                        return ses;
                    }
                }
                return ses;
            }catch(Exception e)
            {
                Trace.WriteLine(e.Message);
                return ses;
            }
            

        }
        public string SetAuthCookie(HttpContext httpContext, string authenticationTicket, string cookieName, string key, DateTime Expiration)
        {
            var encryptedTicket = DisplayUtils.Encrypt(authenticationTicket, key);
            var cookie = new HttpCookie(cookieName, encryptedTicket)
            {
                HttpOnly = true,
                Expires = Expiration
            };
            httpContext.Response.Cookies.Add(cookie);
            return encryptedTicket;
        }
        public void UserSignIn(Sec_UserLogin accountInfo, HttpContext curentHttpContext)
        {
            var token = SetAuthCookie(curentHttpContext, JsonConvert.SerializeObject(accountInfo), "_NVOAUTH", "keyauthen", DateTime.Now.AddDays(15));
        }
        public void Logout(HttpContext httpContext)
        {
            var cookie = new HttpCookie("_NVOAUTH");
            DateTime nowDateTime = DateTime.Now;
            cookie.Expires = nowDateTime.AddDays(-1);
            httpContext.Response.Cookies.Add(cookie);

            httpContext.Request.Cookies.Remove("_NVOAUTH");
            FormsAuthentication.SignOut();
        }
    }
}