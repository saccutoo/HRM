using ClosedXML.Excel;
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
    public class InsurancePositionController : Controller
    {
        // GET: InsurancePosition

        [Permission(TableID = (int)ETable.Insurance_Position, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/InsurancePosition/TableServerSideGetData";
            return View();
        }

        [Permission(TableID = (int)ETable.Insurance_Position, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Config_Insurance_Position_List")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new InsurancePositionDal();
            int total = 0;
            var result = db.GetListInsurancePosition(pageIndex, pageSize, filter, out total);
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

        public ActionResult AddInsurancePosition()
        {
            return PartialView();
        }

        public ActionResult EditInsurancePosition()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Insurance_Position, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Config_Insurance_Position_GetByID")]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new InsurancePositionDal();
            var result = db.GetInsurancePositionById(1, idTable, id);

            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Insurance_Position, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Globallist_GetAll")]
        public ActionResult GetListPositionName()
        {
            var db = new CommonDal();
            var result = db.GetsWhereParentIDnotTree(20, Global.CurrentUser.CurrentLanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Insurance_Position, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "Config_Insurance_Position_Save")]
        public ActionResult _EditInsurancePosition(Config_Insurance_PositionModel model)
        {
            var db = new InsurancePositionDal();
            var entity = model.GetEntity();
            var result = db.UpdateInsurancePositionById(1, 1, entity.AutoID, entity);
            entity.AutoID = 0;
            _AddInsurancePosition(entity);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Insurance_Position, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Add, LogStoreProcedure = "Config_Insurance_Position_Save")]
        public ActionResult _AddInsurancePosition(Config_Insurance_Position insurancePosition)
        {
            var db = new InsurancePositionDal();
            var result = db.AddInsurancePosition(1, 1, insurancePosition);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Insurance_Position, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "Config_Insurance_Position_Delete")]
        public ActionResult _DeleteInsurancePosition(int id, int idTable)
        {
            var db = new InsurancePositionDal();
            var result = db.DeleteInsurancePosition(1, idTable, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        //public ActionResult InsuranceExportExcel(string filterString)
        //{
        //    DataTable dt = new DataTable("Grid");
        //    dt.Columns.AddRange(new DataColumn[6]
        //    {
        //        new DataColumn("InsuranceTypeName"),
        //        new DataColumn("DecisionCode"),
        //        new DataColumn("StatusName"),
        //        new DataColumn("RateCompany"),
        //        new DataColumn("RatePerson"),
        //        new DataColumn("Total")
        //    });
        //    dt.Columns[0].DataType = typeof(string);
        //    dt.Columns[1].DataType = typeof(string);
        //    dt.Columns[2].DataType = typeof(string);
        //    dt.Columns[3].DataType = typeof(double);
        //    dt.Columns[4].DataType = typeof(double);
        //    dt.Columns[5].DataType = typeof(double);
        //    var db = new InsurancePositionDal();
        //    int total = 0;
        //    var lstData = db.ExportExcelInsurancePosition(filterString);
        //    foreach (var item in lstData)
        //    {
        //        dt.Rows.Add(item.InsuranceTypeName, item.DecisionCode, item.StatusName, item.RateCompany, item.RatePerson, item.Total);
        //    }

        //    var wb = new XLWorkbook();
        //    wb.Worksheets.Add(dt);
        //    byte[] data = null;
        //    using (var stream = new MemoryStream())
        //    {
        //        wb.SaveAs(stream);
        //        data = stream.ToArray();
        //    }
        //    return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "InsurancePosition.xlsx");
        //}
    }
}