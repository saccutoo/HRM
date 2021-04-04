﻿using ClosedXML.Excel;
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
    public class utl_Role_PermissionController : Controller
    {
        // GET: utl_Role_Permission
        //[Permission(TableID = (int)ETable.utl_Role_Permission, TypeAction = (int)EAction.Index)]

        public ActionResult Index()
        {
            ViewBag.url = "/utl_Role_Permission/TableServerSideGetData";
            return PartialView();
        }
        //[Permission(TableID = (int)ETable.utl_Role_Permission, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "utl_Role_Permission_GetList")]


        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new utl_Role_Permission_DAL();
            int total = 0;
            int Language = Global.CurrentLanguage;
            var result = db.utl_Role_Permission_GetList(pageIndex, pageSize, filter, out total, Language);
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
        public ActionResult Save_utl_Role_Permission()
        {
            return PartialView();
        }
        //[Permission(TableID = (int)ETable.utl_Role_Permission, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "utl_Role_Permission_Save")]

        public ActionResult utl_Role_Permission_Save(utl_Role_Permission data)
        {
            var db = new utl_Role_Permission_DAL();
            if (data.PermissionType==null)
            {
                data.PermissionType = "Role";
            }
            var result = db.utl_Role_Permission_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        //[Permission(TableID = (int)ETable.utl_Role_Permission, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "utl_Role_Permission_Delete")]

        public ActionResult utl_Role_Permission_Delete(int ID)
        {
            var db = new utl_Role_Permission_DAL();
            var result = db.utl_Role_Permission_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        //[Permission(TableID = (int)ETable.utl_Role_Permission, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "utl_Role_Permission_GetList")]

        public ActionResult utl_Role_PermissionExportExcel(string filter)
        {
            int Language = Global.CurrentLanguage;
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5]
            {
                new DataColumn(AppRes.TableName),
                new DataColumn(AppRes.Permission),
                new DataColumn(AppRes.User),
                new DataColumn(AppRes.Condition),
                new DataColumn(AppRes.Delim),               

            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);


            var db = new utl_Role_Permission_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.utl_Role_Permission_GetList(1, 5000, filter, out total, Language);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.GridName,item.PermissionType ,item.PermissionName, item.Condition, item.Delim);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", " utl_Role_Permission.xlsx");
        }

    }
}