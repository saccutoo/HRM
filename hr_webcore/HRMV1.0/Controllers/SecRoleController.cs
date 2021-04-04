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
    public class SecRoleController : Controller
    {
        // GET: SecRole
        [Permission(TableID = (int)ETable.Sec_Role, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/SecRole/TableServerSideGetData";
            return PartialView();
        }


        [Permission(TableID = (int)ETable.Sec_Role, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "sec_Role_List")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Sec_RoleDAL();
            int total = 0;
            var result = db.GetSecRole(pageIndex, pageSize, filter, out total);
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


        [Permission(TableID = (int)ETable.Sec_Role, TypeAction = (int)EAction.Index)]
        public ActionResult TableServerSide()
        {
            return PartialView();
        }

     
        public ActionResult SaveSecRole()
        {
            return PartialView();
        }


        [Permission(TableID = (int)ETable.Sec_Role, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "sec_Role_GetByID")]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new Sec_RoleDAL();
            var result = db.GetSecRoleById(1, idTable, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Sec_Role, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "SecRole_Save")]

        public ActionResult _SaveSecRole(Sec_Role secRole)
        {
            var db = new Sec_RoleDAL();

            var result = db.SaveSecRole(1, 1, secRole);

            if (result.IsSuccess == true && secRole.RoleID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && secRole.RoleID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.Sec_Role, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "sec_Role_Delete")]

        public ActionResult _DeleteSecRole(int id, int idTable)
        {
            var db = new Sec_RoleDAL();
            var result = db.DeleteSecRole(1, idTable, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.Sec_Role, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "sec_Role_List")]

        public ActionResult SecRoleExportExcel(string filterString)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[3]
            {
                new DataColumn(AppRes.RoleId),
                new DataColumn(AppRes.NameVi),
                new DataColumn(AppRes.NameEn)
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            var db = new Sec_RoleDAL();
            int total = 0;
            var lstData = db.ExportExcelSecRole(filterString);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.RoleID, item.Name, item.NameEN);
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