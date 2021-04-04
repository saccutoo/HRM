using ClosedXML.Excel;
using HRM.App_LocalResources;
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
using HRM.Security;
using static HRM.Constants.Constant;
using HRM.Logger;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class Sys_table_Column_RoleController : Controller
    {
        // GET: Sys_table_Column_Role
        [Permission(TableID = (int)ETable.Sys_table_Column_Role, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/SecRole/TableServerSideGetData";
            ViewBag.url1 = "/Sys_table_Column_Role/TableServerSideGetData";
            return View();
        }

        [Permission(TableID = (int)ETable.Sys_table_Column_Role, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Sys_table_column_Role_list")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Sys_Table_Column_DAL();
            int total = 0;
            var result = db.Get_SyS_Table_CoLumn(pageIndex, pageSize, filter, out total);
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
        //insert
        //int RoleId,int TableColumnId
        //[Permission(TableID = (int)ETable.Sys_table_Column_Role, TypeAction = (int)EAction.Submit)]
        //[WriteLog(Action = EAction.Edit, LogStoreProcedure = "Sys_Table_Column_Role_Insert")]

        public ActionResult _Save_Sys_Table_Column_Role(List<Sys_Table_Column_Role> data)
        {
            var db = new Sys_Table_Column_DAL();
            var result = db.Save_Sys_Table_Column_Role(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }

        [Permission(TableID = (int)ETable.Sys_table_Column_Role, TypeAction = (int)EAction.Get)]
        public ActionResult Get_Sys_Table_Colum_Role(int id, int idTable)
        {
            var db = new Sys_Table_Column_DAL();
            var result = db.Sys_Table_Column_Role_GetID(1, 28, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Sys_table_Column_Role, TypeAction = (int)EAction.Get)]
        public ActionResult getSysTableColumnID(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Sys_Table_Column_DAL();
            int total = 0;
            var result = db.Get_SyS_Table_CoLumn(pageIndex, pageSize, filter, out total);
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

        [Permission(TableID = (int)ETable.Sys_table_Column_Role, TypeAction = (int)EAction.Get)]
        public ActionResult GetAllSysTableColumnRole(int RoleId)
        {
            var db = new Sys_Table_Column_DAL();
            var result = db.GetAllSysTableColumnRole(RoleId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}