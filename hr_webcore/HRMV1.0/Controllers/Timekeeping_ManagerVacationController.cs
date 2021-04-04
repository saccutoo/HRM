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

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class Timekeeping_ManagerVacationController : Controller
    {
        // GET: Furlough
        [Permission(TableID = (int)ETable.Timekeeping_ManagerVacation, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Timekeeping_ManagerVacation, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "HR_Furlough_GetManager")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, int month, int year, int userid, int status, string filter = "")
        {
            var db = new Timekeeping_ManagerVacationDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
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
            var result = db.GetWorkingDaySupplementManager(baseListParam, month, year, out total);

            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal,
                userid = baseListParam.UserId
            }));
        }
        public ActionResult Timekeeping_ManagerVacationExportExcel(int month, int year, string filter)
        {
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = 1,
                PageSize = 10000,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[25]
            {
                new DataColumn(AppRes.Timekeeping_CreateName),
                new DataColumn(AppRes.Timekeeping_DepartmentName),
                new DataColumn(AppRes.HR_FurloughYear),
                new DataColumn(AppRes.FurloughSeniority),
                new DataColumn(AppRes.HR_FurloughMore),
                new DataColumn(AppRes.HR_FurloughMoreNote),
                new DataColumn(AppRes.ToTalFurloughYear),
                new DataColumn(AppRes.FurloughUse),
                new DataColumn(AppRes.HR_FurloughOtherUse),
                new DataColumn(AppRes.HR_FurloughOtherUseNote),
                new DataColumn(AppRes.FurloughLastYear),
                new DataColumn(AppRes.FurloughLastYearUse),
                new DataColumn(AppRes.TotalFurloughYearRemaining),
                new DataColumn(AppRes.Month1),
                new DataColumn(AppRes.Month2),
                new DataColumn(AppRes.Month3),
                new DataColumn(AppRes.Month4),
                new DataColumn(AppRes.Month5),
                new DataColumn(AppRes.Month6),
                new DataColumn(AppRes.Month7),
                new DataColumn(AppRes.Month8),
                new DataColumn(AppRes.Month9),
                new DataColumn(AppRes.Month10),
                new DataColumn(AppRes.Month11),
                new DataColumn(AppRes.Month12),

            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(double);
            dt.Columns[3].DataType = typeof(double);
            dt.Columns[4].DataType = typeof(double);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(double);
            dt.Columns[8].DataType = typeof(double);
            dt.Columns[9].DataType = typeof(string);
            dt.Columns[10].DataType = typeof(double);
            dt.Columns[11].DataType = typeof(double);
            dt.Columns[12].DataType = typeof(double);
            dt.Columns[13].DataType = typeof(double);
            dt.Columns[14].DataType = typeof(double);
            dt.Columns[15].DataType = typeof(double);
            dt.Columns[16].DataType = typeof(double);
            dt.Columns[17].DataType = typeof(double);
            dt.Columns[18].DataType = typeof(double);
            dt.Columns[19].DataType = typeof(double);
            dt.Columns[20].DataType = typeof(double);
            dt.Columns[21].DataType = typeof(double);
            dt.Columns[22].DataType = typeof(double);
            dt.Columns[23].DataType = typeof(double);
            dt.Columns[24].DataType = typeof(double);



            var db = new Timekeeping_ManagerVacationDAL();
            int? total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.GetWorkingDaySupplementManager(baseListParam, month, year, out total);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.Fullname, item.OrganizationUnitName, item.FurloughYear, item.FurloughSeniority, item.FurloughMore, item.FurloughMoreNote,item.ToTalFurloughYear,item.FurloughUse,item.FurloughOtherUse,item.FurloughOtherUseNote,item.FurloughLastYear,item.FurloughLastYearUse,item.TotalFurloughYearRemaining,item.T1,item.T2,item.T3,item.T4,item.T5,item.T6,item.T7,item.T8,item.T9,item.T10,item.T11,item.T12);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Timekeeping_ManagerVacation.xlsx");
        }

    }
}