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
    public class RecommendedListController : Controller
    {
        // GET: RecommendedList
        [Permission(TableID = (int)ETable.RecommendedList, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.RecommendedList, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "HR_WorkingDaySupplement_Get")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, int month, int year, int userid, int status, string filter = "")
        {
            var db = new RecommendedListDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
            int CurrentType = 0;
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
            var result = db.GetWorkingDaySupplementManager(baseListParam, month, year, CurrentType, status, out total);

            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal,
                userid = baseListParam.UserId
            }));
        }
        public ActionResult Edit_Recommended()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.RecommendedList, TypeAction = (int)EAction.Get)]
        public ActionResult Customer_Gets_ByUserID(int UserID)
        {
            var db = new TimekeepingDal();
            var result = db.GetCustomerContractByUser(UserID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.RecommendedList, TypeAction = (int)EAction.Get)]
        public ActionResult HR_CustomerContactGetsByCustomerID(int customerID)
        {
            var db = new TimekeepingDal();
            var result = db.HR_CustomerContactGetsByCustomerID(customerID, Global.CurrentUser.RoleId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.RecommendedList, TypeAction = (int)EAction.Get)]
        public ActionResult HR_WorkingDaySupplement_GetListId(int AutoID)
        {
            var db = new TimekeepingDal();
            int langId = Global.CurrentUser.CurrentLanguageID;
            var result = db.HR_WorkingDaySupplement_GetListId(AutoID, langId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.RecommendedList, TypeAction = (int)EAction.Get)]
        public ActionResult TableServerSideGetData1(int pageIndex, int pageSize, int month, int year, int userid, int status, string filter = "")
        {
            var db = new RecommendedListDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
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
            var result = db.GetTimeKeepingMachine(baseListParam, month, year, out total, userid);

            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal,
                userid = baseListParam.UserId
            }));
        }

        [Permission(TableID = (int)ETable.RecommendedList, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "HR_WorkingDaySupplement_Add")]

        public ActionResult SaveBoSungCong(HR_WorkingDaySupplement entity)
        {
            var db = new Timekeeping_TimeSSNDal();

            DateTime now = DateTime.Now;
            //entity.StaffID = Global.CurrentUser.UserID;

            entity.CreatedBy = entity.StaffID;
            entity.CreatedDate = now;
            int checkduyetbosung = 0;
            var result = new SystemMessage();
            checkduyetbosung = db.HR_WorkingDaySupplement_CheckExists(entity);
            if (checkduyetbosung == 1)
            {
                if (((entity.HourOff ==null || entity.TimeOfActual==null) && entity.Type==1) || ((entity.FromTime ==null || entity.ToTime == null) && entity.Type!=1 ))
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
            else if (checkduyetbosung == 0)
            {
                result.Message = "0";
            }
            else if (checkduyetbosung == 2)
            {
                result.Message = "2";
            }
            else if (checkduyetbosung ==3)
            {
                result.Message = "3";
            }
            else if (checkduyetbosung == 6)
            {
                result.Message = "6";
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.RecommendedList, TypeAction = (int)EAction.Get)]
        public ActionResult HR_WorkingDay_GetHour(int userid)
        {
            var db = new Timekeeping_TimeSSNDal();
            var result = db.HR_WorkingDay_GetHour(userid);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        public ActionResult RecommendedListExportExcel(int pageIndex, int pageSize, int month, int year, int status, string filter = "")
        {
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = 1,
                PageSize = 10000,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[16]
            {
                new DataColumn(AppRes.Timekeeping_CreateName),
                new DataColumn(AppRes.Timekeeping),
                new DataColumn(AppRes.E_CreatedDate),
                new DataColumn(AppRes.HR_FromHour),
                new DataColumn(AppRes.HR_ToHour),
                new DataColumn(AppRes.Timekeeping_TimeOffTitle),
                new DataColumn(AppRes.HR_WorkingDaySupplement),
                new DataColumn(AppRes.MultiplierFactor),
                new DataColumn(AppRes.Promotion_Status),
                new DataColumn(AppRes.Timekeeping_Reason),
                new DataColumn(AppRes.ContactName),
                new DataColumn(AppRes.Purpose),
                new DataColumn(AppRes.Timekeeping_Note),
                new DataColumn(AppRes.Promotion_CreatedOn),
                new DataColumn(AppRes.HR_ManagerNote),
                new DataColumn(AppRes.HR_HRNote),

            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(int);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(string);
            dt.Columns[10].DataType = typeof(string);
            dt.Columns[11].DataType = typeof(string);
            dt.Columns[12].DataType = typeof(string);
            dt.Columns[13].DataType = typeof(DateTime);
            dt.Columns[14].DataType = typeof(string);
            dt.Columns[15].DataType = typeof(string);

            var db = new RecommendedListDAL();
            int? total = 0;
            int CurrentType = 0;

            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.GetWorkingDaySupplementManager(baseListParam, month, year, CurrentType, status, out total);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.StaffName, item.TypeName, item.Date, item.FromTime.HasValue ? item.FromTime.Value.ToString("hh:mm") : string.Empty, item.ToTime.HasValue ? item.ToTime.Value.ToString("hh:mm") : string.Empty, item.HourOff.HasValue ? item.HourOff.Value.ToString("hh:mm") : string.Empty, item.DayOff, item.PercentPayrollID, item.StatusName, item.ReasonTypeName, item.Note, item.CustomerContactName, item.CustomerReasonTypeName, item.CreatedDate, item.ManagerNote, item.HRNote);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RecommendedList.xlsx");
        }

    }
}