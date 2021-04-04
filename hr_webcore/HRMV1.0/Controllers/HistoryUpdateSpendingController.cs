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
    public class HistoryUpdateSpendingController : Controller
    {
        // GET: HistoryUpdateSpending
        public ActionResult Index()
        {
            ViewBag.url = "/HistoryUpdateSpending/TableServerSideGetData";
            return View();
        }
        [WriteLog(Action = Constant.EAction.Get, LogStoreProcedure = "HistoryUpdateSpending")]
        public ActionResult TableServerSideGetData(int pageIndex, string pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            var db = new HistoryUpdateSpendingDAL();
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
            var result = db.getHistoryUpdateSpending(baseListParam, list, Global.CurrentUser.CurrencyTypeID ?? 0, out total, out totalColumns);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId
            }));
        }

        [WriteLog(Action = Constant.EAction.Excel, LogStoreProcedure = "HistoryUpdateSpending")]
        public ActionResult ExportExcel(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            DataTable dt = new DataTable("Grid");

            dt.Columns.AddRange(new DataColumn[5]
            {
                    new DataColumn(AppRes.AccountNumber),
                    new DataColumn(AppRes.SpendingDay),
                    new DataColumn(AppRes.InitialAmount),
                    new DataColumn(AppRes.AmountChanged),
                    new DataColumn(AppRes.ModifiedDate),

            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(DateTime?);
            dt.Columns[2].DataType = typeof(double);
            dt.Columns[3].DataType = typeof(double);
            dt.Columns[5].DataType = typeof(DateTime?);


            var db = new HistoryUpdateSpendingDAL();
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
            var lstData = db.getHistoryUpdateSpendingForExcel(baseListParam, list, Global.CurrentUser.CurrencyTypeID ?? 0, out total, out totalColumns);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                    item.AccountNumber ?? "",
                    item.Day,
                    item.AmountVND ?? 0,
                    item.AmountChanged ?? 0,
                    item.ModifiedDate);
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
            excelName = "CustomerTransferHistory.xlsx";
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}