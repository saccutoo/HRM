using ClosedXML.Excel;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Common;
using HRM.Security;
using static HRM.Constants.Constant;
using HRM.Logger;
using Core.Web.Enums;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class InsuranceController : Controller
    {
        // GET: Insurance
        [Permission(TableID = (int)ETable.Config_Insurance, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/Insurance/TableServerSideGetData";
            return View();
        }

        [Permission(TableID = (int)ETable.Config_Insurance, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Config_Insurance_list")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Config_InsuranceDal();
            int total = 0;
            var result = db.GetListInsurance(pageIndex, pageSize, filter,Global.CurrentLanguage, out total);
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

        public ActionResult AddInsurance()
        {
            return PartialView();
        }

        public ActionResult EditInsurance()
        {
            return PartialView();
        }

        public ActionResult SaveInsurance()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Config_Insurance, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Config_Insurance_GetByID")]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new Config_InsuranceDal();
            var result = db.GetInsuranceById(1, idTable, id);
         
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Config_Insurance, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Globallist_GetAll")]
        public ActionResult GetInsuranceType()
        {
            var db = new CommonDal();
            var result = db.GetsWhereParentIDnotTree(Constants.Constant.numGlobalListParent.Config_Insurance_Type.GetHashCode(), Global.CurrentUser.CurrentLanguageID);

            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Config_Insurance, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Globallist_GetAll")]
        public ActionResult GetInsuranceStatus()
        {
            var db = new CommonDal();
            var result = db.GetsWhereParentIDnotTree(Constants.Constant.numGlobalListParent.Config_Insurance_Status.GetHashCode(), Global.CurrentUser.CurrentLanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Config_Insurance, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "Config_Insurance_Save")]
        public ActionResult _EditInsurance(Config_InsuranceModel model)
        {
            var db = new Config_InsuranceDal();
            var entity = model.GetEntity();
            var result = db.UpdateInsuranceById(1, 1, entity.AutoID, entity);
            entity.AutoID = 0;
            _AddInsurance(entity);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID=(int)ETable.Config_Insurance,TypeAction =(int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "Config_Insurance_Save")]
        public ActionResult _AddInsurance(Config_Insurance insuranceVM)
        {
            var db = new Config_InsuranceDal();
            var result = db.AddInsurance(1, 1, insuranceVM);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Config_Insurance,TypeAction=(int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "SocialInsuranceDetail_GetSumRate")]
        public ActionResult GetSumRate()
        {
            var db = new Config_InsuranceDal();
            var result = db.GetSumRate();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Config_Insurance, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "Config_Insurance_Delete")]
        public ActionResult _DeleteInsurance(int id, int idTable)
        {
            var db = new Config_InsuranceDal();
            var result = db.DeleteInsurance(1, idTable, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Config_Insurance, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "Config_Insurance_list")]
        public ActionResult InsuranceExportExcel(string filterString)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn("InsuranceTypeName"),
                new DataColumn("DecisionCode"),
                new DataColumn("StatusName"),
                new DataColumn("RateCompany"),
                new DataColumn("RatePerson"),
                new DataColumn("Total")
            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(double);
            dt.Columns[4].DataType = typeof(double);
            dt.Columns[5].DataType = typeof(double);
            var db = new Config_InsuranceDal();
            int total = 0;
            var lstData = db.ExportExcelInsurance(filterString, Global.CurrentLanguage);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.InsuranceTypeName,item.DecisionCode,item.StatusName,item.RateCompany,item.RatePerson,item.Total);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Insurance.xlsx");
        }
    }
}