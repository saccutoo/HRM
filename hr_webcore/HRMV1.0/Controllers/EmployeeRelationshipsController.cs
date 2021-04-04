
using System;
using ClosedXML.Excel;
using ERP.DataAccess.DAL;
using ERP.Framework.DataBusiness.Common;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Models;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Web.Mvc;
using HRM.Security;
using HRM.Utils;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using static HRM.Constants.Constant;
using HRM.Logger;
using Core.Web.Enums;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class EmployeeRelationshipsController : Controller
    {
        // GET: EmployeeRelationships
        [Permission(TableID = (int)ETable.EmployeeRelationships, TypeAction = (int)EAction.Index)]
        public ActionResult Index(int Id)
        {
            Session["StaffID"] = Id;
            return PartialView();
        }

        [Permission(TableID = (int)ETable.EmployeeRelationships, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeRelationships_Gets")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, int SessionStaffID = 0, string filter = "")
        {
            var db = new EmployeeRelationshipsDAL();
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
            var result = db.GetEmployeeRelationships(baseListParam, out total, SessionStaffID);
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

        public ActionResult Save_EmployeeRelationships()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.EmployeeRelationships, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "EmployeeRelationships_Save")]
        public ActionResult SaveEmployeeRelationships(EmployeeRelationships obj)
        {
            var db = new EmployeeRelationshipsDAL();
            obj.CreatedBy = Global.CurrentUser.UserID;
            obj.ModifiedBy = Global.CurrentUser.UserID;
            int staffID = 0;
            int.TryParse(Session["StaffID"].ToString(), out staffID);
            var result = db.SaveEmployeeRelationships(Global.CurrentUser.RoleId, 1, obj, staffID);
            if (result.IsSuccess == true && obj.StaffID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && obj.StaffID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.EmployeeRelationships, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeRelationships_GetInfo")]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new EmployeeRelationshipsDAL();
            var languageID = Global.CurrentLanguage;
            var result = db.GetEmployeeRelationshipsById(Global.CurrentUser.RoleId, idTable, id, languageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.EmployeeRelationships, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "EmployeeRelationships_Delete")]
        public ActionResult _DeleteEmployeeRelationships(int id, int idTable)
        {
            var db = new EmployeeRelationshipsDAL();
            var result = db.DeleteEmployeeRelationships(Global.CurrentUser.RoleId, idTable, id);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Delete_success;
            else
                result.Message = AppRes.NotFound;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}