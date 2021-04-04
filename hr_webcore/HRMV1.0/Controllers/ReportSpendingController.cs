using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using ERP.Framework.DataBusiness.Common;
using HRM.Common;
using HRM.Constants;
using HRM.DataAccess.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Logger;
using HRM.Security;
using HRM.App_LocalResources;
using Newtonsoft.Json;
using static HRM.Constants.Constant;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class ReportSpendingController : Controller
    {
        // GET: ReportSpending
        public ActionResult AccountNumber()
        {
            ViewBag.url = "/ReportSpending/GetReportSpendingByAccountNumber";
            ViewBag.urlGeneral = "/ReportSpending/GetReportGeneralSpending";
            return View();
        }
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "ReportSpendingByAccountNumber")]
        public ActionResult GetReportSpendingByAccountNumber(int pageIndex, string pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            var db = new ReportSpendingDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
            //if (list.filter7 == "1" && filter.Split('=').Count() > 1)
            //{
            //    filter = filter.Replace(filter.Split('=')[filter.Split('=').Count() - 1], "1");
            //}
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = Int32.Parse(pageSize),
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetReportSpendingByAccountNumber(baseListParam, list, out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId

            }));
        }

        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "ReportIncreaseSpending")]

        public ActionResult ExportReportSpendingByAccountNumber(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            DataTable dt = new DataTable("Grid");
                
            dt.Columns.AddRange(new DataColumn[12]
            {
                new DataColumn(AppRes.CID),
                new DataColumn(AppRes.DateLink),
                new DataColumn(AppRes.StartDateExclude),
                new DataColumn(AppRes.SpendingExclude),
                new DataColumn(AppRes.EndDateExclude),
                new DataColumn(AppRes.BDAUT),
                new DataColumn(AppRes.OrganizationUnitBDAUT),
                new DataColumn(AppRes.MCC),
                new DataColumn(AppRes.Unit),
                new DataColumn(AppRes.Spending),
                new DataColumn(AppRes.SpendingBDAut ),
                new DataColumn(AppRes.Eligible)

            });


            dt.Columns[0].DataType = typeof(double);
            dt.Columns[1].DataType = typeof(DateTime);
            dt.Columns[2].DataType = typeof(DateTime);
            dt.Columns[3].DataType = typeof(double);
            dt.Columns[4].DataType = typeof(DateTime);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(string);
            dt.Columns[7].DataType = typeof(double);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(double);
            dt.Columns[10].DataType = typeof(double);
            dt.Columns[11].DataType = typeof(double);

            var db = new ReportSpendingDAL();
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            list.FromDate = Convert.ToDateTime(DateTime.ParseExact(list.StringFromDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            list.ToDate = Convert.ToDateTime(DateTime.ParseExact(list.StringToDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            var lstData = db.ExportReportSpendingByAccountNumber(baseListParam, list);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                        item.CID == null ? 0 : Double.Parse(item.CID),
                        item.DateLink == null ? new DateTime(1, 1, 1) : item.DateLink,
                        item.StartDateExclude == null ? new DateTime(1, 1, 1) : item.StartDateExclude,
                        item.SpendingExclude == null ? 0 : item.SpendingExclude,
                        item.EndDateExclude == null ? new DateTime(1, 1, 1) : item.EndDateExclude,
                        item.BD == null ? "" : item.BD,
                        item.OrganizationUnit == null ? "" : item.OrganizationUnit,
                        item.MCC == null ? 0 : Double.Parse(item.MCC),
                        item.Unit == null ? "" : item.Unit,
                        item.Spending == null ? 0 : item.Spending,
                        item.SpendingBDAut == null ? 0 : item.SpendingBDAut,
                        item.Eligible == null ? 0 : item.Eligible
                        
                     );


            }
            if(Global.CurrentUser.RoleId == 29)
            {
               dt.Columns.Remove(AppRes.Spending);
            }
            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            var excelName = "ReportSpendingByAccountNumber.xlsx";
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "ReportGeneralSpending")]
        public ActionResult GetReportGeneralSpending(int pageIndex, string pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            var db = new ReportSpendingDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
            //if (list.filter7 == "1" && filter.Split('=').Count() > 1)
            //{
            //    filter = filter.Replace(filter.Split('=')[filter.Split('=').Count() - 1], "1");
            //}
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = Int32.Parse(pageSize),
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetReportGeneralSpending(baseListParam, list, out total, out totalColumns, 0);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId

            }));
        }
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "ReportIncreaseSpending")]

        public ActionResult ExportReportGeneralSpending(int? pageIndex, int? pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            DataTable dt = new DataTable("Grid");

            dt.Columns.AddRange(new DataColumn[14]
            {
                new DataColumn(AppRes.Unit),
                new DataColumn(AppRes.M1),
                new DataColumn(AppRes.M2),
                new DataColumn(AppRes.M3),
                new DataColumn(AppRes.M4),
                new DataColumn(AppRes.M5),
                new DataColumn(AppRes.M6),
                new DataColumn(AppRes.M7),
                new DataColumn(AppRes.M8),
                new DataColumn(AppRes.M9),
                new DataColumn(AppRes.M10 ),
                new DataColumn(AppRes.M11 ),
                new DataColumn(AppRes.M12 ),
                new DataColumn(AppRes.Total )

            });


            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(double);
            dt.Columns[2].DataType = typeof(double);
            dt.Columns[3].DataType = typeof(double);
            dt.Columns[4].DataType = typeof(double);
            dt.Columns[5].DataType = typeof(double);
            dt.Columns[6].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(double);
            dt.Columns[8].DataType = typeof(double);
            dt.Columns[9].DataType = typeof(double);
            dt.Columns[10].DataType = typeof(double);
            dt.Columns[11].DataType = typeof(double);
            dt.Columns[12].DataType = typeof(double);
            dt.Columns[13].DataType = typeof(double);

            var db = new ReportSpendingDAL();
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var lstData = db.GetReportGeneralSpending(baseListParam, list, out total, out totalColumns,1); ;
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                        item.Unit == null ? "" : item.Unit,
                        item.M1 == null ? 0 : item.M1,
                        item.M2 == null ? 0 : item.M2,
                        item.M3 == null ? 0 : item.M3,
                        item.M4 == null ? 0 : item.M4,
                        item.M5 == null ? 0 : item.M5,
                        item.M6 == null ? 0 : item.M6,
                        item.M7 == null ? 0 : item.M7,
                        item.M8 == null ? 0 : item.M8,
                        item.M9 == null ? 0 : item.M9,
                        item.M10 == null ? 0 : item.M10,
                        item.M11 == null ? 0 : item.M11,
                        item.M12 == null ? 0 : item.M12,
                        item.Total == null ? 0 : item.Total

                     );


            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            var excelName = "ReportGeneralSpending.xlsx";
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

    }
}