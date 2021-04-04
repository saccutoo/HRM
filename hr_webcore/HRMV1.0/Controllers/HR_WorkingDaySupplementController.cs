using ClosedXML.Excel;
using ERP.DataAccess.DAL;
using ERP.Framework.DataBusiness.Common;
using ERP.Framework.WebExtensions.Grid;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Security;
using static HRM.Constants.Constant;
using HRM.Logger;
using Core.Web.Enums;
using HRM.DataAccess.Entity.UserDefinedType;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class HR_WorkingDaySupplementController : Controller
    {
        //// GET: HR_WorkingDaySupplement
        [Permission(TableID = (int)ETable.HR_WorkingDaySupplement, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {

            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_WorkingDaySupplement, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetHR_WorkingDaySupplement")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, int month, int year, int userid, int status, string filter = "", int bosungcong = 0)
        {
            var db = new HR_WorkingDaySupplementDal();
            int? total = 0;
            int quyen = 0; //kiểm tra trang thái được phép duyệt công của tài khoản đăng nhập
            quyen = db.HR_GetStatusForCheckApproval(Global.CurrentUser.LoginUserId);
            if (userid == 0)
            {
                userid = Global.CurrentUser.LoginUserId;
            }
            if(bosungcong == 1)
            {
                filter += " and a.StaffID = " + userid + " ";
            }
            else if (filter.Contains(" AND a.Status = 0 ") == false)
            {
                if (status == 0)
                {
                    if (quyen == 252)
                    {
                        filter += " and a.Status in (1,6) ";
                    }
                    else
                    {
                        filter += " and a.Status = " + quyen + " ";
                    }
                }
            }
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = userid,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetHR_WorkingDaySupplement(baseListParam, out total);
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal
            }));
        }

        public ActionResult DuyetCong()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_WorkingDaySupplement, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "HR_WorkingDaySupplement_GetByAutoId")]
        public ActionResult HR_WorkingDaySupplement_GetListId(int autoid)
        {
            var db = new HR_WorkingDaySupplementDal();
            var baseListParam = new BaseListParam()
            {
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.HR_WorkingDaySupplement_GetListId(baseListParam, autoid);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDaySupplement, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "HR_WorkingDaySupplement_DeleteByAutoId")]
        public ActionResult HR_WorkingDaySupplement_DeleteByAutoId(int autoid)
        {
            var db = new HR_WorkingDaySupplementDal();
            var result = db.HR_WorkingDaySupplement_DeleteByAutoId(autoid);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDaySupplement, TypeAction = (int)EAction.Approval)]
        [WriteLog(Action = EAction.Approval, LogStoreProcedure = "HR_WorkingDaySupplement_Approval")]
        //public ActionResult HR_WorkingDaySupplement_Approval(int AutoID, string Note, int Type)
        //{
        //    var db = new HR_WorkingDaySupplementDal();
        //    var baseListParam = new BaseListParam()
        //    {
        //        UserId = Global.CurrentUser.UserID
        //    };
        //    var result = db.HR_WorkingDaySupplement_Approval(baseListParam, AutoID, Note, Type);
        //    return Content(JsonConvert.SerializeObject(new
        //    {
        //        result
        //    }));
        //}
        public ActionResult HR_WorkingDaySupplement_Approval(List<HR_WorkingDaySupplementType> data)
        {
            var db = new HR_WorkingDaySupplementDal();
            var baseListParam = new BaseListParam()
            {
                UserId = Global.CurrentUser.UserID
            };
            var result = db.HR_WorkingDaySupplement_Approval(baseListParam, data);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}