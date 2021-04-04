using ClosedXML.Excel;
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
    public class SecControllerController : Controller
    {
        // GET: SecController

        [Permission(TableID = (int)ETable.Sec_Controller, TypeAction = (int)EAction.Index)]
        public ActionResult Index()

        {
            ViewBag.url = "/SecController/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.Sec_Controller, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "sec_Controller_List")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Sec_ControllerDal();
            int total = 0;
            var result = db.GetSecController(pageIndex, pageSize, filter, out total);
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

        public ActionResult AddSecContrl()
        {
            return PartialView();
        }

        public ActionResult EditSecContrl()
        {
            return PartialView();
        }


        [Permission(TableID = (int)ETable.Sec_Controller, TypeAction = (int)EAction.Get)]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new Sec_ControllerDal();
            var result = db.GetSecContrlById(1, idTable, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.Sec_Controller, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "sec_Controller_GetByID")]

        public ActionResult _EditSecContrl(Sec_Controller secControl)
        {
            var db = new Sec_ControllerDal();
            var result = db.UpdateSecContrlById(1, 1, secControl.ControllerID, secControl);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.Sec_Controller, TypeAction = (int)EAction.Add)]
        [WriteLog(Action = EAction.Add, LogStoreProcedure = "sec_Controller_Insert")]

        public ActionResult _AddSecContrl(string ControllerName)
        {
            var db = new Sec_ControllerDal();
            Sec_Controller secController = new Sec_Controller();
            secController.ControllerName = ControllerName;
            var result = db.AddSecContrl(1, 1, secController);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.Sec_Controller, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "sec_Controller_Delete")]

        public ActionResult _DeleteSecContrl(int id, int idTable)
        {
            var db = new Sec_ControllerDal();
            var result = db.DeleteSecContrl(1, idTable, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.Sec_Controller, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "sec_Controller_List")]

        public ActionResult SecContrlExportExcel(string filterString)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[2]
            {
                new DataColumn("ControllerID"),
                new DataColumn("ControllerName")
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(string);
            var db = new Sec_ControllerDal();
            int total = 0;
            var lstData = db.ExportExcelSecContrl(filterString);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.ControllerID, item.ControllerName);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SecController.xlsx");
        }
    }
}