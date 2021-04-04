using System.Web.Mvc;
using System.Web.Routing;

namespace Hrm.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin",
                "admin",
                new { controller = "Setting", action = "Index", area = "Admin", id = UrlParameter.Optional },
                new[] { "Hrm.Admin.Controllers" }
            ).DataTokens["UseNamespaceFallback"] = false;
            context.MapRoute(
                "Admin-setting",
                "admin-setting",
                new { controller = "Setting", action = "Index", area = "Admin", id = UrlParameter.Optional },
                new[] { "Hrm.Admin.Controllers" }
            ).DataTokens["UseNamespaceFallback"] = false;
            context.MapRoute(
                "Admin_checklist",
                "Admin/setting-checklist",
                new { controller = "Checklist", action = "Index", area = "Admin", id = UrlParameter.Optional },
                new[] { "Hrm.Admin.Controllers" }
            ).DataTokens["UseNamespaceFallback"] = false;

            Route route = context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Setting", action = "Index", area = "Admin", id = UrlParameter.Optional },
                new[] { "Hrm.Admin.Controllers" }
            );
            route.DataTokens["UseNamespaceFallback"] = false;
        }
    }
}
