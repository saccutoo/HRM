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

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class ConfigAllowanceController : Controller
    {
        // GET: ConfigAllowance
        [Permission(TableID = (int)ETable.ConfigAllowance, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/ConfigAllowance/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.ConfigAllowance, TypeAction = (int)EAction.Get)]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Config_AllowanceDAL();
            int total = 0;
            var result = db.Config_Allowance_GetList(pageIndex, pageSize, filter, out total);
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
        [Permission(TableID = (int)ETable.ConfigAllowance, TypeAction = (int)EAction.Get)]
        public ActionResult GetConfigAllowance()
        {
            var db = new Config_AllowanceDAL();
            var result = db.GetConfigAllowance();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        public ActionResult SaveConfigAllowance()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.ConfigAllowance, TypeAction = (int)EAction.Submit)]

        public ActionResult ConfigAllowance_Save(Cofig_Allowance data)
        {
            var db = new Config_AllowanceDAL();
            //return View();
            var result = db.ConfigAllowance_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }

        [Permission(TableID = (int)ETable.ConfigAllowance, TypeAction = (int)EAction.Delete)]
        public ActionResult ConfigAllowance_Delete(int ID)
        {
            var db = new Config_AllowanceDAL();
            var result = db.ConfigAllowance_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.ConfigAllowance, TypeAction = (int)EAction.Excel)]

        public ActionResult ConfigAllowanceExportExcel(string filter)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(AppRes.Allowance),
                new DataColumn(AppRes.AllowanceEN),
                new DataColumn(AppRes.FromAmount),
                new DataColumn(AppRes.ToAmount),
                new DataColumn(AppRes.Formular),
                new DataColumn(AppRes.Note),

            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(double);
            dt.Columns[3].DataType = typeof(double);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);

            var db = new Config_AllowanceDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.Config_Allowance_GetList(1, 5000, filter, out total);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.Name, item.NameEN, item.FromAmount, item.ToAmount, item.sFormular,item.Note);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Config_Allowance.xlsx");
        }

    }
}