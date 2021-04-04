using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Logger;
using HRM.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using static HRM.Constants.Constant;

namespace HRM.Controllers
{
    public class StaffPlanFundRateController : Controller
    {
        // GET: StaffPlanFundRate
        [Permission(TableID = (int)ETable.StaffPlanFundRate, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/StaffPlanFundRate/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.StaffPlanFundRate, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "StaffPlanFundRate_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new StaffPlanFundRateDAL();
            int total = 0;
            TableColumnsTotal TableColumnsTotal = new TableColumnsTotal();
            int Languagecode = Global.CurrentLanguage;
            var result = db.StaffPlanFundRate_GetList(pageIndex, pageSize, filter, Languagecode, out total, out TableColumnsTotal);
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
        public ActionResult SaveStaffPlanFunRate()
        {
            return PartialView();

        }
        [Permission(TableID = (int)ETable.StaffPlanFundRate, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "StaffPlanFundRate_Save")]
        public ActionResult StaffPlanFundRate_Save(List<StaffPlanFundRate> data)
        {
            //return View();
            var db = new StaffPlanFundRateDAL();
            var result = db.StaffPlanFundRate_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }
         [Permission(TableID = (int)ETable.StaffPlanFundRate, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "StaffPlanFundRate_Delete")]
        public ActionResult StaffPlanFundRate_Delete(int ID)
        {
            var db = new StaffPlanFundRateDAL();
            var result = db.StaffPlanFundRate_Delete(ID);
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