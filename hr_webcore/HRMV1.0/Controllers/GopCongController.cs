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
    public class GopCongController : Controller
    {
        // GET: GopCong
        [Permission(TableID = (int)ETable.Merge, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/GopCong/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.Merge, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "CHECKINOUT_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter7, string filter6, string filter = "")
        {
            var db = new MergeDAL();
            int total = 0;
            if (filter7 == "")filter7 = "0";
            if (filter6 == "")filter6 = "0";
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.Merge_GetList(pageIndex, pageSize, filter, LanguageCode, int.Parse(filter7), int.Parse(filter6), out total);
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
        [Permission(TableID = (int)ETable.Merge, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "Merge")]
        public ActionResult Merge(int WorkingDayMachineIDOld, int WorkingDayMachineIDNew, string ListUserId, DateTime FromDate)
        {
            var db = new Timekeeping_TimeSSNDal();
            SystemMessage result = new SystemMessage();
            result = db.Merge(WorkingDayMachineIDOld, WorkingDayMachineIDNew, ListUserId, FromDate);
            if (result.IsSuccess == true)
                 result.Message = AppRes.MergeSuccess;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        //Lấy thông tin nhân viên theo máy chấm công
        [Permission(TableID = (int)ETable.Merge, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Employee_Get_ByWorkingDayMachineID")]

        public ActionResult EmployeeByWorkingDayMachineID(int? Id)
        {
            var db = new MergeDAL();
            var LanguageID = Global.CurrentLanguage;
            var result = db.EmployeeByWorkingDayMachineID(Id.Value);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        public ActionResult SaveMeger()
        {
            return PartialView();
        }
    }
}