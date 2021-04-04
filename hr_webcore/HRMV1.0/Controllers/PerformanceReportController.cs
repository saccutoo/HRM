using ERP.Framework.DataBusiness.Common;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Security;
using HRM.DataAccess.Entity;
using HRM.DataAccess.Common;
using System.Data;

using ClosedXML.Excel;
using System.IO;
using HRM.App_LocalResources;
using System.Text;
using static HRM.Constants.Constant;
using HRM.Logger;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class PerformanceReportController : Controller
    {
        // PerformanceReport BD
        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Index)]
        public ActionResult BD()
        {
            ViewBag.url = "/PerformanceReport/TableServerSideGetData";
            ViewBag.viewType = 1;
            return View();
        }

        // PerformanceReport Media
        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Index)]
        public ActionResult Media()
        {
            ViewBag.url = "/PerformanceReport/TableServerSideGetData";
            ViewBag.viewType = 2;
            return View();
        }

        // PerformanceReport Account
        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Index)]
        public ActionResult Account()
        {
            ViewBag.url = "/PerformanceReport/TableServerSideGetData";
            ViewBag.viewType = 3;
            return View();
        }

        // PerformanceReport Design
        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Index)]
        public ActionResult Design()
        {
            ViewBag.url = "/PerformanceReport/TableServerSideGetData";
            ViewBag.viewType = 4;
            return View();
        }

        // PerformanceReport Social
        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Index)]
        public ActionResult Social()
        {
            ViewBag.url = "/PerformanceReport/TableServerSideGetData";
            ViewBag.viewType = 5;
            return View();
        }

        // PerformanceReport AutoAds
        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Index)]
        public ActionResult AutoAds()
        {
            ViewBag.url = "/PerformanceReport/TableServerSideGetData";
            ViewBag.viewType = 6;
            return View();
        }

        // PerformanceReport BDX
        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Index)]
        public ActionResult BDX()
        {
            ViewBag.url = "/PerformanceReport/TableServerSideGetData";
            ViewBag.viewType = 7;
            return View();
        }

        // PerformanceReport CSX
        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Index)]
        public ActionResult CSX()
        {
            ViewBag.url = "/PerformanceReport/TableServerSideGetData";
            ViewBag.viewType = 8;
            return View();
        }

        // PerformanceReport TECH
        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Index)]
        public ActionResult TECH()
        {
            ViewBag.url = "/PerformanceReport/TableServerSideGetData";
            ViewBag.viewType = 9;
            return View();
        }

        // PerformanceReport ONN
        [Permission(TableID = (int)ETable.PerformanceReportONN, TypeAction = (int)EAction.Index)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PerformanceReportONN")]
        public ActionResult ONN()
        {
            ViewBag.url = "/PerformanceReport/PerformanceReportONNGetData";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="viewType"></param>
        /// <param name="fromMonth"> filter1</param>
        /// <param name="toMonth">   filter2</param>
        /// <param name="fromQuarter">   filter3</param>
        /// <param name="toQuarter">   filter4</param>
        /// <param name="year">   filter5</param>
        /// <param name="type">   filter6</param>
        /// <param name="reportType">  filter7</param>
        /// <param name="filter">    </param>
        /// <returns></returns>
        /// 
        [Permission(TableID = (int)ETable.PerformanceReportONN, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PerformanceReportONN")]
        public ActionResult PerformanceReportONNGetData(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {
            filter = filter +  (!String.IsNullOrEmpty(list.filter9)  ? (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") + " CompanyName LIKE N'%" + list.filter9 + "%'" : "");
            filter = filter +  (!String.IsNullOrEmpty(list.filter10) ? (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") + " UnitName LIKE N'%" + list.filter10 + "%'" : "");
            filter = filter +  (!String.IsNullOrEmpty(list.filter11) ? (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") + " OrganizationUnitName LIKE N'%" + list.filter11 + "%'" : "");
            filter = filter +  (!String.IsNullOrEmpty(list.filter12) ? (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") + " StaffName LIKE N'%" + list.filter12 + "%'" : "");

            if (Global.CurrentUser.OfficePositionID == 250 || Global.CurrentUser.OfficePositionID == 251 ||
                Global.CurrentUser.OfficePositionID == 252 || Global.CurrentUser.OfficePositionID == 253 ||
                Global.CurrentUser.OfficePositionID == 254 || Global.CurrentUser.RoleId == 1 ||
                Global.CurrentUser.RoleId == 11)
            {
                list.filter7 = list.filter7;
            }
            else
            {
                list.filter7 = "2";
            }
            filter = filter.Replace("!!", "%");
            var db = new PerformanceReportDAL();
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
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.PerformanceReportONN(baseListParam, list, Global.CurrentUser.CurrencyTypeID??194,0 ,out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId
            }));
        }

        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "ReportsPerformanceBD")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {
            if (Global.CurrentUser.OfficePositionID == 250 || Global.CurrentUser.OfficePositionID == 251 ||
                Global.CurrentUser.OfficePositionID == 252 || Global.CurrentUser.OfficePositionID == 253 ||
                Global.CurrentUser.OfficePositionID == 254 || Global.CurrentUser.RoleId == 1 ||
                Global.CurrentUser.RoleId == 11)
            {
                list.filter7 = list.filter7;
            }
            else
            {
                list.filter7 = "2";
            }

            filter = filter + (!String.IsNullOrEmpty(list.filter11) ? (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") + " OrganizationUnitName LIKE N'%" + list.filter11 + "%'" : "");
            filter = filter + (!String.IsNullOrEmpty(list.filter12) ? (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") + " StaffName LIKE N'%" + list.filter12 + "%'" : "");

            filter = filter.Replace("!!", "%");
            var db = new PerformanceReportDAL();
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
                PageSize = pageSize,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetPerformanceReport(baseListParam, list, out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId
            }));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromMonth">filter1</param>
        /// <param name="toMonth">filter2</param>
        /// <param name="fromQuarter">filter3</param>
        /// <param name="toQuarter">filter4</param>
        /// <param name="year">filter5</param>
        /// <param name="type">filter6</param>
        /// <param name="reportType"> filter7</param>
        /// <param name="getColumnMonth or get ColumnQuarter">  filter8</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "ReportsPerformanceBD")]
        public ActionResult PerformanceReportExportExcel(int pageIndex, int pageSize,ListFilterParam list, string filter = "")
        {

            filter = filter + (!String.IsNullOrEmpty(list.filter11) ? (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") + " OrganizationUnitName LIKE N'%" + list.filter11 + "%'" : "");
            filter = filter + (!String.IsNullOrEmpty(list.filter12) ? (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") + " StaffName LIKE N'%" + list.filter12 + "%'" : "");
            if (Global.CurrentUser.OfficePositionID == 250 || Global.CurrentUser.OfficePositionID == 251 ||
                Global.CurrentUser.OfficePositionID == 252 || Global.CurrentUser.OfficePositionID == 253 ||
                Global.CurrentUser.OfficePositionID == 254 || Global.CurrentUser.RoleId != 1 ||
                Global.CurrentUser.RoleId == 11)
            {
                list.filter7 = list.filter7;
            }
            else
            {
                list.filter7 = "2";
            }
            filter = filter.Replace("!!", "%");
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[40]
            {
                    new DataColumn(AppRes.DeptName),
                    new DataColumn(AppRes.Staff),
                    new DataColumn(AppRes.StaffCode),
                    new DataColumn(AppRes.OrganazationUnitCode),
                    new DataColumn(AppRes.L1),
                    new DataColumn(AppRes.L2),
                    new DataColumn(AppRes.L3),
                    new DataColumn(AppRes.L4),
                    new DataColumn(AppRes.L5),
                    new DataColumn(AppRes.L6),
                    new DataColumn(AppRes.L7),
                    new DataColumn(AppRes.L8),
                    new DataColumn(AppRes.L9),
                    new DataColumn(AppRes.L10),
                    new DataColumn(AppRes.L11),
                    new DataColumn(AppRes.L12),
                    new DataColumn(AppRes.TotalMonth),
                    new DataColumn(AppRes.Rate1),
                    new DataColumn(AppRes.Rate2),
                    new DataColumn(AppRes.Rate3),
                    new DataColumn(AppRes.Rate4),
                    new DataColumn(AppRes.Rate5),
                    new DataColumn(AppRes.Rate6),
                    new DataColumn(AppRes.Rate7),
                    new DataColumn(AppRes.Rate8),
                    new DataColumn(AppRes.Rate9),
                    new DataColumn(AppRes.Rate10),
                    new DataColumn(AppRes.Rate11),
                    new DataColumn(AppRes.Rate12),
                    new DataColumn(AppRes.QL1),
                    new DataColumn(AppRes.QL2),
                    new DataColumn(AppRes.QL3),
                    new DataColumn(AppRes.QL4),
                    new DataColumn(AppRes.TotalQuarter),
                    new DataColumn(AppRes.RateQ1),
                    new DataColumn(AppRes.RateQ2),
                    new DataColumn(AppRes.RateQ3),
                    new DataColumn(AppRes.RateQ4),
                    new DataColumn(AppRes.R_RateYear),
                    new DataColumn(AppRes.Note)
            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
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
            dt.Columns[17].DataType = typeof(double);
            dt.Columns[18].DataType = typeof(double);
            dt.Columns[19].DataType = typeof(double);
            dt.Columns[20].DataType = typeof(double);
            dt.Columns[21].DataType = typeof(double);
            dt.Columns[22].DataType = typeof(double);
            dt.Columns[23].DataType = typeof(double);
            dt.Columns[24].DataType = typeof(double);
            dt.Columns[25].DataType = typeof(double);
            dt.Columns[26].DataType = typeof(double);
            dt.Columns[27].DataType = typeof(double);
            dt.Columns[28].DataType = typeof(double);
            dt.Columns[29].DataType = typeof(double);
            dt.Columns[30].DataType = typeof(double);
            dt.Columns[31].DataType = typeof(double);
            dt.Columns[32].DataType = typeof(double);
            dt.Columns[33].DataType = typeof(double);
            dt.Columns[34].DataType = typeof(double);
            dt.Columns[35].DataType = typeof(double);
            dt.Columns[36].DataType = typeof(double);
            dt.Columns[37].DataType = typeof(double);
            dt.Columns[38].DataType = typeof(double);
            dt.Columns[39].DataType = typeof(string);
            var db = new PerformanceReportDAL();
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                PageIndex = pageIndex,
                PageSize = int.MaxValue,
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };

            var lstData = db.ExportExcelPerformanceReport(baseListParam, list, out total, out totalColumns);
            foreach (var item in lstData)
            {
              
                dt.Rows.Add(
                item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                item.StaffName ?? "",
                item.StaffCode ?? "",
                item.OrganazationUnitCode ?? "",
                item.L1 == null ? 0 : Math.Round(item.L1??0,2),
                item.L2 == null ? 0 : Math.Round(item.L2??0,2),
                item.L3 == null ? 0 : Math.Round(item.L3??0,2),
                item.L4 == null ? 0 : Math.Round(item.L4??0,2),
                item.L5 == null ? 0 : Math.Round(item.L5??0,2),
                item.L6 == null ? 0 : Math.Round(item.L6??0,2),
                item.L7 == null ? 0 : Math.Round(item.L7??0,2),
                item.L8 == null ? 0 : Math.Round(item.L8??0,2),
                item.L9 == null ? 0 : Math.Round(item.L9??0,2),
                item.L10 == null ? 0 : Math.Round(item.L10??0,2),
                item.L11 == null ? 0 : Math.Round(item.L11??0,2),
                item.L12 == null ? 0 : Math.Round(item.L12 ?? 0, 2),
                item.TotalMonth == null ? 0 : Math.Round(item.TotalMonth ?? 0, 2),
                item.Rate1  == null ? 0 : Math.Round(item.Rate1??0,2),
                item.Rate2  == null ? 0 : Math.Round(item.Rate2??0,2),
                item.Rate3  == null ? 0 : Math.Round(item.Rate3??0,2),
                item.Rate4  == null ? 0 : Math.Round(item.Rate4??0,2),
                item.Rate5  == null ? 0 : Math.Round(item.Rate5??0,2),
                item.Rate6  == null ? 0 : Math.Round(item.Rate6??0,2),
                item.Rate7  == null ? 0 : Math.Round(item.Rate7??0,2),
                item.Rate8  == null ? 0 : Math.Round(item.Rate8??0,2),
                item.Rate9  == null ? 0 : Math.Round(item.Rate9??0,2),
                item.Rate10 == null ? 0 : Math.Round(item.Rate10??0,2),
                item.Rate11 == null ? 0 : Math.Round(item.Rate11??0,2),
                item.Rate12 == null ? 0 : Math.Round(item.Rate12??0,2),
                item.QL1 == null ? 0 : Math.Round(item.QL1??0,2),
                item.QL2 == null ? 0 : Math.Round(item.QL2??0,2),
                item.QL3 == null ? 0 : Math.Round(item.QL3??0,2),
                item.QL4 == null ? 0 : Math.Round(item.QL4 ?? 0, 2),
                item.TotalQuarter == null ? 0 : Math.Round(item.TotalQuarter ?? 0, 2),
                item.RateQ1 == null ? 0 : Math.Round(item.RateQ1??0,2),
                item.RateQ2 == null ? 0 : Math.Round(item.RateQ2??0,2),
                item.RateQ3 == null ? 0 : Math.Round(item.RateQ3??0,2),
                item.RateQ4 == null ? 0 : Math.Round(item.RateQ4 ?? 0, 2),
                item.RateMonth == null ? 0 : Math.Round(item.RateMonth??0,2),
                 item.Note == null ? "" : item.Note
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
            var excelName = "";
            switch (list.viewType)
            {
                case 1:
                    // code block
                    excelName = "PerformanceReportBD.xlsx";
                    break;
                case 2:
                    // code block
                    excelName = "PerformanceReportMedia.xlsx";
                    break;
                case 3:
                    // code block
                    excelName = "PerformanceReportAccount.xlsx";
                    break;
                case 4:
                    // code block
                    excelName = "PerformanceReportDesign.xlsx";
                    break;
                case 5:
                    // code block
                    excelName = "PerformanceReportSocial.xlsx";
                    break;
                case 6:
                    // code block
                    excelName = "PerformanceReportAutoAds.xlsx";
                    break;
                case 7:
                    // code block
                    excelName = "PerformanceReportBDX.xlsx";
                    break;
                case 8:
                    // code block
                    excelName = "PerformanceReportCSX.xlsx";
                    break;
                case 9:
                    // code block
                    excelName = "PerformanceReportTECH.xlsx";
                    break;

}
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "ReportsPerformanceONN")]
        public ActionResult PerformanceReportONNExportExcel(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {
            filter = filter + (!String.IsNullOrEmpty(list.filter9)  ?  (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") +" CompanyName LIKE N'%" + list.filter9 + "%'" : "");
            filter = filter + (!String.IsNullOrEmpty(list.filter10) ?  (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") +" UnitName LIKE N'%" + list.filter10 + "%'" : "");
            filter = filter + (!String.IsNullOrEmpty(list.filter11) ?  (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") +" OrganizationUnitName LIKE N'%" + list.filter11 + "%'" : "");
            filter = filter + (!String.IsNullOrEmpty(list.filter12) ?  (!String.IsNullOrEmpty(filter.Trim()) ? " AND " : "") +" StaffName LIKE N'%" + list.filter12 + "%'" : "");

            if (Global.CurrentUser.OfficePositionID == 250 || Global.CurrentUser.OfficePositionID == 251 ||
                Global.CurrentUser.OfficePositionID == 252 || Global.CurrentUser.OfficePositionID == 253 ||
                Global.CurrentUser.OfficePositionID == 254 || Global.CurrentUser.RoleId != 1 ||
                Global.CurrentUser.RoleId == 11)
            {
                list.filter7 = list.filter7;
            }
            else
            {
                list.filter7 = "2";
            }
            filter = filter.Replace("!!", "%");
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[42]
            {
                    new DataColumn(AppRes.Company),
                    new DataColumn(AppRes.ByUnit),
                    new DataColumn(AppRes.DeptName),
                    new DataColumn(AppRes.Staff),
                    new DataColumn(AppRes.StaffCode),
                    new DataColumn(AppRes.OrganazationUnitCode),
                    new DataColumn(AppRes.L1),
                    new DataColumn(AppRes.L2),
                    new DataColumn(AppRes.L3),
                    new DataColumn(AppRes.L4),
                    new DataColumn(AppRes.L5),
                    new DataColumn(AppRes.L6),
                    new DataColumn(AppRes.L7),
                    new DataColumn(AppRes.L8),
                    new DataColumn(AppRes.L9),
                    new DataColumn(AppRes.L10),
                    new DataColumn(AppRes.L11),
                    new DataColumn(AppRes.L12),
                    new DataColumn(AppRes.TotalMonth),
                    new DataColumn(AppRes.Rate1),
                    new DataColumn(AppRes.Rate2),
                    new DataColumn(AppRes.Rate3),
                    new DataColumn(AppRes.Rate4),
                    new DataColumn(AppRes.Rate5),
                    new DataColumn(AppRes.Rate6),
                    new DataColumn(AppRes.Rate7),
                    new DataColumn(AppRes.Rate8),
                    new DataColumn(AppRes.Rate9),
                    new DataColumn(AppRes.Rate10),
                    new DataColumn(AppRes.Rate11),
                    new DataColumn(AppRes.Rate12),
                    new DataColumn(AppRes.QL1),
                    new DataColumn(AppRes.QL2),
                    new DataColumn(AppRes.QL3),
                    new DataColumn(AppRes.QL4),
                    new DataColumn(AppRes.TotalQuarter),

                    new DataColumn(AppRes.RateQ1),
                    new DataColumn(AppRes.RateQ2),
                    new DataColumn(AppRes.RateQ3),
                    new DataColumn(AppRes.RateQ4),
                    new DataColumn(AppRes.R_RateYear),
                    new DataColumn(AppRes.Note)
            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
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
            dt.Columns[17].DataType = typeof(double);
            dt.Columns[18].DataType = typeof(double);
            dt.Columns[19].DataType = typeof(double);
            dt.Columns[20].DataType = typeof(double);
            dt.Columns[21].DataType = typeof(double);
            dt.Columns[22].DataType = typeof(double);
            dt.Columns[23].DataType = typeof(double);
            dt.Columns[24].DataType = typeof(double);
            dt.Columns[25].DataType = typeof(double);
            dt.Columns[26].DataType = typeof(double);
            dt.Columns[27].DataType = typeof(double);
            dt.Columns[28].DataType = typeof(double);
            dt.Columns[29].DataType = typeof(double);
            dt.Columns[30].DataType = typeof(double);
            dt.Columns[31].DataType = typeof(double);
            dt.Columns[32].DataType = typeof(double);
            dt.Columns[33].DataType = typeof(double);
            dt.Columns[34].DataType = typeof(double);
            dt.Columns[35].DataType = typeof(double);
            dt.Columns[36].DataType = typeof(double);
            dt.Columns[37].DataType = typeof(double);
            dt.Columns[38].DataType = typeof(double);
            dt.Columns[39].DataType = typeof(double);
            dt.Columns[40].DataType = typeof(double);
            dt.Columns[41].DataType = typeof(string);
            var db = new PerformanceReportDAL();
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                PageIndex = 1,
                PageSize = int.MaxValue,
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };

            var lstData = db.PerformanceReportONN(baseListParam, list, Global.CurrentUser.CurrencyTypeID??0, 1, out total, out totalColumns);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                item.CompanyName == null ? "" : item.CompanyName,
                item.UnitName == null ? "" : item.UnitName,
                item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                item.StaffName ?? "",
                item.StaffCode == null ? "" : item.StaffCode,
                item.OrganazationUnitCode == null ? "" : item.OrganazationUnitCode,
                item.L1 == null ? 0 : Math.Round(item.L1 ?? 0, 2),
                item.L2 == null ? 0 : Math.Round(item.L2 ?? 0, 2),
                item.L3 == null ? 0 : Math.Round(item.L3 ?? 0, 2),
                item.L4 == null ? 0 : Math.Round(item.L4 ?? 0, 2),
                item.L5 == null ? 0 : Math.Round(item.L5 ?? 0, 2),
                item.L6 == null ? 0 : Math.Round(item.L6 ?? 0, 2),
                item.L7 == null ? 0 : Math.Round(item.L7 ?? 0, 2),
                item.L8 == null ? 0 : Math.Round(item.L8 ?? 0, 2),
                item.L9 == null ? 0 : Math.Round(item.L9 ?? 0, 2),
                item.L10 == null ? 0 : Math.Round(item.L10 ?? 0, 2),
                item.L11 == null ? 0 : Math.Round(item.L11 ?? 0, 2),
                item.L12 == null ? 0 : Math.Round(item.L12 ?? 0, 2),
                item.TotalMonth == null ? 0 : Math.Round(item.TotalMonth ?? 0, 2),
                item.Rate1 == null ? 0 : Math.Round(item.Rate1 ?? 0, 2),
                item.Rate2 == null ? 0 : Math.Round(item.Rate2 ?? 0, 2),
                item.Rate3 == null ? 0 : Math.Round(item.Rate3 ?? 0, 2),
                item.Rate4 == null ? 0 : Math.Round(item.Rate4 ?? 0, 2),
                item.Rate5 == null ? 0 : Math.Round(item.Rate5 ?? 0, 2),
                item.Rate6 == null ? 0 : Math.Round(item.Rate6 ?? 0, 2),
                item.Rate7 == null ? 0 : Math.Round(item.Rate7 ?? 0, 2),
                item.Rate8 == null ? 0 : Math.Round(item.Rate8 ?? 0, 2),
                item.Rate9 == null ? 0 : Math.Round(item.Rate9 ?? 0, 2),
                item.Rate10 == null ? 0 : Math.Round(item.Rate10 ?? 0, 2),
                item.Rate11 == null ? 0 : Math.Round(item.Rate11 ?? 0, 2),
                item.Rate12 == null ? 0 : Math.Round(item.Rate12 ?? 0, 2),
                item.QL1 == null ? 0 : Math.Round(item.QL1 ?? 0, 2),
                item.QL2 == null ? 0 : Math.Round(item.QL2 ?? 0, 2),
                item.QL3 == null ? 0 : Math.Round(item.QL3 ?? 0, 2),
                item.QL4 == null ? 0 : Math.Round(item.QL4 ?? 0, 2),
                item.TotalQuarter == null ? 0 : Math.Round(item.TotalQuarter ?? 0, 2),
                item.RateQ1 == null ? 0 : Math.Round(item.RateQ1 ?? 0, 2),
                item.RateQ2 == null ? 0 : Math.Round(item.RateQ2 ?? 0, 2),
                item.RateQ3 == null ? 0 : Math.Round(item.RateQ3 ?? 0, 2),
                item.RateQ4 == null ? 0 : Math.Round(item.RateQ4 ?? 0, 2),
                item.RateMonth == null ? 0 : Math.Round(item.RateMonth ?? 0, 2),
                item.Note == null ? "" : item.Note
                );

            }

            
            if(list.filter7 == "4")
            {
                dt.Columns.Remove(AppRes.DeptName);
                dt.Columns.Remove(AppRes.ByUnit);
                dt.Columns.Remove(AppRes.Staff);
            }
            else if(list.filter7 == "3")
            {
                dt.Columns.Remove(AppRes.DeptName);
                dt.Columns.Remove(AppRes.Staff);
            }
            else if (list.filter7 == "1")
            {
                dt.Columns.Remove(AppRes.Staff);
            }
            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            var excelName = "PerformanceReport_ONN.xlsx";
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}