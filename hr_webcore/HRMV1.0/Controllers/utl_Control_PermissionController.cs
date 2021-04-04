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
    public class utl_Control_PermissionController : Controller
    {
        // GET: utl_Control_Permission
        [Permission(TableID = (int)ETable.utl_Control_Permission, TypeAction = (int)EAction.Index)]

        public ActionResult Index()
        {
            ViewBag.url = "/utl_Control_Permission/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.utl_Control_Permission, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "utl_Control_Permission_GetList")]


        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new utl_Control_Permission_DAL();
            int total = 0;
            int Language = Global.CurrentLanguage;
            var result = db.utl_Control_Permission_GetList(pageIndex, pageSize, filter, out total, Language);
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
        public ActionResult Save_utl_Control_Permission()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.utl_Control_Permission, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "utl_Control_Permission_Save")]

        public ActionResult utl_Control_Permission_Save(utl_Control_Permission data)
        {
            var db = new utl_Control_Permission_DAL();
            if (data.DisplayOrder == 0)
            {
                data.DisplayOrder = 1;
            }
            var result = db.utl_Control_Permission_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.utl_Control_Permission, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "utl_Control_Permission_Delete")]

        public ActionResult utl_Control_Permission_Delete(int ID)
        {
            var db = new utl_Control_Permission_DAL();
            var result = db.utl_Control_Permission_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.utl_Control_Permission, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "utl_Control_Permission_GetList")]

        public ActionResult utl_Control_PermissionExportExcel(string filter)
        {
            int Language = Global.CurrentLanguage;
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn(AppRes.TableName),
                new DataColumn(AppRes.ColumnName),
                new DataColumn(AppRes.Permission),
                new DataColumn(AppRes.CustomizeHTML),
                new DataColumn(AppRes.Data),
                new DataColumn(AppRes.isAction),             

            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(bool);
           


            var db = new utl_Control_Permission_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.utl_Control_Permission_GetList(1, 5000, filter, out total, Language);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.ColumnName, item.PermissionName, item.PermissionType, item.CustomHtml, item.DataCondition, item.IsActive);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "utl_Control_Permission.xlsx");
        }

    }
}