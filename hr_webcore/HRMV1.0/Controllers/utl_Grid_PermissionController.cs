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
    public class utl_Grid_PermissionController : Controller
    {

        [Permission(TableID = (int)ETable.utl_Grid_Permission, TypeAction = (int)EAction.Index)]

        // GET: utl_Grid_Permission
        public ActionResult Index()
        {
            ViewBag.url = "/utl_Grid_Permission/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.utl_Grid_Permission, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "utl_Grid_Permission_GetList")]


        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new utl_Grid_Permission_DAL();
            int total = 0;
            int Language = Global.CurrentLanguage;
            var result = db.utl_Grid_Permission_GetList(pageIndex, pageSize, filter, out total, Language);
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
        public ActionResult Save_utl_Grid_Permission()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.utl_Grid_Permission, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "utl_Grid_Permission_Save")]

        public ActionResult utl_Grid_Permission_Save(utl_Grid_Permission data)
        {
            var db = new utl_Grid_Permission_DAL();           
            var result = db.utl_Grid_Permission_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.utl_Grid_Permission, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "utl_Grid_Permission_Delete")]

        public ActionResult utl_Grid_Permission_Delete(int ID)
        {
            var db = new utl_Grid_Permission_DAL();
            var result = db.utl_Grid_Permission_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.utl_Grid_Permission, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "utl_Grid_Permission_GetList")]

        public ActionResult utl_Grid_PermissionExportExcel(string filter)
        {
            int Language = Global.CurrentLanguage;
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[13]
            {
                new DataColumn(AppRes.TableName),
                new DataColumn(AppRes.Permission),
                new DataColumn(AppRes.IsAdd),
                new DataColumn(AppRes.IsEdit),
                new DataColumn(AppRes.Timekeeping_DelTitle),
                new DataColumn(AppRes.isAction),
                new DataColumn(AppRes.Search),
                new DataColumn(AppRes.btnExcel),
                new DataColumn(AppRes.btnImportExcel),
                new DataColumn(AppRes.Timekeeping_SubmitTitle),
                new DataColumn(AppRes.IsApproval),
                new DataColumn(AppRes.IsDisApproval),
                new DataColumn(AppRes.btnCopy),

            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(bool);
            dt.Columns[3].DataType = typeof(bool);
            dt.Columns[4].DataType = typeof(bool);
            dt.Columns[5].DataType = typeof(bool);
            dt.Columns[6].DataType = typeof(bool);
            dt.Columns[7].DataType = typeof(bool);
            dt.Columns[8].DataType = typeof(bool);
            dt.Columns[9].DataType = typeof(bool);
            dt.Columns[10].DataType = typeof(bool);
            dt.Columns[11].DataType = typeof(bool);
            dt.Columns[12].DataType = typeof(bool);


            var db = new utl_Grid_Permission_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.utl_Grid_Permission_GetList(1, 5000, filter, out total, Language);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.GridName, item.PermissionName, item.IsAdd, item.IsEdit, item.IsDelete, item.IsActive,item.IsFilterButton,
                    item.IsExportExcel,item.IsImportExcel,item.IsSubmit,item.IsApproval,item.IsDisApproval,item.IsCopy);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", " utl_Grid_Permission.xlsx");
        }

    }
}