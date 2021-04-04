using HRM.Common;
using HRM.DataAccess.DAL;
using ERP.Framework.DataBusiness.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.DataAccess.Entity;
using HRM.App_LocalResources;
using System.Text.RegularExpressions;
using HRM.Models;
using HRM.Security;
using static HRM.Constants.Constant;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using HRM.Logger;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class SocialInsuranceDetailController : Controller
    {
        // GET: SocialInsuranceDetail
        [Permission(TableID = (int)ETable.SocialInsuranceDetail, TypeAction = (int)EAction.Index)]
        public ActionResult Index(int Id)
        {
            Session["StaffID"] = Id;
            return PartialView();
        }

      
        [Permission(TableID = (int)ETable.SocialInsuranceDetail, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "SocialInsuranceDetail_Gets")]

        public ActionResult TableServerSideGetData(int pageIndex = 1, int pageSize = 5, int SessionStaffID=0, string filter = "")
        {
            var db = new SocialInsuranceDetailDAL();
            int? total = 0;
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            int staffID = 0;
            int.TryParse(Session["StaffID"].ToString(), out staffID);
            var result = db.GetSocialInsuranceDetail(baseListParam, out total, SessionStaffID);
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

        public ActionResult Save_SocialInsuranceDetail()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.SocialInsuranceDetail, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "SocialInsuranceDetail_Save")]

        public ActionResult SaveSocialInsuranceDetail(SocialInsuranceDetail obj)
        {
            var db = new SocialInsuranceDetailDAL();
            int staffID = 0;
            int.TryParse(Session["StaffID"].ToString(), out staffID);
            var result = db.SaveSocialInsuranceDetail(Global.CurrentUser.RoleId, 1, obj, obj.StaffID);
            if (result.IsSuccess == true && obj.InsuranceID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && obj.InsuranceID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            else if (result.IsSuccess == false && result.existedResult==-1)
            {
                result.Message = AppRes.FromMonthValidate + result.ExistedDate;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.SocialInsuranceDetail, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "SocialInsurance_Delete")]

        public ActionResult _DeleteSocialInsuranceDetail(int id, int idTable)
        {
            var db = new SocialInsuranceDetailDAL();
            var result = db.DeleteSocialInsuranceDetail(Global.CurrentUser.RoleId, idTable, id, Global.CurrentLanguage);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Delete_success;
            else
                result.Message = AppRes.NotFound;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.SocialInsuranceDetail, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "SocialInsurance_GetInfo")]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new SocialInsuranceDetailDAL();
            var result = db.GetSocialInsuranceDetailById(Global.CurrentUser.RoleId, idTable, id, Global.CurrentLanguage);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.SocialInsuranceDetail, TypeAction = (int)EAction.Get)]
        public ActionResult GetSocialInsuranceLastID(int SessionStaffID = 0)
        {
            var db = new SocialInsuranceDetailDAL();
            int staffID = 0;
            int.TryParse(Session["StaffID"].ToString(), out staffID);
            var result = db.GetSocialInsuranceLastID(SessionStaffID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.SocialInsuranceDetail, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "SocialInsuranceDetail_Gets")]

        public ActionResult SocialInsuranceExportExcel(string filterString, int pageIndex, int pageSize, int SessionStaffID = 0)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[13]
            {
                new DataColumn(AppRes.InsuranceStatus),
                new DataColumn(AppRes.FromMonth),
                new DataColumn(AppRes.ToMonth),
                new DataColumn(AppRes.TimeToPaySocialInsuranceNumber),
                new DataColumn(AppRes.PlaceHoldInsuranceNumber),
                new DataColumn(AppRes.InsuranceSalary),
                new DataColumn(AppRes.CompanyRate),
                new DataColumn(AppRes.PersonRate),
                new DataColumn(AppRes.InsuranceNumber),
                new DataColumn(AppRes.PlaceHeathCare),
                new DataColumn(AppRes.Regime),
                new DataColumn(AppRes.Timekeeping_Status),
                new DataColumn(AppRes.Note)
            });

            dt.Columns[5].DataType = typeof(double);
            dt.Columns[6].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(double);
            dt.Columns[8].DataType = typeof(double);
            var db = new SocialInsuranceDetailDAL();
            int? total = 0;
            int staffID = 0;
            int.TryParse(Session["StaffID"].ToString(), out staffID);
            var baseListParam = new BaseListParam()
            {
                FilterField = filterString,
                OrderByField = "",
                PageIndex = pageIndex,
                PageSize = int.MaxValue,
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var lstData = db.ExportExcelSocialInsurance(baseListParam, out total, SessionStaffID);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                    item.StatusName,
                    item.FromMonth,
                    item.ToMonth,
                    item.DateReturn,
                    item.PlaceHold,
                    item.BasicSalary,
                    item.RateCompany,
                    item.RatePerson,
                    item.InsuranceNumber,
                    item.PlaceHealthCare,
                    item.RegimeName,
                    item.ApproveStatusName,
                    item.Note
                   );
            }
            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SocialInsurance.xlsx");
        }
    }
}