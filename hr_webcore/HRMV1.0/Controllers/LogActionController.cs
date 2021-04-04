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
    public class LogActionController : Controller
    {
        // GET: LogAction
        [Permission(TableID = (int)ETable.Log, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/LogAction/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.Log, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Log_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Log_DAL();
            int total = 0;
            int Language = Global.CurrentLanguage;
            var result = db.Log_GetList(pageIndex, pageSize, filter, out total);
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

    }
}