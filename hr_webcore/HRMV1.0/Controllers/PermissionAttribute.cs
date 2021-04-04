using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static HRM.Constants.Constant;

namespace HRM.Controllers
{
    public class PermissionAttribute : AuthorizeAttribute
    {

        public int TableID { set; get; }
        public int TypeAction { set; get; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Sys_Table_Role_ActionDal db = new Sys_Table_Role_ActionDal();
            int userRole = Global.CurrentUser.RoleId;
            int userId = Global.CurrentUser.UserID;
            Sys_Table_Role_Action sysAc =
                db.GetAll_Sys_Table_Role_ActionToCache()
                .Find(x => x.TableId == this.TableID && (x.RoleId == userRole || x.UserId == userId));
            if (sysAc != null)
            {
                switch (TypeAction)
                {
                    case (int)EAction.Index:
                        return sysAc.isIndex;

                    case (int)EAction.Get:
                        return sysAc.isGet;

                    case (int)EAction.Add:
                        return sysAc.isAdd;

                    case (int)EAction.Edit:
                        return sysAc.isEdit;

                    case (int)EAction.Delete:
                        return sysAc.isDelete;
                    case (int)EAction.Submit:
                        return sysAc.isSubmit??false;

                    case (int)EAction.Excel:
                        return sysAc.isExcel;

                    case (int)EAction.Approval:
                        return sysAc.isApproval ?? false;

                    case (int)EAction.DisApproval:
                        return sysAc.isDisApproval ?? false;
                         
                    case (int)EAction.Copy:
                        return sysAc.isCopy ?? false; ;

                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //var TempData = filterContext.Controller.TempData;
            //TempData["Messages"] = "Bạn không có quyền truy cập mục này";
            filterContext.Result = new RedirectToRouteResult(
                                   new RouteValueDictionary
                                   {
                                       { "action", "Error_401" },
                                       { "controller", "Authentication" }
                                   });
        }
    }
}