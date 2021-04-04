using Hrm.Core.Infrastructure;
using Hrm.Framework.Themes;
using Hrm.Framework.Context;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Hrm.Common.Helpers;
using Hrm.Common;
using System.IO;

namespace Hrm.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //except the themeable razor view engine we use
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());
            //initialize engine context
            EngineContext.Initialize(false);
        }
        protected void Application_BeginRequest()
        {
            try
            {
                CurrentContext.WebBaseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                if (!CurrentContext.WebBaseUrl.EndsWith("/"))
                {
                    CurrentContext.WebBaseUrl += "/";
                }
                if (CurrentContext.WebBaseUrl.ToLower().Contains("onpeople"))
                {
                    CurrentContext.WebBaseUrl = CurrentContext.WebBaseUrl.Replace(":" + HttpContext.Current.Request.Url.Port, string.Empty);
                }
                if (CurrentContext.WebBaseUrl.ToLower() != Config.GetConfig("UrlScheme") + Config.GetConfig(Constant.WebBaseUrl) + "/")
                {
                    var baseUrl = Config.GetConfig(Constant.WebBaseUrl);

                    var currentRequest = CurrentContext.WebBaseUrl.Replace(Config.GetConfig("UrlScheme"), string.Empty).Replace(Config.GetConfig(Constant.WebBaseUrl) + "/", Config.GetConfig(Constant.WebBaseUrl));
                    if (!string.IsNullOrEmpty(currentRequest) && currentRequest.IndexOf(baseUrl) > -1)
                    {
                        var currentUrl = currentRequest.Replace(baseUrl, string.Empty);
                        if (!string.IsNullOrEmpty(currentUrl) && currentUrl.IndexOf(".") > -1)
                        {
                            CurrentContext.DbName = currentUrl.Split('.')[0];
                        }
                    }
                    if (!string.IsNullOrEmpty(CurrentContext.DbName))
                    {
                        CurrentUser.DbName = CurrentContext.DbName;
                    }
                    if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("/registration") || HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("/login"))
                    {
                        Response.RedirectToRoute("dashboard-detail");
                    }
                }
                else
                {
                    CurrentContext.DbName = Constant.SystemDbName;
                    CurrentUser.DbName = Constant.SystemDbName;
                }
            }
            catch (Exception)
            {
                FormsAuthentication.SignOut();
            }

        }
    }
}
