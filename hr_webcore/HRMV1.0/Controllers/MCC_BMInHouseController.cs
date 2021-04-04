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
using Core.Web.Enums;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class MCC_BMInHouseController : Controller
    {
        // GET: MCC_BMInHouse
        [Permission(TableID = (int)ETable.MCC_BMInHouse, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/MCC_BMInHouse/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.MCC_BMInHouse, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "MCC_BMInHouse_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new MCC_BMInHouse_DAL();
            int total = 0;
            var result = db.MCC_BMInHouse_GetList(pageIndex, pageSize, filter, out total);
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

        [Permission(TableID = (int)ETable.MCC_BMInHouse, TypeAction = (int)EAction.Get)]
        public ActionResult MCC_BMInHouseSave()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.MCC_BMInHouse, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "MCC_BMInHouse_Save")]
        public ActionResult MCC_BMInHouse_Save(MCC_BMInHouse data)
        {
            var db = new MCC_BMInHouse_DAL();
            var result = db.MCC_BMInHouse_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.MCC_BMInHouse, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "MCC_BMInHouse_Delete")]
        public ActionResult MCC_BMInHouse_Delete(int ID)
        {
            var db = new MCC_BMInHouse_DAL();
            var result = db.MCC_BMInHouse_Delete(ID);
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