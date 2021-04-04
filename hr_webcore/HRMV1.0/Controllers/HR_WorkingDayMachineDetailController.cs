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
    public class HR_WorkingDayMachineDetailController : Controller
    {
        // GET: HR_WorkingDayMachineDetail
        [Permission(TableID = (int)ETable.HR_WorkingDayMachineDetail, TypeAction = (int)EAction.Get)]
        public ActionResult Index()
        {
            ViewBag.url = "/HR_WorkingDayMachineDetail/TableServerSideGetData";
            return PartialView();
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "HR_WorkingDayMachineDetail_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new HR_WorkingDayMachineDetail_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentLanguage;
            var result = db.HR_WorkingDayMachineDetail_GetList(pageIndex, pageSize, filter, LanguageCode, out total);
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

        public ActionResult SaveHR_WorkingDayMachineDetail()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayMachineDetail, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "HR_WorkingDayMachineDetail_Save")]
        public ActionResult HR_WorkingDayMachineDetail_Save(HR_WorkingDayMarchineDetail data)
        {
            //return View();
            var db = new HR_WorkingDayMachineDetail_DAL();
            var result = db.HR_WorkingDayMachineDetail_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayMachineDetail, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "HR_WorkingDayMachineDetail_Delete")]
        public ActionResult HR_WorkingDayMachineDetail_Delete(int ID)
        {
            var db = new HR_WorkingDayMachineDetail_DAL();
            var result = db.HR_WorkingDayMachineDetail_Delete(ID);
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