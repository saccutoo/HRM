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

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class Sec_StaffMarginLevelController : Controller
    {
        // GET: Sec_StaffMarginLevel
        [Permission(TableID = (int)ETable.Sec_StaffMarginLevel, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/Sec_StaffMarginLevel/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.Sec_StaffMarginLevel, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetSec_StaffMarginLevel")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Sec_StaffMarginLevelDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.GetSec_StaffMarginLevel(pageIndex, pageSize, filter, out total, LanguageCode);
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

        public ActionResult SaveSec_StaffMarginLevel()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.Sec_StaffMarginLevel, TypeAction = (int)EAction.Get)]
        public ActionResult GetSec_StaffMarginLevelByAutoID(int id)
        {
            var db = new Sec_StaffMarginLevelDAL();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.GetSec_StaffMarginLevelByAutoID(id, LanguageCode);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.Sec_StaffMarginLevel, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "SaveSec_StaffMarginLevel")]

        public ActionResult _SaveSec_StaffMarginLevel(Sec_StaffMarginLevel Sec_StaffMarginLevel)
        {
            var db = new Sec_StaffMarginLevelDAL();

            var result = db.SaveSec_StaffMarginLevel(Sec_StaffMarginLevel);

            if (result.IsSuccess == true && Sec_StaffMarginLevel.AutoID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && Sec_StaffMarginLevel.AutoID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.Sec_StaffMarginLevel, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "DeleteSec_StaffMarginLevel")]

        public ActionResult DeleteSec_StaffMarginLevel(int id, int idTable)
        {
            var db = new Sec_StaffMarginLevelDAL();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.DeleteSec_StaffMarginLevel(1, idTable, id, LanguageCode);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.Sec_StaffMarginLevel, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "GetSec_StaffMarginLevel")]

        public ActionResult ExportExcelSec_StaffMarginLevel(string filterString)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8]
            {
                new DataColumn(AppRes.StaffLevelID),
                new DataColumn(AppRes.MinMargin),
                new DataColumn(AppRes.MaxMargin),
                new DataColumn(AppRes.CreatedOn),
                new DataColumn(AppRes.CreatedBy),
                new DataColumn(AppRes.Status),
                new DataColumn(AppRes.StartDate),
                new DataColumn(AppRes.EndDate)
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(DateTime);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(DateTime);
            dt.Columns[7].DataType = typeof(DateTime);
            var db = new Sec_StaffMarginLevelDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.GetSec_StaffMarginLevel(1, 100, filterString, out total, LanguageCode);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.StaffLevelID, item.MinMarginMoney, item.MaxMarginMoney, 
                    item.CreatedOn, item.Fullname, item.StatusName, item.StartDate, item.EndDate);
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