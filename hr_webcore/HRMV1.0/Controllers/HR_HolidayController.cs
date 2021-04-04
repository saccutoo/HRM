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
using Core.Web.Enums;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class HR_HolidayController : Controller
    {
        // GET: HR_Holiday
        [Permission(TableID = (int)ETable.HR_Holiday, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/HR_Holiday/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_Holiday, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetHR_Holiday")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new HR_HolidayDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.GetHR_Holiday(pageIndex, pageSize, filter, out total, LanguageCode);
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

        public ActionResult SaveHR_Holiday()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_Holiday, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Get_HR_WorkingDayMachine")]
        public ActionResult Get_HR_WorkingDayMachine()
        {
            var db = new HR_HolidayDAL();
            var result = db.Get_HR_WorkingDayMachine();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_Holiday, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetHR_HolidayByAutoID")]
        public ActionResult GetHR_HolidayByAutoID(int id)
        {
            var db = new HR_HolidayDAL();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.GetHR_HolidayByAutoID(id, LanguageCode);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_Holiday, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "SaveHR_Holiday")]
        public ActionResult _SaveHR_Holiday(HR_Holiday HR_Holiday)
        {
            var db = new HR_HolidayDAL();

            var result = db.SaveHR_Holiday(HR_Holiday);

            if (result.IsSuccess == true && HR_Holiday.HolidayID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && HR_Holiday.HolidayID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_Holiday, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "DeleteHR_Holiday")]
        public ActionResult DeleteHR_Holiday(int id, int idTable)
        {
            var db = new HR_HolidayDAL();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.DeleteHR_Holiday(1, idTable, id, LanguageCode);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_Holiday, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "GetHR_Holiday")]
        public ActionResult ExportExcelHR_Holiday(string filterString)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn(AppRes.HolidayID),
                new DataColumn(AppRes.StartDate),
                new DataColumn(AppRes.EndDate),
                new DataColumn(AppRes.ContactTypeID),
                new DataColumn(AppRes.Note),
                new DataColumn(AppRes.WorkingDayMachineName)
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(DateTime);
            dt.Columns[2].DataType = typeof(DateTime);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            var db = new HR_HolidayDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.GetHR_Holiday(1, 50000, filterString, out total, LanguageCode);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.HolidayID, item.FromDate, item.ToDate,
                    item.TypeName, item.Note, item.WorkingDayMachineName);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HR_Holiday.xlsx");
        }
    }
}