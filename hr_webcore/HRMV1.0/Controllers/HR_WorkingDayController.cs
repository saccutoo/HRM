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
    public class HR_WorkingDayController : Controller
    {
        // GET: HR_WorkingDay
        [Permission(TableID = (int)ETable.HR_WorkingDay, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/HR_WorkingDay/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_WorkingDay, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetHR_WorkingDay")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new HR_WorkingDayDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.GetHR_WorkingDay(pageIndex, pageSize, filter, out total, LanguageCode);
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

        public ActionResult SaveHR_WorkingDay()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_WorkingDay, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Get_HR_WorkingDayMachine")]
        public ActionResult Get_HR_WorkingDayMachine()
        {
            var db = new HR_WorkingDayDAL();
            var result = db.Get_HR_WorkingDayMachine();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDay, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetHR_WorkingDayByAutoID")]
        public ActionResult GetHR_WorkingDayByAutoID(int id)
        {
            var db = new HR_WorkingDayDAL();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.GetHR_WorkingDayByAutoID(id, LanguageCode);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDay, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "SaveHR_WorkingDay")]
        public ActionResult _SaveHR_WorkingDay(HR_WorkingDay HR_WorkingDay)
        {
            var db = new HR_WorkingDayDAL();

            var result = db.SaveHR_WorkingDay(HR_WorkingDay);

            if (result.IsSuccess == true && HR_WorkingDay.WorkingDayID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && HR_WorkingDay.WorkingDayID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDay, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "DeleteHR_WorkingDay")]
        public ActionResult DeleteHR_WorkingDay(int id, int idTable)
        {
            var db = new HR_WorkingDayDAL();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.DeleteHR_WorkingDay(1, idTable, id, LanguageCode);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDay, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "GetHR_WorkingDay")]
        public ActionResult ExportExcelHR_WorkingDay(string filterString)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[10]
            {
                new DataColumn(AppRes.WorkingDayID),
                new DataColumn(AppRes.WorkingDayMachineName),
                new DataColumn(AppRes.MorningHourStart),
                new DataColumn(AppRes.MorningHourMid),
                new DataColumn(AppRes.MorningHourEnd),
                new DataColumn(AppRes.AfernoonHourStart),
                new DataColumn(AppRes.AfernoonHourMid),
                new DataColumn(AppRes.AfternoonHourEnd),
                new DataColumn(AppRes.StartDate),
                new DataColumn(AppRes.EndDate)
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(string);
            dt.Columns[7].DataType = typeof(string);
            dt.Columns[8].DataType = typeof(DateTime);
            dt.Columns[9].DataType = typeof(DateTime);
            var db = new HR_WorkingDayDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.GetHR_WorkingDay(1, 100, filterString, out total, LanguageCode);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.WorkingDayID, item.WorkingDayMachineName, item.MorningHourStart,
                    item.MorningHourMid, item.MorningHourEnd, item.AfernoonHourStart, item.AfernoonHourMid, item.AfternoonHourEnd,
                    item.StartDate, item.EndDate);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SecRole.xlsx");
        }
    }
}