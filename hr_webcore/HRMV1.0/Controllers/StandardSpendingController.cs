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

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class StandardSpendingController : Controller
    {
        // GET: StandardSpending
        [Permission(TableID = (int)ETable.StandardSpending, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/StandardSpending/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.StandardSpending, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "StandardSpending_GetList")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new StandardSpending_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentLanguage;
            var result = db.StandardSpending_GetList(pageIndex, pageSize, filter, LanguageCode, out total);
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
        public ActionResult SaveStandardSpending()
        {
            return PartialView();

        }
        [Permission(TableID = (int)ETable.StandardSpending, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "StandardSpending_Save")]

        public ActionResult StandardSpending_Save(StandardSpending Data)
        {
            Data.CreatedBy = Global.CurrentUser.UserID;
            Data.ModifiedBy = Global.CurrentUser.UserID;
            Data.CreatedDate = DateTime.Now;
            Data.ModifiedDate = DateTime.Now;
            var db = new StandardSpending_DAL();
            var result = db.StandardSpending_Save(Data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }
        [Permission(TableID = (int)ETable.StandardSpending, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete,LogStoreProcedure = "StandardSpending_Delete")]

        public ActionResult StandardSpending_Delete(int ID)
        {
            var db = new StandardSpending_DAL();
            var result = db.StandardSpending_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }
        [Permission(TableID = (int)ETable.StandardSpending, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "StandardSpending_GetList")]

        public ActionResult StandardSpendingExportExcel(string filter)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[10]
            {
                new DataColumn(AppRes.StaffLevelID),
                new DataColumn(AppRes.StandardSpending),
                new DataColumn(AppRes.Policy),
                new DataColumn(AppRes.MinSpending),
                new DataColumn(AppRes.MinPerson),
                new DataColumn(AppRes.R_StartDate),
                new DataColumn(AppRes.EndDate),
                new DataColumn(AppRes.CreatedDate),
                new DataColumn(AppRes.ModifiedDate),
                new DataColumn(AppRes.Promotion_Status),

            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(double);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(double);
            dt.Columns[4].DataType = typeof(int);
            dt.Columns[5].DataType = typeof(DateTime);
            dt.Columns[6].DataType = typeof(DateTime);
            dt.Columns[7].DataType = typeof(string);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(string);


            var db = new StandardSpending_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.StandardSpending_GetList(1, 5000, filter,LanguageCode, out total);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.StaffLevelName, item.StandardSpendingAmount, item.Name, item.MinSpending, item.MinPerson, item.StartDate, item.EndDate, item.CreatedDate == null ? "" : item.CreatedDate.Value.ToString("dd/MM/yyyy"), item.ModifiedDate == null ? "" : item.ModifiedDate.Value.ToString("dd/MM/yyyy"), item.StatusName);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StandardSpending.xlsx");
        }
    }

}