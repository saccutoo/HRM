using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace HRM.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Xử lý trước khi vào controller thì chạy hàm này
        /// Nhiệm vụ: Kiểm tra đối với từng User thì sẽ được truy cập vào url nào
        /// Nếu không có quyền thì sẽ chuyển về NoAutAuthen
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            System.Web.HttpContext.Current.Session["UserIdKept"] = Global.CurrentUser.UserID;
            
        }
    }
}