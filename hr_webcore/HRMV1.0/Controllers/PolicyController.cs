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
    public class PolicyController : Controller
    {
        // GET: Policy
        [Permission(TableID = (int)ETable.Policy, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/Policy/TableServerSideGetData";
            ViewBag.url1 = "/PolicyAllowance/TableServerSideGetData";
            ViewBag.url2 = "/Policy/SpendingAdjustmentRateGetData";

            return PartialView();
        }

        [Permission(TableID = (int)ETable.Policy, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Policy_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Policy_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.Policy_GetList(pageIndex, pageSize, filter, LanguageCode, out total);
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

        public ActionResult SavePolicy()
        {
            return PartialView();

        }

        [Permission(TableID = (int)ETable.Policy, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "PolicyAllowance_Save")]
        public ActionResult Policy_Save(Policy data,PolicyAllowance [] List,int [] RemoveListPolicyAllowance)
        {
            //return View();
            var db = new Policy_DAL();
            var db1 = new PolicyAllowance_DAL();
            int PolicyID = 0;
            data.CreatedOn = Global.CurrentUser.UserID;
            #region
            var result = db.Policy_Save(data);
            if (List!=null)
            {
                for (int i = 0; i < List.Length; i++)
                {
                    if (result.Message!="0")
                    {                       
                        PolicyID = Convert.ToInt32(result.Message);
                        var AutoID = List[i].AutoID;
                        var AllowanceID = List[i].AllowanceID;
                        var savePolicyAllowance = db1.PolicyAllowance_Save(AutoID, PolicyID, AllowanceID);
                    }
                    else
                    {
                        PolicyID = data.PolicyID;
                        var AutoID = List[i].AutoID;
                        var AllowanceID = List[i].AllowanceID;
                        var savePolicyAllowance = db1.PolicyAllowance_Save(AutoID, PolicyID, AllowanceID);
                    }
                }
            }          

            if (RemoveListPolicyAllowance != null)
            {
                for (int i = 0; i < RemoveListPolicyAllowance.Length; i++)
                {
                    var x = RemoveListPolicyAllowance[i];
                    var DeletePolicyAllowance = db1.PolicyAllowance_Delete(x);
                }

            }
            if (data.ListKPI != null && data.ListKPI.Length > 0)
            {
                data.KPI = "";
                for (int i = 0; i < data.ListKPI.Length; i++)
                {
                    if (data.KPI!="")
                    {
                        data.KPI += "," + data.ListKPI[i];
                    }
                    else
                    {
                        data.KPI +=data.ListKPI[i];
                    }
                }
                if (result.Message != "0")
                {
                    PolicyID = Convert.ToInt32(result.Message);
                    var savePolicyKpi = db.PolicyKpi_Save(PolicyID, data.KPI);
                }
                else
                {
                    var savePolicyKpi = db.PolicyKpi_Save(data.PolicyID, data.KPI);
                }
                    
            }
            #endregion
           
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }

        [Permission(TableID = (int)ETable.Policy, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "Policy_Delete")]
        public ActionResult Policy_Delete(int ID)
        {
            var db = new Policy_DAL();
            var result = db.Policy_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.Policy, TypeAction = (int)EAction.Get)]
        public ActionResult ListKpiByPolicyID(int PolicyID)
        {
            var db = new Policy_DAL();
            var result = db.PolicyKpi_Get_ByPolicyID(PolicyID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult CheckPolicyName(Policy data)
        {
            var db = new Policy_DAL();
            data.CreatedOn = Global.CurrentUser.UserID;
            var result = db.CheckPolicyName(data);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "SpendingAdjustmentRate_GetList")]
        public ActionResult SpendingAdjustmentRateGetData(int policyId)
        {
            var db = new PolicyDetail_DAL();
            var result = db.GetSpendingAdjustmentRateByPolicyId(policyId);
            return Content(JsonConvert.SerializeObject(new
            {
                ListSpending = result
            }));
        }
    }

}