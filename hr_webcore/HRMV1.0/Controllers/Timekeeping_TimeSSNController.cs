using ERP.Framework.DataBusiness.Common;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Security;
using HRM.DataAccess.Entity;
using ERP.Framework.App_LocalResources;
using System.Data;
using static HRM.Constants.Constant;
using HRM.Logger;
using HRM.DataAccess.Entity.UserDefinedType;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class Timekeeping_TimeSSNController : Controller
    {
        // GET: TimekeepingMachine
        [Permission(TableID = (int)ETable.Timekeeping_TimeSSN, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url1 = "/Timekeeping_TimeSSN/TableServerSideGetData";
            ViewBag.url2 = "/HR_WorkingDaySupplement/TableServerSideGetData";
            ViewBag.url3 = "/RecommendedList/TableServerSideGetData";
            ViewBag.url4 = "/HR_WorkingDaySummary/TableServerSideGetData";
            ViewBag.url5 = "/Timekeeping/TableServerSideGetData";
            ViewBag.url6 = "/Timekeeping_ManagerVacation/TableServerSideGetData";


            return PartialView();
        }

        [Permission(TableID = (int)ETable.Timekeeping_TimeSSN, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Get_TimeKeepingMachine")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, int month, int year, string filter = "")
        {
            var db = new Timekeeping_TimeSSNDal();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
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
            var result = db.GetTimeKeepingMachine(baseListParam, month, year, out totalColumns);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                lstTotal = totalColumns,
                userid = baseListParam.UserId
            }));
        }

        [Permission(TableID = (int)ETable.Timekeeping_TimeSSN, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "HR_WorkingDaySupplement_Send")]

        public ActionResult HR_WorkingDaySupplement_Send(int user)
        {
            var db = new Timekeeping_TimeSSNDal();
            int userid = user;
            if (userid == 0)
            {
                userid = Global.CurrentUser.LoginUserId;
            }
            var result = db.HR_WorkingDaySupplement_Send(userid);
            if (result.IsSuccess == true)
            {
                result.Message = AppRes.MS_Update_success;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.Timekeeping_TimeSSN, TypeAction = (int)EAction.Get)]
        public ActionResult HR_CheckMissDayApproval()
        {
            var db = new Timekeeping_TimeSSNDal();
            int ShowTabApproval = db.HR_CheckMissDayApproval(Global.CurrentUser.LoginUserId);
            return Content(JsonConvert.SerializeObject(new
            {
                ShowTabApproval = ShowTabApproval
            }));
        }

        [Permission(TableID = (int)ETable.Timekeeping_TimeSSN, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "HR_WorkingDaySupplement_Add")]

        public ActionResult SaveBoSungCong(HR_WorkingDaySupplement entity)
        {
            entity.HourOff = entity.FullHourOff;
            var db = new Timekeeping_TimeSSNDal();
            DateTime now = DateTime.Now;
            int langid = Global.CurrentUser.CurrentLanguageID;
            if (entity.StaffID == 0)
            {
                entity.StaffID = Global.CurrentUser.UserID;
            }
            entity.CreatedBy = entity.StaffID;
            entity.CreatedDate = now;
            int checkphep = 0;
            int checkduyetbosung = 0;
            if (entity.Type == 2)
            {
                checkphep = db.HR_CheckFurlough(entity, langid);
            }
            var result = new SystemMessage();
           
            if (checkphep == 0)
            {
                checkduyetbosung = db.HR_WorkingDaySupplement_CheckExists(entity);
                if (checkduyetbosung == 2 && entity.ReasonType == 1214)
                {
                    result.Message = "1";
                }
                else if (checkduyetbosung == 0)
                {
                    result.Message = "0";
                }
                else if (checkduyetbosung == 3)
                {
                    result.Message = "3";
                }
                else if (((entity.HourOff == null || entity.TimeOfActual == null) && entity.Type == 1) || ((entity.FromTime == null || entity.ToTime == null) && entity.Type != 1))
                {
                    result.Message = "4";
                }
                else
                {
                    if (entity.Type != 5 && entity.Type != 4)
                    {
                        entity.PercentPayrollID = null;
                    }
                    result = db.SaveBoSungCong(entity);
                    if (result.IsSuccess == true)
                    {
                        result.Message = AppRes.MS_Update_success;
                    }
                }
            }
            else
            {
                result.Message = "2";
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Timekeeping_TimeSSN, TypeAction = (int)EAction.Get)]
        public ActionResult HR_WorkingDay_GetHour(int userid)
        {
            var db = new Timekeeping_TimeSSNDal();
            if (userid == 0)
            {
                userid = Global.CurrentUser.LoginUserId;
            }
            var result = db.HR_WorkingDay_GetHour(userid);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.Timekeeping_TimeSSN, TypeAction = (int)EAction.Get)]
        public ActionResult GetCustomers(int userid)
        {
            var db = new Timekeeping_TimeSSNDal();
            if (userid == 0)
            {
                userid = Global.CurrentUser.LoginUserId;
            }
            var result = db.GetCustomers(userid);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult GetDateApproval(int userid)
        {
            var db = new Timekeeping_TimeSSNDal();
            if (userid == 0)
            {
                userid = Global.CurrentUser.LoginUserId;
            }
            var result = db.GetDateApproval(userid);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.Timekeeping_TimeSSN, TypeAction = (int)EAction.Get)]
        public ActionResult GetCustomerContacts(int customerid)
        {
            var db = new Timekeeping_TimeSSNDal();
            int roleid = Global.CurrentUser.RoleId;
            var result = db.GetCustomerContacts(customerid, roleid);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult BoSungCong()
        {
            return PartialView();
        }
        public ActionResult AddBoSungCong()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.Timekeeping_TimeSSN, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "HR_WorkingDaySupplement_SaveList")]
        public ActionResult SaveListWorkingday(List<HR_WorkingDaySupplementSaveListType> Data)
        {
            SystemMessage result = new SystemMessage();
            var db = new Timekeeping_TimeSSNDal();
            foreach (var item in Data)
            {
                item.HourOff = item.FullHourOff;
                item.CreatedBy = item.StaffID;
                item.CreatedDate = DateTime.Now;
                if (item.StaffID == 0)
                {
                    item.StaffID = Global.CurrentUser.UserID;
                }
                if (item.Type != 5 && item.Type != 4)
                {
                    item.PercentPayrollID = null;
                }
                if (((item.HourOff == null || item.TimeOfActual == null) && item.Type == 1) || ((item.FromTime == null || item.ToTime == null) && item.Type != 1))
                {
                    result.Message = "4";
                    result.IsSuccess = false;
                    return Content(JsonConvert.SerializeObject(new
                    {
                        result
                    }));
                }
            }
            int OutPut = 0;
            result = db.SaveListWorkingday(Data, out OutPut);
            if (OutPut==5)
            {
                result.Message = "2";
            }
            else if(OutPut == 2 && Data[0].ReasonType == 1214)
            {
                result.Message = "1";
            }
            else if (OutPut == 0)
            {
                result.Message = "0";
            }
            else if (OutPut == 3)
            {
                result.Message = "3";
            }
            else  if(OutPut== 6)
            {
                result.Message = "6";
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}