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
using HRM.DataAccess.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using Newtonsoft.Json;
using HRM.App_LocalResources;
using HRM.Security;
using HRM.Logger;
using static HRM.Constants.Constant;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class ReportIncreaseSpendingController : Controller
    {
        // GET: ReportIncreaseSpending
        public ActionResult Index()
        {
            ViewBag.url = "/ReportIncreaseSpending/TableServerSideGetData";
            return View(); 
        }
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "ReportIncreaseSpending")]

        public ActionResult TableServerSideGetData(int pageIndex, string pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            var db = new ReportIncreaseSpendingDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
            if (list.filter7 == "1" && filter.Split('=').Count()>1)
            {
                filter = filter.Replace(filter.Split('=')[filter.Split('=').Count() - 1], "1");
            }
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
            var result = db.GetReportIncreaseSpending(baseListParam, list, out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId
                
            }));
        }
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "ReportIncreaseSpending")]

        public ActionResult ExportExcel(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            DataTable dt = new DataTable("Grid");
            if (list.filter7 == "2")
            {
                dt.Columns.AddRange(new DataColumn[8]
                {
                    new DataColumn(AppRes.DeptName),
                    new DataColumn(AppRes.Staff),
                    new DataColumn(AppRes.AssignedSpending),
                    new DataColumn(AppRes.SpendingInMonth),
                    new DataColumn(AppRes.IncreaseSpending ),
                    new DataColumn(AppRes.SpendingIncreasedStandard),
                    new DataColumn(AppRes.CompletionRate),
                    new DataColumn(AppRes.StaffStatus)


                });
                dt.Columns[0].DataType = typeof(string);
                dt.Columns[1].DataType = typeof(string);
                dt.Columns[2].DataType = typeof(double);
                dt.Columns[3].DataType = typeof(double);
                dt.Columns[4].DataType = typeof(double);
                dt.Columns[5].DataType = typeof(double);
                dt.Columns[6].DataType = typeof(double);
                dt.Columns[7].DataType = typeof(string);

            }
            else
            {
                dt.Columns.AddRange(new DataColumn[6]
                {
                     new DataColumn(AppRes.DeptName),
                    new DataColumn(AppRes.AssignedSpending),
                    new DataColumn(AppRes.SpendingInMonth),
                    new DataColumn(AppRes.IncreaseSpending ),
                    new DataColumn(AppRes.SpendingIncreasedStandard),
                    new DataColumn(AppRes.CompletionRate)

                });
                dt.Columns[0].DataType = typeof(string);
                dt.Columns[1].DataType = typeof(double);
                dt.Columns[2].DataType = typeof(double);
                dt.Columns[3].DataType = typeof(double);
                dt.Columns[4].DataType = typeof(double);
                dt.Columns[5].DataType = typeof(double);
            }
            var db = new ReportIncreaseSpendingDAL();
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
            var lstData = db.ExportReportIncreaseSpending(baseListParam, list, out total, out totalColumns);
            foreach (var item in lstData)
            {

                if (list.filter7 == "2")
                {
                    dt.Rows.Add(
                        item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                        item.StaffName == null ? "" : item.StaffName,
                        item.AssignedSpending == null ? 0 : item.AssignedSpending,
                        item.SpendingInMonth == null ? 0 : item.SpendingInMonth,
                        item.IncreaseSpending == null ? 0 : item.IncreaseSpending,
                        item.SpendingIncreasedStandard == null ? 0 : item.SpendingIncreasedStandard,
                        item.CompletionRate == null ? 0 : item.CompletionRate,
                        item.StaffStatus == null ? "" : item.StaffStatus);
                        


                }
                else
                {
                    dt.Rows.Add(
                        item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                        item.AssignedSpending == null ? 0 : item.AssignedSpending,
                        item.SpendingInMonth == null ? 0 : item.SpendingInMonth,
                        item.IncreaseSpending == null ? 0 : item.IncreaseSpending,
                        item.SpendingIncreasedStandard == null ? 0 : item.SpendingIncreasedStandard,
                        item.CompletionRate == null ? 0 : item.CompletionRate);
                }

            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            var excelName = "";
            switch (list.filter7)
            {
                case "2":
                    // code block
                    excelName = "ReportIncreaseSpending.xlsx";
                    break;
                case "1":
                    // code block
                    excelName = "ReportIncreaseSpendingByDepartment.xlsx";
                    break;
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}