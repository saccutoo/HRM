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
    public class HR_WorkingDayConfigController : Controller
    {
        // GET: HR_WorkingDayConfig
        [Permission(TableID = (int)ETable.HR_WorkingDayConfig, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/HR_WorkingDayConfig/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayConfig, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetHR_WorkingDayConfig")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new HR_WorkingDayConfigDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.GetHR_WorkingDayConfig(pageIndex, pageSize, filter, out total, LanguageCode);
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

        public ActionResult SaveHR_WorkingDayConfig()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayConfig, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Get_HR_WorkingDayMachine")]
        public ActionResult Get_HR_WorkingDayMachine()
        {
            var db = new HR_WorkingDayConfigDAL();
            var result = db.Get_HR_WorkingDayMachine();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayConfig, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetHR_WorkingDayConfigByAutoID")]
        public ActionResult GetHR_WorkingDayConfigByAutoID(int id)
        {
            var db = new HR_WorkingDayConfigDAL();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.GetHR_WorkingDayConfigByAutoID(id, LanguageCode);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayConfig, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "SaveHR_WorkingDayConfig")]
        public ActionResult _SaveHR_WorkingDayConfig(HR_WorkingDayConfig HR_WorkingDayConfig)
        {
            var db = new HR_WorkingDayConfigDAL();

            var result = db.SaveHR_WorkingDayConfig(HR_WorkingDayConfig);

            if (result.IsSuccess == true && HR_WorkingDayConfig.AutoID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && HR_WorkingDayConfig.AutoID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayConfig, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "DeleteHR_WorkingDayConfig")]
        public ActionResult DeleteHR_WorkingDayConfig(int id, int idTable)
        {
            var db = new HR_WorkingDayConfigDAL();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.DeleteHR_WorkingDayConfig(1, idTable, id, LanguageCode);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayConfig, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "GetHR_WorkingDayConfig")]
        public ActionResult ExportExcelHR_WorkingDayConfig(string filterString)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn(AppRes.AutoID),
                new DataColumn(AppRes.DateFromNumber),
                new DataColumn(AppRes.DateToNumber),
                new DataColumn(AppRes.WorkingDayMachineName),
                new DataColumn(AppRes.StartMonth),
                new DataColumn(AppRes.EndMonth),
                new DataColumn(AppRes.NoTimeChecking)
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(int);
            dt.Columns[2].DataType = typeof(int);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(DateTime);
            dt.Columns[5].DataType = typeof(DateTime);
            dt.Columns[6].DataType = typeof(bool);
            var db = new HR_WorkingDayConfigDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.GetHR_WorkingDayConfig(1, 100, filterString, out total, LanguageCode);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.AutoID, item.DateFromNumber, item.DateToNumber,
                    item.WorkingDayMachineName, item.StartMonth, item.EndMonth, item.NoTimeChecking);
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