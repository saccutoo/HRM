
using System;
using ClosedXML.Excel;
using ERP.DataAccess.DAL;
using ERP.Framework.DataBusiness.Common;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Models;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Web.Mvc;
using HRM.Security;
using HRM.Utils;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using static HRM.Constants.Constant;
using HRM.Logger;
using Core.Web.Enums;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class EmployeeAllowanceController : Controller
    {
        // GET: EmployeeAllowance
       [Permission(TableID = (int)ETable.EmployeeAllowance, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
           
            return PartialView();
        }


        [Permission(TableID = (int)ETable.EmployeeAllowance, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeAllowance_List")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new EmployeeAllowanceDAL();
            int total = 0;
            var LanguageID = Global.CurrentLanguage;
            var RoleId = Global.CurrentUser.RoleId;
            var UserID = Global.CurrentUser.UserID;
            int wpID = 0;
            var DeptID = Global.CurrentUser.OrganizationUnitID;
            var result = db.GetEmployeeAllowance(pageIndex, pageSize, filter, out total, LanguageID, RoleId, UserID, DeptID,wpID);
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
    }
}