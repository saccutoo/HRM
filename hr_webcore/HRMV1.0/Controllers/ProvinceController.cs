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
    public class ProvinceController : Controller
    {
        // GET: Province
        [Permission(TableID = (int)ETable.Province, TypeAction = (int)EAction.Get)]
        public ActionResult Index()
        {
            ViewBag.url = "/Province/TableServerSideGetData";
            return PartialView();
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Province_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Province_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentLanguage;
            var result = db.Province_GetList(pageIndex, pageSize, filter, LanguageCode, out total);
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

        public ActionResult SaveProvince()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Province, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "Province_Save")]
        public ActionResult Province_Save(Province data)
        {
            var db = new Province_DAL();
            var result = db.Province_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Province, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "Province_Delete")]
        public ActionResult Province_Delete(int ID)
        {
            var db = new Province_DAL();
            var result = db.Province_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}