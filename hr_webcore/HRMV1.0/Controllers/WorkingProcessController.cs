using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using Newtonsoft.Json;
using HRM.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Models;
using HRM.Security;
using static HRM.Constants.Constant;
using HRM.Logger;
using System.Threading;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class WorkingProcessController : Controller
    {
        // GET: WorkingProcess
        [Permission(TableID = (int)ETable.WorkingProcess, TypeAction = (int)EAction.Index)]
        public ActionResult Index(int Id)
        {
            Session["StaffID"]  = Id;
            return PartialView();
        }
        [Permission(TableID = (int)ETable.WorkingProcess, TypeAction = (int)EAction.Get)]   
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "WorkingProcess_Gets")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize , int SessionStaffID, string filter = "")
        {
            var db = new WorkingProcessDAL();
            int total = 0;
            var LanguageID = Global.CurrentLanguage;
            var RoleId = Global.CurrentUser.RoleId;
            var UserID = Global.CurrentUser.UserID;
            var OfficePositionID = Global.CurrentUser.OfficePositionID;
            int staffID = 0;
            int.TryParse(Session["StaffID"].ToString(),out staffID);
            var DeptID = Global.CurrentUser.OrganizationUnitID;
            var result = db.GetWorkingProcess(pageIndex, pageSize,filter, out total, LanguageID, RoleId, UserID, DeptID, SessionStaffID, OfficePositionID);
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

     
        public ActionResult Save_WorkingProcess()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.WorkingProcess, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "WorkingProcess_GetInfo")]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new WorkingProcessDAL();
            var OfficePositionID = Global.CurrentUser.OfficePositionID;
            var result = db.GetWorkingProcessById(Global.CurrentUser.RoleId, idTable, id, OfficePositionID);
            var db1 = new EmployeeAllowanceDAL();
            int total = 0;
            var LanguageID = Global.CurrentLanguage;
            var RoleId = Global.CurrentUser.RoleId;
            var UserID = Global.CurrentUser.UserID;
            var DeptID = Global.CurrentUser.OrganizationUnitID;
            var result1 = db1.GetEmployeeAllowance(1, int.MaxValue, "wpid=" + id, out total, LanguageID, RoleId, UserID, DeptID, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result,
                result1
            }));
        }

        [Permission(TableID = (int)ETable.WorkingProcess, TypeAction = (int)EAction.Get)]
        public ActionResult GetPolicy()
        {
            var db = new WorkingProcessDAL();
            var result = db.GetPolicy();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.WorkingProcess, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "WorkingProcess_Save")]

        public ActionResult SaveWorkingProcess(WorkingProcess obj)
        {
            var db = new WorkingProcessDAL();
            int staffID = 0;
            int.TryParse(Session["StaffID"].ToString(), out staffID);
            var listDelete = obj.EmployeeAllowanceDeleteList;
            var result = db.SaveWorkingProcess(Global.CurrentUser.RoleId, 1, obj, obj.StaffID);
            if (result.IsSuccess == true && obj.WPID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && obj.WPID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            else if (result.IsSuccess == false && result.existedResult==-1)
            {
                result.Message = AppRes.CheckWPStartDate + result.ExistedDate;
            }
            //Start thread to update staff parent( new version of staff parent)
            //affect to dbo.StaffParent
            Thread thread = new Thread(() => UpdateStaffParent()) {
                Name = "UpdateStaffParent"
            };
            thread.Start();

            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.WorkingProcess, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "WorkingProcess_Delete")]

        public ActionResult _DeleteWorkingProcess(int id, int idTable)
        {
            var db = new WorkingProcessDAL();
            var result = db.DeleteWorkingProcess(Global.CurrentUser.RoleId, idTable, id);
            if (result.IsSuccess == true) { result.Message = AppRes.MS_Delete_success; }
            else{ result.Message = AppRes.NotFound; }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        public void UpdateStaffParent()
        {
            var db = new WorkingProcessDAL();
            db.UpdateStaffParent();

        }
    }
}