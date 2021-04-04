using ClosedXML.Excel;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Constants;
using HRM.Security;
using static HRM.Constants.Constant;
using HRM.Logger;
using Core.Web.Enums;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class GlobalListController : Controller
    {
        // GET: GlobalList
        [Permission(TableID = (int)ETable.GlobalList, TypeAction = (int)EAction.Index)]
        public ActionResult Index()

        {
            ViewBag.url = "/GlobalList/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.GlobalList, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Globallist_GetAll")]
        public ActionResult GetParent()
        {
            var db = new CommonDal();
            var result = db.GetsWhereParentID(Constant.numGlobalListParent.AP_Genaral.GetHashCode(), Global.CurrentUser.CurrentLanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.GlobalList, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Globallist_GetAll")]
        public ActionResult GetListGlobalistByParentId()
        {
            var db = new CommonDal();
            var result = db.GetsWhereParentID(Constant.numGlobalListParent.AP_Genaral.GetHashCode(), Global.CurrentUser.CurrentLanguageID);
            List<Department> result1 = result.Select(x => new Department { id = x.GlobalListID,label = x.DisplayName, Name = x.Name, NameEN = x.NameEN,ParentID = 0,Value = null, collapsed = false,children = new List<Department>()}).ToList();
            var list = JsonConvert.SerializeObject(result1,
              Formatting.None,
              new JsonSerializerSettings()
              {
                  ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
              });

            return Content(list, "application/json");
        }


        [Permission(TableID = (int)ETable.GlobalList, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "globallist_GetNameByUserID")]
        public ActionResult GetFullName()
        {
            var db = new GlobalDal();
            int userid = Global.CurrentUser.UserID;
            var result = db.GetNameByUserID(userid);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.GlobalList, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Globallist_GetAll")]
        public ActionResult GetCategoryList(int parameter)
        {
            var db = new CommonDal();
            var result = db.GetsWhereParentID(parameter, Global.CurrentUser.CurrentLanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.GlobalList, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "globallist_List")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new GlobalDal();
            int total = 0;
            var result = db.GetGlobalList(pageIndex, pageSize, filter, out total);
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

       
        public ActionResult AddGlobalList()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.GlobalList, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "globallist_GetByGlobalListID")]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new GlobalDal();
            var result = db.GetGlobalListById(1, idTable, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.GlobalList, TypeAction = (int)EAction.Add)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "globallist_Save")]
        public ActionResult _AddGlobalList(GlobalList obj)
        {
            var db = new GlobalDal();
            obj.CreatedBy = Global.CurrentUser.UserID;
            var result = db.AddGlobalList(1, 1, obj);
            if (result.IsSuccess == true && obj.GlobalListID > 0)
                result.Message = AppRes.MS_Update_success;
            else if (result.IsSuccess == true)
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.GlobalList, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "globallist_Detele")]
        public ActionResult _DeleteGlobalList(int id, int idTable)
        {
            var db = new GlobalDal();
            var result = db.DeleteGlobalList(1, idTable, id);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Delete_success;
            else
                result.Message = AppRes.NotFound;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.GlobalList, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "globallist_List")]
        public ActionResult GlobalListExportExcel(string filterString)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn(AppRes.ID),
                new DataColumn(AppRes.Name),
                new DataColumn(AppRes.NameEn),
                new DataColumn(AppRes.Status),
                new DataColumn(AppRes.Value),
                new DataColumn(AppRes.ValueEn),
                new DataColumn(AppRes.Note)
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(string);
            var db = new GlobalDal();
            int total = 0;
            var lstData = db.ExportExcelGlobalList(filterString);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.GlobalListID, item.Name, item.NameEN, item.IsActive1, item.Value, item.ValueEN, item.Descriptions);
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