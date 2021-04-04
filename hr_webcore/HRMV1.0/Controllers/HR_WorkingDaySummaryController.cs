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

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class HR_WorkingDaySummaryController : Controller
    {
        // GET: HR_WorkingDaySummary
        [Permission(TableID = (int)ETable.HR_WorkingDaySummary, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            //ViewBag.url = "/HR_WorkingDaySummary/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_WorkingDaySummary, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetHR_WorkingDaySummary")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, int month, int year, int userid, int status, string filter = "")
        {
            var db = new HR_WorkingDaySummaryDal();
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetHR_WorkingDaySummary(baseListParam, out totalColumns,month,year);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                lstTotal = totalColumns
            }));
        }
        [Permission(TableID = (int)ETable.HR_WorkingDaySummary, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "WorkingDayTemporary_Get")]
        public ActionResult GetWorkingDayTemporary(int month, int year, int userid, string filter = "")
        {
            var db = new HR_WorkingDaySummaryDal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetWorkingDayTemporary(baseListParam, month, year);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
            }));
        }
    }
}