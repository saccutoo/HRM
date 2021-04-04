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
    public class ReportSourceLeadBDXController : Controller
    {
        // GET: ReportSourceLead
        [Permission(TableID = (int)ETable.ReportSourceLead, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/ReportSourceLeadBDX/Get_CustomerClientSource_BDX";
            return View();
        }

        [Permission(TableID = (int)ETable.ReportSourceLead, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Get_CustomerClientSource_BDX")]
        public ActionResult Get_CustomerClientSource_BDX(int pageIndex, int pageSize, string filter = "",string startdate = "" ,string enddate = "", int OrganizationUnitID = 0)
        {
            var db = new ReportSourceLeadDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
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
            var result = db.Get_CustomerClientSource_BDX(baseListParam, startdate, enddate, OrganizationUnitID, out total);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                userid = baseListParam.UserId
            }));
        }

        [Permission(TableID = (int)ETable.ReportSourceLead, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "Get_CustomerClientSource_BDX")]
        public ActionResult Get_CustomerClientSource_BDXExportExcel(int pageIndex, int pageSize, string filter = "", string startdate = "", string enddate = "", int OrganizationUnitID = 0)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8]
            {
                    new DataColumn(AppRes.StatusList),
                    new DataColumn(AppRes.Quantity_MKT),
                    new DataColumn(AppRes.Quantity_Web),
                    new DataColumn(AppRes.Quantity_Sale),
                    new DataColumn(AppRes.Total),
                    new DataColumn(AppRes.Rate_MKT),
                    new DataColumn(AppRes.Rate_Web),
                    new DataColumn(AppRes.Rate_Sale)
            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(double);
            dt.Columns[2].DataType = typeof(double);
            dt.Columns[3].DataType = typeof(double);
            dt.Columns[4].DataType = typeof(double);
            dt.Columns[5].DataType = typeof(double);
            dt.Columns[6].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(double);
            var db = new ReportSourceLeadDAL();
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

            var lstData = db.Get_CustomerClientSource_BDX(baseListParam, startdate, enddate, OrganizationUnitID, out total);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                item.StatusList == null ? "" : item.StatusList,
                item.Quantity_MKT = item.Quantity_MKT,
                item.Quantity_Web = item.Quantity_Web,
                item.Quantity_Sale = item.Quantity_Sale,
                item.Total = item.Total,
                item.Rate_MKT = item.Rate_MKT,
                item.Rate_Web = item.Rate_Web,
                item.Rate_Sale = item.Rate_Sale
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
            var excelName = "Get_CustomerClientSource_BDXExportExcel.xlsx";
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}