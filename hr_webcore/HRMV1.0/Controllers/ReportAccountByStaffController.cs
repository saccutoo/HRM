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
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Security;
using Newtonsoft.Json;
using HRM.Logger;
using static HRM.Constants.Constant;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class ReportAccountByStaffController : Controller
    {
        // GET: ReportAccount
        public ActionResult Index()
        {
            ViewBag.url = "/ReportAccountByStaff/TableServerSideGetData";
            return View();
        }
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "ReportAccountByStaff")]

        public ActionResult TableServerSideGetData(int pageIndex, string pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            var db = new ReportAccountByStaffDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
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
            var result = db.GetReportAccountByStaff(baseListParam, list, Global.CurrentUser.CurrencyTypeID??0, out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId
            }));
        }
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "ReportAccountByStaff")]

        public ActionResult ExportExcel(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            DataTable dt = new DataTable("Grid");
            if (list.filter7 == "2")
            {
                dt.Columns.AddRange(new DataColumn[17]
                {
                    new DataColumn(AppRes.DeptName),
                    new DataColumn(AppRes.Staff),
                    new DataColumn(AppRes.VIP),
                    new DataColumn(AppRes.Advanced),
                    new DataColumn(AppRes.Standard),
                    new DataColumn(AppRes.Substandard),
                    new DataColumn(AppRes.Invalid),
                    new DataColumn(AppRes.VIPS),
                    new DataColumn(AppRes.AdvancedS),
                    new DataColumn(AppRes.StandardS),
                    new DataColumn(AppRes.AccountsConvertedBySpending),
                    new DataColumn(AppRes.FeeAmount),
                    new DataColumn(AppRes.MarginR),
                    new DataColumn(AppRes.AccountsConvertedByMargin),
                    new DataColumn(AppRes.TotalAccountConversion),
                    new DataColumn(AppRes.RateACBSPerTAC),
                    new DataColumn(AppRes.RateOfMarginFeePerTotalMargin),

                });
                dt.Columns[0].DataType = typeof(string);
                dt.Columns[1].DataType = typeof(string);
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
                dt.Columns[14].DataType = typeof(double);
                dt.Columns[15].DataType = typeof(double);
                dt.Columns[16].DataType = typeof(double);

            }
            else
            {
                dt.Columns.AddRange(new DataColumn[16]
                {
                    new DataColumn(AppRes.DeptName),
                    new DataColumn(AppRes.VIP),
                    new DataColumn(AppRes.Advanced),
                    new DataColumn(AppRes.Standard),
                    new DataColumn(AppRes.Substandard),
                    new DataColumn(AppRes.Invalid),
                    new DataColumn(AppRes.VIPS),
                    new DataColumn(AppRes.AdvancedS),
                    new DataColumn(AppRes.StandardS),
                    new DataColumn(AppRes.AccountsConvertedBySpending),
                    new DataColumn(AppRes.FeeAmount),
                    new DataColumn(AppRes.MarginR),
                    new DataColumn(AppRes.AccountsConvertedByMargin),
                    new DataColumn(AppRes.TotalAccountConversion),
                    new DataColumn(AppRes.RateACBSPerTAC),
                    new DataColumn(AppRes.RateOfMarginFeePerTotalMargin),

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
                dt.Columns[14].DataType = typeof(double);
                dt.Columns[15].DataType = typeof(double);
            }
            var db = new ReportAccountByStaffDAL();
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
            var lstData = db.ExportReportAccountByStaff(baseListParam, list, out total, out totalColumns);
            foreach (var item in lstData)
            {

                if (list.filter7 == "2")
                {
                    dt.Rows.Add(
                    item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                    item.StaffName == null ? "" : item.StaffName,
                    item.VIP == null ? 0 : item.VIP,
                    item.Advanced == null ? 0 : item.Advanced,
                    item.Standard == null ? 0 : item.Standard,
                    item.Substandard == null ? 0 : item.Substandard,
                    item.Invalid == null ? 0 : item.Invalid,
                    item.VIPS == null ? 0 : item.VIPS,
                    item.AdvancedS == null ? 0 : item.AdvancedS,
                    item.StandardS == null ? 0 : item.StandardS,
                    item.AccountsConvertedBySpending == null ? 0 : item.AccountsConvertedBySpending,
                    item.FeeAmount == null ? 0 : item.FeeAmount,
                    item.Margin == null ? 0 : item.Margin,
                    item.AccountsConvertedByMargin == null ? 0 : item.AccountsConvertedByMargin,
                    item.TotalAccountsConverted == null ? 0 : item.TotalAccountsConverted,
                    item.RateACBSPerTAC == null ? 0 : item.RateACBSPerTAC,
                    item.RateOfMarginFeePerTotalMargin == null ? 0 : item.RateOfMarginFeePerTotalMargin);
                }
                else
                {
                    dt.Rows.Add(
                    item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                    item.VIP == null ? 0 : item.VIP,
                    item.Advanced == null ? 0 : item.Advanced,
                    item.Standard == null ? 0 : item.Standard,
                    item.Substandard == null ? 0 : item.Substandard,
                    item.Invalid == null ? 0 : item.Invalid,
                    item.VIPS == null ? 0 : item.VIPS,
                    item.AdvancedS == null ? 0 : item.AdvancedS,
                    item.StandardS == null ? 0 : item.StandardS,
                    item.AccountsConvertedBySpending == null ? 0 : item.AccountsConvertedBySpending,
                    item.FeeAmount == null ? 0 : item.FeeAmount,
                    item.Margin == null ? 0 : item.Margin,
                    item.AccountsConvertedByMargin == null ? 0 : item.AccountsConvertedByMargin,
                    item.TotalAccountsConverted == null ? 0 : item.TotalAccountsConverted,
                    item.RateACBSPerTAC == null ? 0 : item.RateACBSPerTAC,
                    item.RateOfMarginFeePerTotalMargin == null ? 0 : item.RateOfMarginFeePerTotalMargin);
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
                    excelName = "ReportAccountByStaff.xlsx";
                    break;
                case "1":
                    // code block
                    excelName = "ReportAccountByDepartment.xlsx";
                    break;
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}