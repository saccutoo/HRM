using ERP.Framework.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using HRM.Models;
using HRM.Common;
using StackExchange.Profiling;
using System.Data.SqlClient;
using ERP.Framework.Configuration;

namespace HRMV1._0
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SqlDependency.Start(Config.GetConfigByKey("ErpCacheDependency"));
        }
        protected void Application_authenticateRequest(object sender, EventArgs e)
        {

            try
            {
                var authCookie = HttpContext.Current.Request.Cookies["_NVOAUTH"];
                string encTicket = authCookie?.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    var encryptedTicket = DisplayUtils.Decrypt(encTicket, "keyauthen");
                    var loginToken = new FormsAuthenticationTicket(1, "_NVOAUTH", DateTime.Now, authCookie.Expires,
                true, encryptedTicket);
                    var acc = new UserIdentity(loginToken);
                    HttpContext.Current.User = acc;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        protected void Application_BeginRequest()
        {

            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }

            var culture = "vi-VN";
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[Constant.APP_CURRENT_LANG];
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
                }
                else
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
                }
               // System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            }
            catch (Exception exception)
            {
                FormsAuthentication.SignOut();
            }

        }
    }
}
