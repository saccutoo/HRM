
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
using HRM.DataAccess.Entity.UserDefinedType;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class StaffPlanRenewalRateController : Controller
    {
        // GET: StaffPlanRenewalRate
        public ActionResult Index()
        {
            ViewBag.url = "/StaffPlanRenewalRate/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.StaffPlanRenewalRate, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "StaffPlanRenewalRate_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new StaffPlanRenewalRateDAL();
            int total = 0;
            TableColumnsTotal TableColumnsTotal = new TableColumnsTotal();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.StaffPlanRenewalRate_GetList(pageIndex, pageSize, filter, LanguageCode, out total,out TableColumnsTotal);

            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal,
                TotalQuarter = TableColumnsTotal

            }));
        }
        public ActionResult SaveStaffPlanRenewalRate()
        {
            return PartialView();

        }
        [Permission(TableID = (int)ETable.StaffPlanRenewalRate, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "StaffPlanRenewalRate_Save")]
        public ActionResult StaffPlanRenewalRate_Save(List<StaffPlanFundRate> data)
        {
            var db = new StaffPlanRenewalRateDAL();
            var result = db.StaffPlanRenewalRate_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }
        [Permission(TableID = (int)ETable.StaffPlanRenewalRate, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "StaffPlanRenewalRate_Delete")]
        public ActionResult StaffPlanRenewalRate_Delete(int ID)
        {
            var db = new StaffPlanRenewalRateDAL();
            var result = db.StaffPlanRenewalRate_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Delete_success;
            else
                result.Message = AppRes.MS_DeleteError;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}