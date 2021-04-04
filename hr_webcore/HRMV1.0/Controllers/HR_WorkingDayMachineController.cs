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
    public class HR_WorkingDayMachineController : Controller
    {
        // GET: Config_PersonalIncomeTax
        [Permission(TableID = (int)ETable.HR_WorkingDayMachine, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/HR_WorkingDayMachine/TableServerSideGetData";
            return PartialView();
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "HR_WorkingDayMachine_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new HR_WorkingDayMachine_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentLanguage;
            var result = db.HR_WorkingDayMachine_GetList(pageIndex, pageSize, filter, LanguageCode, out total);
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

        public ActionResult SaveHR_WorkingDayMachine()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayMachine, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "HR_WorkingDayMachine_Save")]
        public ActionResult HR_WorkingDayMachine_Save(HR_WorkingDayMachine data)
        {
            var db = new HR_WorkingDayMachine_DAL();
            var result = db.HR_WorkingDayMachine_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayMachine, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "HR_WorkingDayMachine_Delete")]
        public ActionResult HR_WorkingDayMachine_Delete(int ID)
        {
            var db = new HR_WorkingDayMachine_DAL();
            var result = db.HR_WorkingDayMachine_Delete(ID);
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