using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Models;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using System.Web.Security;

namespace HRM.Security
{
    public class HRMAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpContext.Current.Session["UserId"] = Global.CurrentUser.UserID;
            System.Web.HttpContext.Current.Session["UserIdKept"] = Global.CurrentUser.UserID;
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (filterContext.ActionDescriptor.ActionName != "ForceChangePassword")
                {
                    var secUserDal = new Sec_UserDal();
                    var acc = new Sec_UserLogin();
                    secUserDal.DoLoginAll(Global.CurrentUser.UserName, out acc);

                    if (acc.NeedChangePassword)
                    {
                        filterContext.Result = new RedirectToRouteResult(new
                            System.Web.Routing.RouteValueDictionary(new { controller = "Authentication", action = "ForceChangePassword" }));
                    }
                }
                return;
            }
            else
            {
                var rawUrl = HttpContext.Current.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(new
                        System.Web.Routing.RouteValueDictionary(new { controller = "Authentication", action = "Login", RedirectUrl = rawUrl }));

            }
        }
    }
}