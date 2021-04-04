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
using HRM.DataAccess.Entity.UserDefinedType;
namespace HRM.Controllers
{
    [HRMAuthorize]
    public class OrganizationUnitPlanController : Controller
    {
        // GET: OrganizationUnitPlanController
        [Permission(TableID = (int)ETable.OrganizationUnitPlan, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/OrganizationUnitPlan/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.OrganizationUnitPlan, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "OrganizationUnitPlan_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new OrganizationUnitPlan_DAL();
            ToTalMonth ToTalMonth = new ToTalMonth();
            int total = 0;
            int Languagecode = Global.CurrentLanguage;
            var result = db.OrganizationUnitPlan_GetList(pageIndex, pageSize, filter, Languagecode, out total, out ToTalMonth);
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal,
                ToTalMonth = ToTalMonth

            }));
        }

        //[Permission(TableID = (int)ETable.OrganizationUnitPlan, TypeAction = (int)EAction.Get)]
        //[WriteLog(Action = EAction.Get, LogStoreProcedure = "OrganizationUnit_GetALL")]
        public ActionResult OrganizationUnit_GetALL()
        {
            var db = new OrganizationUnitPlan_DAL();
            var result = db.OrganizationUnit_GetALL();           
            return Content(JsonConvert.SerializeObject(new
            {
                result,                
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnitPlan, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "OrganizationUnitPlan_Save")]
        public ActionResult OrganizationUnitPlan_Save(List<OrganizationUnitPlan> data)
        {
            
            var db = new OrganizationUnitPlan_DAL();
            var result = new SystemMessage();
            foreach (var item in data)
            {
                item.Type = 0;
                item.CreatedBy = Global.CurrentUser.UserID;
                item.CreatedOn = DateTime.Now;
                item.CreatedOn = DateTime.Now;
                item.ModifiedBy = Global.CurrentUser.UserID;
                result = db.OrganizationUnitPlan_Save(item);
            }
            
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnitPlan, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "OrganizationUnitPlan_Delete")]
        public ActionResult OrganizationUnitPlan_Delete(int ID)
        {

            var db = new OrganizationUnitPlan_DAL();
            var result = db.OrganizationUnitPlan_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult OrganizationUnitPlanSave()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.OrganizationUnitPlan, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "OrganizationUnitPlan_GetList")]
        public ActionResult OrganizationUnitPlanExportExcel(string filter = "")
        {
            DataTable dt = new DataTable("Grid");
            if (filter.Contains("!!") == true)
            {
                filter = filter.Replace("!!", "%");
            }
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(AppRes.OrganizationUnit),
                new DataColumn(AppRes.Currency),
                new DataColumn(AppRes.Year),
                new DataColumn(AppRes.Promotion_Status),
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
                new DataColumn(AppRes.R_Total),
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

            var db = new OrganizationUnitPlan_DAL();
            ToTalMonth ToTalMonth = new ToTalMonth();

            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.OrganizationUnitPlan_GetList(1, 10000, filter, LanguageCode, out total,out ToTalMonth);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.OrganizationUnitName, item.CurrencyName, item.Year,item.StatusName, item.M1, item.M2, item.M3, item.M4, item.M5, item.M6, item.M7,item.M8,item.M9,item.M10,item.M11,item.M12,item.SumValue);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OrganizationUnitPlan.xlsx");
        }

    }

}