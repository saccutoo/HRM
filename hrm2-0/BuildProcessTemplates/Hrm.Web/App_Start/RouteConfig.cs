using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hrm.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Search",
                url: "Search/{action}/{id}",
                defaults: new { controller = "Search", action = "List", id = UrlParameter.Optional },
                namespaces: new[] { "Hrm.Framework.Controllers" }
            ).DataTokens["UseNamespaceFallback"] = false;
            routes.MapRoute(
                name: "staff-list",
                url: "staff-list",
                defaults: new { controller = "Staff", action = "List", id = UrlParameter.Optional },
                namespaces: new[] { "Hrm.Web.Controllers" }
            );
            routes.MapRoute(
               name: "staff-detail",
               url: "staff-detail",
               defaults: new { controller = "Staff", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "Hrm.Web.Controllers" }
            );
            routes.MapRoute(
               name: "working-calculation-period",
               url: "working-calculation-period",
               defaults: new { controller = "Workingday", action = "WorkingDayCalculationPeriod", id = UrlParameter.Optional },
               namespaces: new[] { "Hrm.Admin.Controllers" }
            );
            routes.MapRoute(
             name: "registration",
             url: "registration",
             defaults: new { controller = "Authentication", action = "Registration" },
             namespaces: new[] { "Hrm.Web.Controllers" }
            ).DataTokens["UseNamespaceFallback"] = false;
            routes.MapRoute(
               name: "login",
               url: "login",
               defaults: new { controller = "Authentication", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "Hrm.Web.Controllers" }
            );
            routes.MapRoute(
               name: "staff-orgchart",
               url: "staff-orgchart",
               defaults: new { controller = "Staff", action = "Orgchart", id = UrlParameter.Optional },
               namespaces: new[] { "Hrm.Web.Controllers" }
            );
            routes.MapRoute(
              name: "staff-onboarding",
              url: "staff-onboarding",
              defaults: new { controller = "Staff", action = "Onboarding", id = UrlParameter.Optional },
              namespaces: new[] { "Hrm.Web.Controllers" }
            );
            routes.MapRoute(
               name: "workingday-detail",
               url: "workingday-detail",
               defaults: new { controller = "Workingday", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "Hrm.Web.Controllers" }
            );
            routes.MapRoute(
              name: "workingday-shift",
              url: "workingday-shift",
              defaults: new { controller = "Workingday", action = "Shift", id = UrlParameter.Optional },
              namespaces: new[] { "Hrm.Admin.Controllers" }
           );
            routes.MapRoute(
            name: "workingday-holiday",
            url: "workingday-holiday",
            defaults: new { controller = "Workingday", action = "Holiday", id = UrlParameter.Optional },
            namespaces: new[] { "Hrm.Admin.Controllers" }
         );
            routes.MapRoute(
              name: "workingday-annualleave",
              url: "workingday-annualleave",
              defaults: new { controller = "Workingday", action = "AnnualLeave", id = UrlParameter.Optional },
              namespaces: new[] { "Hrm.Admin.Controllers" }
           );
            routes.MapRoute(
              name: "general-category",
              url: "general-category",
              defaults: new { controller = "MasterData", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "Hrm.Admin.Controllers" }
           );
            routes.MapRoute(
             name: "salary-element",
             url: "salary-element",
             defaults: new { controller = "SalaryElement", action = "SalaryElement", id = UrlParameter.Optional },
             namespaces: new[] { "Hrm.Admin.Controllers" }
          );
            routes.MapRoute(
            name: "add-salary-type",
            url: "add-salary-type/{id}",
            defaults: new { controller = "SalaryType", action = "AddSalaryType", id = UrlParameter.Optional },
            namespaces: new[] { "Hrm.Admin.Controllers" }
         );
            routes.MapRoute(
             name: "salary-type",
             url: "salary-type",
             defaults: new { controller = "SalaryType", action = "SalaryType", id = UrlParameter.Optional },
             namespaces: new[] { "Hrm.Admin.Controllers" }
          );
            
            routes.MapRoute(
            name: "dashboard-detail",
            url: "dashboard-detail",
            defaults: new { controller = "Dashboard", action = "Detail", id = UrlParameter.Optional },
            namespaces: new[] { "Hrm.Web.Controllers" }
            );
            routes.MapRoute(
                           name: "dashboard",
                           url: "dashboard",
                           defaults: new { controller = "Dashboard", action = "Detail", id = UrlParameter.Optional },
                           namespaces: new[] { "Hrm.Web.Controllers" }
                       );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Hrm.Web.Controllers" }
            ).DataTokens["UseNamespaceFallback"] = false;
        }
    }
}
