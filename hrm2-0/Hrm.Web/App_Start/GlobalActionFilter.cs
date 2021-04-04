using System.Web;
using System.Web.Optimization;
using System.Web.Mvc;
using System;
using System.Web.Routing;

namespace Hrm.Web
{
    public class GlobalActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}
