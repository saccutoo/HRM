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
    public class HR_WorkingDayMachineSatListController : Controller
    {
        // GET: HR_WorkingDayMachineSatList
        [Permission(TableID = (int)ETable.HR_WorkingDayMachineSatList, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/HR_WorkingDayMachineSatList/TableServerSideGetData";
            return PartialView();
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "WorkingDayMachineSatList_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new WorkingDayMachineSatList_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.WorkingDayMachineSatList_GetList(pageIndex, pageSize, filter, LanguageCode, out total);
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

        [Permission(TableID = (int)ETable.HR_WorkingDayMachineSatList, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "HR_WorkingDayMachine_GetList")]
        public ActionResult WorkingDayMachineSatList_GetList()
        {
            var db = new WorkingDayMachineSatList_DAL();
            var result = db.WorkingDayMachineSatList_GetList();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayMachineSatList, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "HR_WorkingDayMachineSatList_Save")]
        public ActionResult WorkingDayMachineSatList_Save(HR_WorkingDayMachineSatList data,int [] ListWorkingDayMachineID)
        {
            var db = new WorkingDayMachineSatList_DAL();
            data.CreatedDate = DateTime.Now;
            data.CreatedBy = Global.CurrentUser.UserID;
            SystemMessage result = new SystemMessage();
            if (ListWorkingDayMachineID != null)
            {
                for (int i = 0; i < ListWorkingDayMachineID.Length; i++)
                {
                    data.WorkingDayMachineID = ListWorkingDayMachineID[i];
                    result = db.WorkingDayMachineSatList_Save(data);
                }
            }
            else
            {
                result = db.WorkingDayMachineSatList_Save(data);
            }
          
            //return View();
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }

        public ActionResult SaveWorkingDayMachineSatList()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.HR_WorkingDayMachineSatList, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "HR_WorkingDayMachineSatList_Delete")]
        public ActionResult HR_WorkingDayMachineSatList_Delete(int ID)
        {
            var db = new WorkingDayMachineSatList_DAL();
            var result = db.HR_WorkingDayMachineSatList_Delete(ID);
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
