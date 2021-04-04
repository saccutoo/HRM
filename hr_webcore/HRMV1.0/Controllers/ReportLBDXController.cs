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
    public class ReportLBDXController : Controller
    {
        // GET: ReportL
        [Permission(TableID = (int)ETable.ReportL_Staff, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/ReportLBDX/Report_L_By_Department_BDX";
            return View();
        }

        [Permission(TableID = (int)ETable.ReportL_Staff, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Report_L_By_Staff_BDX")]
        public ActionResult Report_L_By_Staff_BDX(int pageIndex, int pageSize, string filter = "", string startdate2 = "", string enddate2 = "")
        {
            var db = new ReportLDAL();
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
            var result = db.Report_L_By_Staff_BDX(baseListParam, startdate2, enddate2, out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId
            }));
        }

        [Permission(TableID = (int)ETable.ReportL_Department, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Report_L_By_Department_BDX")]
        public ActionResult Report_L_By_Department_BDX(int pageIndex, int pageSize, string filter = "", string startdate2 = "", string enddate2 = "")
        {
            var db = new ReportLDAL();
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
            var result = db.Report_L_By_Department_BDX(baseListParam, startdate2, enddate2, out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId
            }));
        }

        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "Report_L_By_Department_BDX")]
        public ActionResult Report_L_By_Department_BDXExportExcel(int pageIndex, int pageSize, string filter = "", string startdate2 = "", string enddate2 = "")
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[23]
            {
                    new DataColumn(AppRes.DeptName),
                    new DataColumn(AppRes.L0_1),
                    new DataColumn(AppRes.L0_2),
                    new DataColumn(AppRes.L0_3),
                    new DataColumn(AppRes.L0_4),
                    new DataColumn(AppRes.L1_1),
                    new DataColumn(AppRes.L1_2),
                    new DataColumn(AppRes.L1_3),
                    new DataColumn(AppRes.L1_4),
                    new DataColumn(AppRes.L2_1),
                    new DataColumn(AppRes.L2_2),
                    new DataColumn(AppRes.L2_3),
                    new DataColumn(AppRes.L2_4),
                    new DataColumn(AppRes.L3_1),
                    new DataColumn(AppRes.L3_2),
                    new DataColumn(AppRes.L3_3),
                    new DataColumn(AppRes.L3_4),
                    new DataColumn(AppRes.L4_1),
                    new DataColumn(AppRes.L4_2),
                    new DataColumn(AppRes.L5),
                    new DataColumn(AppRes.L6_1),
                    new DataColumn(AppRes.L6_2),
                    new DataColumn(AppRes.Total)
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
            dt.Columns[16].DataType = typeof(double);
            dt.Columns[17].DataType = typeof(double);
            dt.Columns[18].DataType = typeof(double);
            dt.Columns[19].DataType = typeof(double);
            dt.Columns[20].DataType = typeof(double);
            dt.Columns[21].DataType = typeof(double);
            dt.Columns[22].DataType = typeof(double);
            var db = new ReportLDAL();
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                PageIndex = pageIndex,
                PageSize = 50000,
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };

            var lstData = db.Report_L_By_Department_BDX(baseListParam, startdate2, enddate2, out total, out totalColumns);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                item.department == null ? "" : item.department,
                item.L0_1 = item.L0_1,
                item.L0_2 = item.L0_2,
                item.L0_3 = item.L0_3,
                item.L0_4 = item.L0_4,
                item.L1_1 = item.L1_1,
                item.L1_2 = item.L1_2,
                item.L1_3 = item.L1_3,
                item.L1_4 = item.L1_4,
                item.L2_1 = item.L2_1,
                item.L2_2 = item.L2_2,
                item.L2_3 = item.L2_3,
                item.L2_4 = item.L2_4,
                item.L3_1 = item.L3_1,
                item.L3_2 = item.L3_2,
                item.L3_3 = item.L3_3,
                item.L3_4 = item.L3_4,
                item.L4_1 = item.L4_1,
                item.L4_2 = item.L4_2,
                item.L5 = item.L5,
                item.L6_1 = item.L6_1,
                item.L6_2 = item.L6_2,
                item.Total = item.Total
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
            var excelName = "Report_L_By_Department_BDX.xlsx";
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [Permission(TableID = (int)ETable.PerformanceReport, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "Report_L_By_Staff_BDX")]
        public ActionResult Report_L_By_Staff_BDXExportExcel(int pageIndex, int pageSize, string filter = "", string startdate2 = "", string enddate2 = "")
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[24]
            {
                    new DataColumn(AppRes.Staff),
                    new DataColumn(AppRes.DeptName),
                    new DataColumn(AppRes.L0_1),
                    new DataColumn(AppRes.L0_2),
                    new DataColumn(AppRes.L0_3),
                    new DataColumn(AppRes.L0_4),
                    new DataColumn(AppRes.L1_1),
                    new DataColumn(AppRes.L1_2),
                    new DataColumn(AppRes.L1_3),
                    new DataColumn(AppRes.L1_4),
                    new DataColumn(AppRes.L2_1),
                    new DataColumn(AppRes.L2_2),
                    new DataColumn(AppRes.L2_3),
                    new DataColumn(AppRes.L2_4),
                    new DataColumn(AppRes.L3_1),
                    new DataColumn(AppRes.L3_2),
                    new DataColumn(AppRes.L3_3),
                    new DataColumn(AppRes.L3_4),
                    new DataColumn(AppRes.L4_1),
                    new DataColumn(AppRes.L4_2),
                    new DataColumn(AppRes.L5),
                    new DataColumn(AppRes.L6_1),
                    new DataColumn(AppRes.L6_2),
                    new DataColumn(AppRes.Total)
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
            dt.Columns[17].DataType = typeof(double);
            dt.Columns[18].DataType = typeof(double);
            dt.Columns[19].DataType = typeof(double);
            dt.Columns[20].DataType = typeof(double);
            dt.Columns[21].DataType = typeof(double);
            dt.Columns[22].DataType = typeof(double);
            dt.Columns[23].DataType = typeof(double);
            dt.Columns[23].DataType = typeof(double);
            var db = new ReportLDAL();
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                PageIndex = pageIndex,
                PageSize = 50000,
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };

            var lstData = db.Report_L_By_Staff_BDX(baseListParam, startdate2, enddate2, out total, out totalColumns);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                item.BD == null ? "" : item.BD,
                item.department == null ? "" : item.department,
                item.L0_1 = item.L0_1,
                item.L0_2 = item.L0_2,
                item.L0_3 = item.L0_3,
                item.L0_4 = item.L0_4,
                item.L1_1 = item.L1_1,
                item.L1_2 = item.L1_2,
                item.L1_3 = item.L1_3,
                item.L1_4 = item.L1_4,
                item.L2_1 = item.L2_1,
                item.L2_2 = item.L2_2,
                item.L2_3 = item.L2_3,
                item.L2_4 = item.L2_4,
                item.L3_1 = item.L3_1,
                item.L3_2 = item.L3_2,
                item.L3_3 = item.L3_3,
                item.L3_4 = item.L3_4,
                item.L4_1 = item.L4_1,
                item.L4_2 = item.L4_2,
                item.L5 = item.L5,
                item.L6_1 = item.L6_1,
                item.L6_2 = item.L6_2,
                item.Total = item.Total
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
            var excelName = "Report_L_By_Staff_BDX.xlsx";
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}