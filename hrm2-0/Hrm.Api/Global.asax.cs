using Hrm.Common;
using Hrm.Core.Infrastructure;
using Hrm.Framework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hrm.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            EngineContext.Initialize(false);
        }
        protected void Application_BeginRequest()
        {
            CurrentContext.WebBaseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            CurrentUser.DbName = "System";
        }
    }
}
