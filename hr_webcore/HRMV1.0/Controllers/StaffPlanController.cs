using ClosedXML.Excel;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.DataAccess.Entity;
using HRM.Models;
using ERP.Framework.DataBusiness.Common;
using HRM.Security;
using static HRM.Constants.Constant;
using HRM.Logger;
using HRM.DataAccess.Entity.UserDefinedType;

namespace HRM.Controllers
{
    public class StaffPlanController : Controller
    {
        // GET: StaffPlan

        [Permission(TableID = (int)ETable.StaffPlan, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/StaffPlan/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.StaffPlan, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "SocialInsuranceDetail_Gets")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new StaffPlan_DAL();
            int total = 0;
            ToTalMonth ToTalMonth = new ToTalMonth();
            int LanguageCode = Global.CurrentLanguage;
            var result = db.StaffPlan_GetList(pageIndex, pageSize, filter, LanguageCode, out total, out ToTalMonth);
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            lstTotal.Total4 = "45";
            lstTotal.Total5 = "55";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal,
                ToTalMonth = ToTalMonth

            }));
        }
        [Permission(TableID = (int)ETable.StaffPlan, TypeAction = (int)EAction.Get)]

        public ActionResult SaveStaffPlan()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.StaffPlan, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "StaffPlan_Save")]

        public ActionResult StaffPlan_Save(List<StaffPlan> data)
        {
            var db = new StaffPlan_DAL();
            var result = new SystemMessage();
            foreach (var item in data)
            {
                item.Type = 0;
                item.CreatedBy = Global.CurrentUser.UserID;
                item.CreatedOn = DateTime.Now;
                item.CreatedOn = DateTime.Now;
                item.ModifiedBy = Global.CurrentUser.UserID;
                result = db.StaffPlan_Save(item);
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

        [Permission(TableID = (int)ETable.StaffPlan, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "StaffPlan_Delete")]

        public ActionResult StaffPlan_Delete(int ID)
        {

            var db = new StaffPlan_DAL();
            var result = db.StaffPlan_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        //[Permission(TableID = (int)ETable.StaffPlan, TypeAction = (int)EAction.Get)]

        public ActionResult Staff_GetALL()
        {
            var db = new StaffPlan_DAL();
            var result = db.Staff_GetALL();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.StaffPlan, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "StaffPlan_GetList")]

        public ActionResult StaffPlanExportExcel(string filter = "")
        {
            DataTable dt = new DataTable("Grid");
            if (filter.Contains("!!") == true)
            {
                filter = filter.Replace("!!", "%");
            }
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(AppRes.Employee),
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
            dt.Columns[4].DataType = typeof(string);
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

            var db = new StaffPlan_DAL();
            ToTalMonth ToTalMonth = new ToTalMonth();

            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.StaffPlan_GetList(1, 100000, filter, LanguageCode, out total, out ToTalMonth);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.StaffName,item.OrganizationUnitName, item.CurrencyName, item.Year,item.StatusName, item.M1, item.M2, item.M3, item.M4, item.M5, item.M6, item.M7, item.M8, item.M9, item.M10, item.M11, item.M12, item.SumValue);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", " StaffPlan.xlsx");
        }
    }
}