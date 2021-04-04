using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Logger;
using HRM.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static HRM.Constants.Constant;
using System.Data;
using System;
using ClosedXML.Excel;
using System.IO;

namespace HRM.Controllers
{
    public class PolicyDetailController : Controller
    {
        // GET: PolicyDetail
        // GET: Policy
        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/PolicyDetail/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PolicyDetail_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new PolicyDetail_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.PolicyDetail_GetList(pageIndex, pageSize, filter, LanguageCode, out total);
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
        public ActionResult SavePolicyDetail()
        {
            return PartialView();

        }

        public ActionResult CheckSFormular()
        {
            return PartialView();
        }

        public ActionResult CopyPolicyDetail()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "PolicyDetail_Save")]
        public ActionResult PolicyDetail_Save(PolicyDetail data)
        {
            //return View();
            var db = new PolicyDetail_DAL();
            data.UserId = Global.CurrentUser.UserID;
            var result = db.PolicyDetail_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }

        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "PolicyDetail_Delete")]
        public ActionResult PolicyDetail_Delete(int ID)
        {
            var db = new PolicyDetail_DAL();
            var result = db.PolicyDetail_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Delete_success;
            else
                result.Message = AppRes.MS_DeleteError;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PolicyDetail_GetListFormularByStaffIdAndPolicyId")]
        public ActionResult PolicyDetail_GetListFormularByStaffIdAndPolicyId(int id,int staffId, int policyId)
        {
            var db = new PolicyDetail_DAL();
            var result = db.PolicyDetail_GetListFormularByStaffIdAndPolicyId(id,staffId, policyId);
            return Content(JsonConvert.SerializeObject(new
            {
                  result
            }));
        }
        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PolicyDetail_GetResultSFormular")]
        public ActionResult PolicyDetail_GetResultSFormular(string sFormularstr)
        {
            var db = new PolicyDetail_DAL();
            var result = db.PolicyDetail_GetResultSFormular(sFormularstr);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PolicyDetail_GetListKpicodeByPolicyId")]  
        public ActionResult PolicyDetail_GetListKpicodeByPolicyId(int policyId)
        {
            var db = new PolicyDetail_DAL();
            var result = db.PolicyDetail_GetListKpicodeByPolicyId(policyId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PolicyDetail_GetListFomularRunError")]
        public ActionResult PolicyDetail_GetListFomularRunError(string fomularStr)
        {
            var db = new PolicyDetail_DAL();
            string resultFomularError = string.Empty;

            var result = db.PolicyDetail_GetListFomularRunError(fomularStr, out  resultFomularError);
             return Content(JsonConvert.SerializeObject(new
            {
               resultFomular =  resultFomularError
            }));
        }

        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Get)]
        public ActionResult CheckFormular()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Get)]
        public ActionResult CheckFomularReturnExcel(string month, string year, string typeReward)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[42]
            {
                new DataColumn("Month"),
                new DataColumn("Year"),
                new DataColumn("Staff code"),
                new DataColumn("Full name"),
                new DataColumn("OrganizationUnitID"),
                new DataColumn("PolicyID"),
                new DataColumn("StaffLevelID"),
                new DataColumn("OfficePositionID"),
                new DataColumn("sFormular"),
                new DataColumn("SFormularProbation"),
                new DataColumn("SFormularCompensation"),
                new DataColumn("SFormularAllowances"),
                new DataColumn("SFormularBonus"),
                new DataColumn("Margincompensation"),
                new DataColumn("StandardSpendingAmount"),
                new DataColumn("StandardProbation"),
                new DataColumn("Amount"),
                new DataColumn("AmountProbation"),
                new DataColumn("AmountCompensation"),
                new DataColumn("AmountAllowances"),
                new DataColumn("AmountBonus"),
                new DataColumn("Quota"),
                new DataColumn("QuotaTV"),
                new DataColumn("Per"),
                new DataColumn("Standardworkingday"),
                new DataColumn("Working Day"),
                new DataColumn("WorkingdayTV"),
                new DataColumn("WorkingdayGHTV"),
                new DataColumn("Margin P"),
                new DataColumn("Margin MN"),
                new DataColumn("Margin BDT"),
                new DataColumn("Run Rate"),
                new DataColumn("CTTTMN"),
                new DataColumn("CTTTP"),
                new DataColumn("Fund DIF"),
                new DataColumn("Margin DMN"),
                new DataColumn("Renewal DIF"),
                new DataColumn("Renewal Rate"),
                new DataColumn("Margin PNew"),
                new DataColumn("Margin MNNew"),
                new DataColumn("Passrate"),
                new DataColumn("Note")

            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(int);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(int);
            dt.Columns[5].DataType = typeof(int);
            dt.Columns[6].DataType = typeof(int);
            dt.Columns[7].DataType = typeof(int);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(string);
            dt.Columns[10].DataType = typeof(string);
            dt.Columns[11].DataType = typeof(string);
            dt.Columns[12].DataType = typeof(string);
            dt.Columns[13].DataType = typeof(decimal);
            dt.Columns[14].DataType = typeof(decimal);
            dt.Columns[15].DataType = typeof(decimal);
            dt.Columns[16].DataType = typeof(decimal);
            dt.Columns[17].DataType = typeof(decimal);
            dt.Columns[18].DataType = typeof(decimal);
            dt.Columns[19].DataType = typeof(decimal);
            dt.Columns[20].DataType = typeof(decimal);
            dt.Columns[21].DataType = typeof(decimal);
            dt.Columns[22].DataType = typeof(decimal);
            dt.Columns[23].DataType = typeof(decimal);
            dt.Columns[24].DataType = typeof(decimal);
            dt.Columns[25].DataType = typeof(decimal);
            dt.Columns[26].DataType = typeof(decimal);
            dt.Columns[27].DataType = typeof(decimal);
            dt.Columns[28].DataType = typeof(decimal);
            dt.Columns[29].DataType = typeof(decimal);
            dt.Columns[30].DataType = typeof(decimal);
            dt.Columns[31].DataType = typeof(decimal);
            dt.Columns[32].DataType = typeof(decimal);
            dt.Columns[33].DataType = typeof(decimal);
            dt.Columns[34].DataType = typeof(decimal);
            dt.Columns[35].DataType = typeof(decimal);
            dt.Columns[36].DataType = typeof(decimal);
            dt.Columns[37].DataType = typeof(decimal);
            dt.Columns[38].DataType = typeof(decimal);
            dt.Columns[39].DataType = typeof(decimal);
            dt.Columns[40].DataType = typeof(decimal);
            dt.Columns[41].DataType = typeof(string);
            var db = new PolicyDetail_DAL();   
            var lstData = db.CheckFomularReturnExcel(Int32.Parse(month), Int32.Parse(year), Int32.Parse(typeReward));
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.Month, item.Year, item.StaffCode, item.Fullname, item.OrganizationUnitID, item.PolicyID, item.StaffLevelID, item.OfficePositionID,
                   item.sFomular, item.SFomularProbation, item.SFomularCompensation, item.SFormularAllowances, item.SFormularBonus,
                   item.Margincompensation, item.StandardSpendingAmount, item.StandardProbation, item.Amount, item.AmountProbation, item.AmountCompensation, item.AmountAllowances,
                   item.AmountBonus, item.Quota, item.QuotaTV, item.Per, item.Standardworkingday, item.Workingday, item.WorkingdayTV, item.WorkingdayGHTV,
                   item.MarginP, item.MarginMN, item.MarginBDT, item.RunRate, item.CTTTMN, item.CTTTP,item.FundDIF, item.MarginDMN, item.RenewalDIF,
                   item.RenewalRate, item.MarginPNew, item.MarginMNNew, item.Passrate, item.Note == "Correct" ? string.Empty : item.Note);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ListFomular.xlsx");
        }

        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "PolicyDetail_CopyPolicyDetail")]
        public ActionResult PolicyDetail_CopyPolicyDetail(List<string> lstPolicyCopy , int policyId , int staffLevelId)
        {
            var db = new PolicyDetail_DAL();
            var strPolicyCopy = lstPolicyCopy != null ? String.Join(",", lstPolicyCopy.ToArray()) : string.Empty;
            var currentUserId = Global.CurrentUser.UserID;
            var result = db.CopyPolicyDetail(strPolicyCopy, policyId, staffLevelId, currentUserId);
            if(result != 0)
            {
                return Content(JsonConvert.SerializeObject(new
                {   
                    IsSuccess = true,
                    Message = AppRes.MS_Copy_success
                }));
            }
            return Content(JsonConvert.SerializeObject(new
            {
                IsSuccess = false,
                Message = AppRes.FormulaValid
            }));
        }
    }
}