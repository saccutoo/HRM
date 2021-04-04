
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
    public class EmployeeBonus_DisciplineController : Controller
    {
        // GET: EmployeeBonus_Discipline

        [Permission(TableID = (int)ETable.EmployeeBonus_Discipline, TypeAction = (int)EAction.Index)]
        public ActionResult Index(int Id)
        {
            Session["StaffID"] = Id;
            return PartialView();
        }

        [Permission(TableID = (int)ETable.EmployeeBonus_Discipline, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeBonus_Discipline_Gets")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, int SessionStaffID = 0, string filter = "")
        {
            var db = new EmployeeBonus_DisciplineDAL();
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
            int Type = 0; // khen thuong
            var result = db.GetEmployeeBonus_Discipline(baseListParam, out total, SessionStaffID, Type);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total
            }));
        }

        [Permission(TableID = (int)ETable.EmployeeBonus_Discipline, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeBonus_Discipline_Gets")]
        public ActionResult TableServerSideGetData2(int pageIndex, int pageSize, int SessionStaffID = 0, string filter = "")
        {
            var db = new EmployeeBonus_DisciplineDAL();
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
            int Type = 1; // Ky luat
            var result = db.GetEmployeeBonus_Discipline(baseListParam, out total, SessionStaffID, Type); 
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total
            }));
        }


        public ActionResult Save_EmployeeBonusDiscipline()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.EmployeeBonus_Discipline, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "EmployeeBonus_Discipline_Save")]
        public ActionResult SaveEmployeeBonusDiscipline(EmployeeBonus_Discipline obj)
        {
            var db = new EmployeeBonus_DisciplineDAL();
            int staffID = 0;
            int.TryParse(Session["StaffID"].ToString(), out staffID);
            obj.CreatedBy = Global.CurrentUser.UserID;
            obj.ModifiedBy = Global.CurrentUser.UserID;
            var result = db.SaveEmployeeBonusDiscipline(Global.CurrentUser.RoleId, 1, obj, staffID);
            if (result.IsSuccess == true && obj.AutoID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && obj.AutoID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.EmployeeBonus_Discipline, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "EmployeeBonus_Discipline_Delete")]
        public ActionResult _DeleteEmployeeBonusDiscipline(int id, int idTable)
        {
            var db = new EmployeeBonus_DisciplineDAL();
            var result = db.DeleteEmployeeBonusDiscipline(Global.CurrentUser.RoleId, idTable, id, Global.CurrentLanguage);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Delete_success;
            else
                result.Message = AppRes.NotFound;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.EmployeeBonus_Discipline, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeBonus_Discipline_GetInfo")]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new EmployeeBonus_DisciplineDAL();
            var result = db.GetEmployeeBonus_DisciplineDALById(Global.CurrentUser.RoleId, idTable, id, Global.CurrentLanguage);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        [Permission(TableID = (int)ETable.EmployeeBonus_Discipline, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeBonus_Discipline_GetDropdownByType")]
        public ActionResult GetActionByParentAndType(int ParentID,int Type)
        {
            var db = new EmployeeBonus_DisciplineDAL();
            var result = db.GetActionByParentAndType(ParentID,Type);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.EmployeeBonus_Discipline, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "EmployeeBonus_Discipline_Gets")]
        public ActionResult EmployeeBonusDisciplineExportExcel(string filterString, int pageIndex, int pageSize,int Type,int SessionStaffID=0)
        {
            DataTable dt = new DataTable("Grid");
            if (Type == 1)
            {
                dt.Columns.AddRange(new DataColumn[9]
                {
                    new DataColumn(AppRes.DisciplineGroup),
                    new DataColumn(AppRes.DisciplinaryContent),
                    new DataColumn(AppRes.DisciplineForm),
                    new DataColumn(AppRes.FineAmount),
                    new DataColumn(AppRes.Currency),
                    new DataColumn(AppRes.DecisionNo),
                    new DataColumn(AppRes.SignDay),
                    new DataColumn(AppRes.EffectiveDate),
                    new DataColumn(AppRes.Note)

                });
            }
            else
            {
                dt.Columns.AddRange(new DataColumn[9]
                {
                    new DataColumn(AppRes.RewardGroup),
                    new DataColumn(AppRes.RewardContent),
                    new DataColumn(AppRes.RewardForm),
                    new DataColumn(AppRes.RewardMoney),
                    new DataColumn(AppRes.Currency),
                    new DataColumn(AppRes.DecisionNo),
                    new DataColumn(AppRes.SignDay),
                    new DataColumn(AppRes.EffectiveDate),
                    new DataColumn(AppRes.Note)

                });
            }
            dt.Columns[3].DataType = typeof(double);
            var db = new EmployeeBonus_DisciplineDAL();
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
            var lstData = db.ExportExcelEmployeeBonusDiscipline(baseListParam, out total, SessionStaffID, Type);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                    item.GroupName,
                    item.Content,
                    item.ActionName,
                    item.Amount,
                    item.CurrencyName,
                    item.DecisionNo,
                    item.SignDate,
                    item.ApplyDate,
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
            var excelName = "";
            switch (Type)
            {
                case 0:
                    // khen thưởng
                    excelName = "EmployeeBonus.xlsx";
                    break;
                case 1:
                    // kỷ luật
                    excelName = "EmployeeDiscipline.xlsx";
                    break;
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

    }
}