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
using HRM.Models;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class DetailAccountReportController : Controller
    {
        // GET: DetailAccountReport
        public ActionResult Index()
        {
            ViewBag.url = "/DetailAccountReport/TableServerSideGetDataAccount";
            ViewBag.url1 = "/DetailAccountReport/TableServerSideGetDataPayment";
            ViewBag.url2 = "/DetailAccountReport/TableServerSideGetDataPaymentRefer";
            return View();
        }

        [WriteLog(Action = Constant.EAction.Get, LogStoreProcedure = "ReportMccAccountDetailNew")]
        public ActionResult TableServerSideGetDataAccount(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {
            var db = new DetailAccountReportDAL();
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
                PageSize = pageSize,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.ReportMccAccountDetailNew(baseListParam, list, Global.CurrentUser.CurrencyTypeID??0, out total, out totalColumns);
            var lstTotal = new TableColumnsTotalModel();
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                roleid = baseListParam.UserType.ToString()

            }));

        }

        [WriteLog(Action = Constant.EAction.Get, LogStoreProcedure = "PaymentProductReportGets")]
        public ActionResult TableServerSideGetDataPayment(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {

            var db = new PaymentProductDAL();
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
                PageSize = pageSize,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetPaymentProductReport(baseListParam, list, out total, out totalColumns);
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                roleid = baseListParam.UserType.ToString()

            }));
        }

        [WriteLog(Action = Constant.EAction.Get, LogStoreProcedure = "ReportsMarginRefer")]
        public ActionResult TableServerSideGetDataPaymentRefer(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {
            var db = new PaymentProductDAL();
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
                PageSize = pageSize,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetPaymentProductRefer(baseListParam, list, out total, out totalColumns);
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                roleid = baseListParam.UserType.ToString()

            }));
        }

        [WriteLog(Action = Constant.EAction.Excel, LogStoreProcedure = "ReportMccAccountDetailNew")]
        public ActionResult DetailAccountReportExportExcel(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {
            filter = filter.Replace("!!", "%");
            DateTime FromDateTypeDate = new DateTime();
            DateTime ToDateTypeDate = new DateTime();
            list.FromDate = Convert.ToDateTime(DateTime.ParseExact(list.StringFromDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            list.ToDate = Convert.ToDateTime(DateTime.ParseExact(list.StringToDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[11]
            {
                new DataColumn(AppRes.AccCode),
                new DataColumn(AppRes.TitleAccount),
                new DataColumn(AppRes.AccountStatus),
                new DataColumn(AppRes.AccountLevel),
                new DataColumn(AppRes.isPartner),
                new DataColumn(AppRes.CalculationDate),
                new DataColumn(AppRes.BD),
                new DataColumn(AppRes.OrganizationUnitName),
                new DataColumn(AppRes.Category),
                new DataColumn(AppRes.T),
                new DataColumn(AppRes.AccountConverted)
            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(double);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(bool);
            dt.Columns[5].DataType = typeof(DateTime);
            dt.Columns[6].DataType = typeof(string);
            dt.Columns[7].DataType = typeof(string);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(double);
            dt.Columns[10].DataType = typeof(double);

            var db = new DetailAccountReportDAL();
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
                PageSize = int.MaxValue,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var lstData = db.ExportExcelDetailAccountReport(baseListParam, list);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                    item.CustomerCode,
                    item.AccountNumber,
                    item.StatusAccount == null ? "" : item.StatusAccount,
                    item.AccountLevel == null ? "" : item.AccountLevel,
                    item.IsPartner,
                    item.FirstDateLinked,
                    item.BD,
                    item.Department,
                    item.AccountType == null ? "" : item.AccountType,
                    item.T == null ? 0 : item.T,
                    item.AccountConversion == null ? 0 : item.AccountConversion
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DetailAccountReport.xlsx");
        }

        [WriteLog(Action = Constant.EAction.Excel, LogStoreProcedure = "PaymentProductReportGets")]
        public ActionResult PaymentExportExcel(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {
            filter = filter.Replace("!!", "%");
            DateTime FromDateTypeDate = new DateTime();
            DateTime ToDateTypeDate = new DateTime();
            list.FromDate = Convert.ToDateTime(DateTime.ParseExact(list.StringFromDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            list.ToDate = Convert.ToDateTime(DateTime.ParseExact(list.StringToDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[10]
            {
                  new DataColumn(AppRes.CustomerCode),
                    new DataColumn(AppRes.Email),
                new DataColumn(AppRes.Website),
                 new DataColumn(AppRes.BD),
                new DataColumn(AppRes.OrganizationUnitName),
               new DataColumn(AppRes.Product),
               new DataColumn(AppRes.Margin),
                new DataColumn(AppRes.PaymentDate),
                new DataColumn(AppRes.StatusName),
                new DataColumn(AppRes.AccountConverted)
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(DateTime);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(double);

            var db = new PaymentProductDAL();
            int? total = 0;
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = int.MaxValue,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var lstData = db.ExportExcelPaymentProduct(baseListParam, list, out total);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                    item.CustomerID,
                    item.Email == null ? "" : item.Email,
                    item.Website == null ? "" : item.Website,
                    item.BDName == null ? "" : item.BDName,
                    item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                    item.ProductName == null ? "" : item.ProductName,
                    item.Amount,
                    item.PaymentDate,
                    item.StatusName == null ? "" : item.StatusName,
                    item.AccountConversion
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReportPaymentProduct.xlsx");
        }

        [WriteLog(Action = Constant.EAction.Excel, LogStoreProcedure = "ReportsMarginRefer")]
        public ActionResult PaymentReferExportExcel(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {
            filter = filter.Replace("!!", "%");
            DateTime FromDateTypeDate = new DateTime();
            DateTime ToDateTypeDate = new DateTime();
            list.FromDate = Convert.ToDateTime(DateTime.ParseExact(list.StringFromDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            list.ToDate = Convert.ToDateTime(DateTime.ParseExact(list.StringToDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8]
            {
                new DataColumn(AppRes.CustomerCode),
                 new DataColumn(AppRes.Email),
                new DataColumn(AppRes.ContractCode),
               new DataColumn(AppRes.PaymentDate),
               new DataColumn(AppRes.BD),
                new DataColumn(AppRes.OrganizationUnitName),
                new DataColumn(AppRes.Amount),
                new DataColumn(AppRes.AccountConverted)
            });

            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(DateTime);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(double);

            var db = new PaymentProductDAL();
            int? total = 0;
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = int.MaxValue,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var lstData = db.ExportExcelPaymentRefer(baseListParam, list, out total);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                    item.CustomerCode,
                    item.Email,
                    item.ContractCode == null ? "" : item.ContractCode,
                    item.PaymentDate,
                    item.BD == null ? "" : item.BD,
                    item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                    item.Amount,
                    item.AccountConversion
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReportsMarginRefer.xlsx");
        }
    }
}