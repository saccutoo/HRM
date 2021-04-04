using ClosedXML.Excel;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.DataAccess.Entity;
using HRM.Models;
using ERP.Framework.DataBusiness.Common;
using HRM.Security;
using static HRM.Constants.Constant;
using HRM.Logger;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class OrganizationUnitController : Controller
    {
        // GET: OrganizationUnit
        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/OrganizationUnit/TableServerSideGetData";
            ViewBag.CurrentRoleId = Global.CurrentUser.RoleId;
            return PartialView();
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "OrganizationUnit_List_All")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new OrganizationUnitDAL();
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
            var result = db.GetOrganizationUnit(baseListParam, out total);
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

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetListEmployeeByOrganizationUnitID")]
        public ActionResult GetEmployee(int OrganizationUnitID)
        {
            var db = new OrganizationUnitDAL();
            var result = db.GetListEmployeeByOrganizationUnitID(OrganizationUnitID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetColumnByUsserID")]
        public ActionResult GetColumnByUsserID(int TableId, string filter)
        {
            var db = new OrganizationUnitDAL();
            int UserID = Global.CurrentUser.LoginUserId;
            var result = db.GetColumnByUsserID(TableId, UserID, filter);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "UpdateColumnByUsserID")]
        public ActionResult UpdateColumnByUsserID(int TableID, string Visible, int OrderNo, int TableColumnId)
        {
            var db = new OrganizationUnitDAL();
            int UserID = Global.CurrentUser.LoginUserId;
            var result = db.UpdateColumnByUsserID(UserID, TableID, Visible, OrderNo, TableColumnId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult ViewSaveOrganizationUnit()
        {
            return PartialView();
        }

        public ActionResult DinhDangCot()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "OrganizationUnit_GetsByID")]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new OrganizationUnitDAL();
            int usertype = Global.CurrentUser.RoleId;
            int userid = Global.CurrentUser.UserID;
            int LanguageID = Global.CurrentLanguage;
            var result = db.GetOrganizationUnitById(id, idTable, userid, usertype, LanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "OrganizationUnit_List_All")]
        public ActionResult GetListDepartment(int pageIndex = 1, int pageSize = 500, string filter = "")
        {
            var db = new OrganizationUnitDAL();
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
            var result = db.GetOrganizationUnit(baseListParam, out total); ;
            var list = JsonConvert.SerializeObject(result,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");

        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "OrganizationUnit_Save")]
        public ActionResult SaveOrganizationUnit(OrganizationUnit secRole)
        {
            var db = new OrganizationUnitDAL();
            int trung = 0;
            var result = db.SaveOrganizationUnit(1, 1, secRole, out trung);

            if (result.IsSuccess == true && secRole.OrganizationUnitID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && secRole.OrganizationUnitID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                bientrung = trung,
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "OrganizationUnit_DeleteByID")]
        public ActionResult _DeleteOrganizationUnit(int id, int idTable)
        {
            var db = new OrganizationUnitDAL();
            int usertype = Global.CurrentUser.RoleId;
            int userid = Global.CurrentUser.UserID;
            int LanguageID = Global.CurrentLanguage;
            var result = db.DeleteOrganizationUnit(id, idTable, userid, usertype, LanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "OrganizationUnit_List_All")]
        public ActionResult OrganizationUnitExportExcel(string filterString)
        {
            var baseListParam = new BaseListParam()
            {
                FilterField = filterString,
                OrderByField = "",
                PageIndex = 1,
                PageSize = int.MaxValue,
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn(AppRes.OrganazationUnitCode),
                new DataColumn(AppRes.NameVi),
                new DataColumn(AppRes.NameEn),
                new DataColumn(AppRes.Email),
                new DataColumn(AppRes.Phone),
                new DataColumn(AppRes.Status)
            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            var db = new OrganizationUnitDAL();
            int total = 0;
            var lstData = db.ExportOrganizationUnit(baseListParam, filterString);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.OrganizationUnitCode, item.Name, item.NameEN, item.Email, item.Phone,
                    item.Status == 0 ? "Không hoạt động" : "Hoạt động");
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SecRole.xlsx");
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Get_All_OrganizationUnitWhereRole")]
        public ActionResult GetOrganizationUnit(int chon)
        {
            var db = new OrganizationUnitDAL();
            var result = db.OrganizationUnitAll(chon, Global.CurrentUser.RoleId, Global.CurrentUser.UserID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Get_EmployeeWhereOrganizationUnitID")]
        public ActionResult GetEmployeeWhereOrganizationUnit(string id)
        {
            var db = new OrganizationUnitDAL();
            var languageID = Global.CurrentLanguage;
            var result = db.GetEmployeeWhereOrganizationUnit(languageID, id, Global.CurrentUser.UserID, Global.CurrentUser.RoleId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeByOrganizationUnitID")]
        public ActionResult EmployeeByOrganizationUnitID(string id)
        {
            var db = new OrganizationUnitDAL();
            var languageID = Global.CurrentLanguage;
            int id1 = Convert.ToInt32(id);
            var result = db.EmployeeByOrganizationUnitID(languageID, id1, Global.CurrentUser.UserID, Global.CurrentUser.RoleId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeByOrganizationUnitIDSupplement")]
        public ActionResult EmployeeByOrganizationUnitIDSupplement(string id)
        {
            var db = new OrganizationUnitDAL();
            var languageID = Global.CurrentLanguage;
            int id1 = Convert.ToInt32(id);
            var result = db.EmployeeByOrganizationUnitIDSupplement(languageID, id1, Global.CurrentUser.UserID, Global.CurrentUser.RoleId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetOrganizationUnitWhereParentID")]
        public ActionResult GetOrganizationUnitWhereParent(int ParentID)
        {
            var db = new OrganizationUnitDAL();
            var result = db.OrganizationUnitWhereParentID(ParentID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "GetOrganizationUnitWhereCompanyID")]
        public ActionResult GetOrganizationUnitWhereCompany(int CompanyID)
        {
            var db = new OrganizationUnitDAL();
            var result = db.OrganizationUnitWhereCompanyID(CompanyID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.OrganizationUnit, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Get_OrganizationUnit_Where_Type")]
        public ActionResult GetCompany()
        {
            var db = new OrganizationUnitDAL();
            var result = db.GetCompany();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}