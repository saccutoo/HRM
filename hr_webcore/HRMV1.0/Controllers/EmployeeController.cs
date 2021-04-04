
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
    public class EmployeeController : Controller
    {
        // GET: Employee
        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/Employee/TableServerSideGetData";
            ViewBag.url1 = "/OrganizationUnit/TableServerSideGetData";
            ViewBag.url2 = "/WorkingProcess/TableServerSideGetData";
            ViewBag.url3 = "/EmployeeAllowance/TableServerSideGetData";
            ViewBag.url4 = "/SocialInsuranceDetail/TableServerSideGetData";
            ViewBag.url5 = "/EmployeeRelationships/TableServerSideGetData";
            ViewBag.url6 = "/EmployeeBonus_Discipline/TableServerSideGetData";
            ViewBag.CurrentRoleId = Global.CurrentUser.RoleId;
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Employee_Gets")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter)
        {
            var db = new EmployeeDAL();
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
            var result = db.GetEmployee(baseListParam, out total);
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal,
                hrID = baseListParam.UserId.ToString(),
                roleID = baseListParam.UserType,
                positionID = Global.CurrentUser.OfficePositionID
            }));
        }


        public ActionResult Save_Employee()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Get)]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new EmployeeDAL();
            var result = db.GetEmployeeById(Global.CurrentUser.RoleId, idTable, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "Employee_Save")]
        public ActionResult SaveEmployee(Employee obj)
        {
            var db = new EmployeeDAL();
            obj.CreatedBy = Global.CurrentUser.UserID;
            obj.ModifiedBy = Global.CurrentUser.UserID;
            // mã hóa pass
            if (!string.IsNullOrEmpty(obj.Password))
            {
                obj.Password = Md5Utils.Encryption(obj.Password);
            }
            var result = db.SaveEmployee(Global.CurrentUser.RoleId, 1, obj);
            if (result.IsSuccess == true && obj.StaffID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && obj.StaffID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            else if (result.IsSuccess == false && result.existedResult == -4) // đã tồn tại SSN
            {
                result.Message = AppRes.SSNExists;
            }
            else if (result.IsSuccess == false && result.existedResult == -3) //đã tồn tại mã nhân viên
            {
                result.Message = AppRes.StaffCodeExists;
            }
            else if (result.IsSuccess == false && result.existedResult == -2) // đã tồn tại tên đăng nhập
            {
                result.Message = AppRes.UserNameExists;
            }
            else if (result.IsSuccess == false && result.existedResult == -1) // đã tồn tại mã và tên dn và ssn
            {
                result.Message = AppRes.UserNameStaffCodeExists;
            }

            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "EmployeeDelete")]
        public ActionResult _DeleteEmployee(int id, int idTable)
        {
            var db = new EmployeeDAL();
            var result = db.DeleteEmployee(Global.CurrentUser.RoleId, idTable, id);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Delete_success;
            else
                result.Message = AppRes.NotFound;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Get)]
        public ActionResult GetStatusContract()
        {
            var db = new CommonDal();
            var result = db.GetsWhereParentIDnotTree(Constants.Constant.numGlobalListParent.Statuscontract.GetHashCode(), Global.CurrentUser.CurrentLanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Get)]
        public ActionResult GetStaff()
        {
            var db = new EmployeeDAL();
            var result = db.GetStaff();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Get)]
        public ActionResult GetProvinceByCountry(int countryID)
        {
            var db = new EmployeeDAL();
            var result = db.GetProvinceByCountry(countryID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Get)]
        public ActionResult GetHRIds()
        {
            var db = new EmployeeDAL();
            var result = db.GetHRIds();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Get)]
        public ActionResult GetWorkingDayMachine()
        {
            var db = new EmployeeDAL();
            var result = db.GetWorkingDayMachine();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Get)]
        public ActionResult GetRoleID()
        {
            var db = new EmployeeDAL();
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetRoleID(baseListParam);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Get)]
        public ActionResult GetListDepartment(int pageIndex = 1, int pageSize = 500, string filter = "")
        {
            var db = new OrganizationUnitDAL();
            int usertype = Global.CurrentUser.RoleId;
            int userid = Global.CurrentUser.UserID;
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = 1,
                PageSize = int.MaxValue,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            int? total = 0;
            var result = db.GetListDepartmemt(baseListParam, out total);
            var list = JsonConvert.SerializeObject(result,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");

        }

        [Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "Employee_Gets")]
        public ActionResult EmployeeExportExcel(string filterString, int pageIndex, int pageSize)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[32]
            {
                new DataColumn(AppRes.StaffCode),
                new DataColumn(AppRes.EmployeeStatus),
                new DataColumn(AppRes.FullName),
                new DataColumn(AppRes.Date_Birth),
                new DataColumn(AppRes.Gender),
                new DataColumn(AppRes.Branch),
                new DataColumn(AppRes.Company),
                new DataColumn(AppRes.Office),
                new DataColumn(AppRes.Department),
                new DataColumn(AppRes.Position),
                new DataColumn(AppRes.StatusOfLaborContract),
                new DataColumn(AppRes.BrowsingStatus),
                new DataColumn(AppRes.DecisionNo),
                new DataColumn(AppRes.WPStartDate),
                new DataColumn(AppRes.WPEndDate),
                new DataColumn(AppRes.Timekeeping_Status),
                new DataColumn(AppRes.WPNote),
                new DataColumn(AppRes.Manager),
                new DataColumn(AppRes.ManagementHR),
                new DataColumn(AppRes.OfficeRole),
                new DataColumn(AppRes.Rank),
                new DataColumn(AppRes.ContractType),
                new DataColumn(AppRes.StartDateContract),
                new DataColumn(AppRes.EndDateContract),
                new DataColumn(AppRes.ContractNo),
                new DataColumn(AppRes.Policy),
                new DataColumn(AppRes.Currency),
                new DataColumn(AppRes.BasicPay),
                new DataColumn(AppRes.EfficiencyBonus),
                new DataColumn(AppRes.SumAllowance),
                new DataColumn(AppRes.ReceipientBankName),
                new DataColumn(AppRes.BankAccount)
            });
            dt.Columns[3].DataType = typeof(DateTime);
            dt.Columns[13].DataType = typeof(DateTime);
            dt.Columns[14].DataType = typeof(DateTime);
            dt.Columns[27].DataType = typeof(double);
            dt.Columns[28].DataType = typeof(double);
            dt.Columns[29].DataType = typeof(double);
            var db = new EmployeeDAL();
            int? total = 0;
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
            var lstData = db.ExportExcelEmployee(baseListParam, out total);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                    item.StaffCode,
                    item.StatusName,
                    item.FullName,
                    item.BirthDay,
                    item.GenderName,
                    item.BranchName,
                    item.CompanyName,
                    item.OfficeName,
                    item.OrganizationUnitNameExcel,
                    item.OfficePositionName,
                    item.StatusContractName,
                    item.WPTypeName,
                    item.DecisionNo,
                    item.WPStartDate == null ? null : item.WPStartDate,
                    item.WPEndDate == null ? null : item.WPEndDate,
                    item.BrowsingStatus,
                    item.WPNote,
                    item.Manager,
                    item.HRIdsName,
                    item.OfficeRole,
                    item.Rank,
                    item.ContractType,
                    item.StartDateContract,
                    item.EndDateContract,
                    item.ContractNo,
                    item.PolicyApplies,
                    item.Currency,
                    item.BasicPay,
                    item.EfficiencyBonus,
                    item.SumAllowance,
                    item.BankName,
                    item.BankNumber
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee.xlsx");
        }

        //[Permission(TableID = (int)ETable.Staff, TypeAction = (int)EAction.Excel)]
        //public ActionResult EmployeeExportExcel(string filterString, int pageIndex, int pageSize)
        //{
        //    DataTable dt = new DataTable("Grid");
        //    dt.Columns.AddRange(new DataColumn[29]
        //    {
        //        new DataColumn(AppRes.StaffCode),
        //        new DataColumn(AppRes.Department),
        //        new DataColumn("Tên không dấu"),
        //        new DataColumn(AppRes.FullName),
        //        new DataColumn(AppRes.Office),
        //        new DataColumn(AppRes.Position),
        //        new DataColumn("Cấp bậc"),
        //        new DataColumn(AppRes.EmployeeStatus),
        //        new DataColumn("Ngày vào làm"),
        //        new DataColumn("Ngày chính thức"),
        //        new DataColumn("Ngày nghỉ việc"),
        //        new DataColumn("Ngày hưởng"),
        //        new DataColumn("Số tháng làm việc"),
        //        new DataColumn("Mức % được hưởng"),
        //        new DataColumn(AppRes.Date_Birth),
        //        new DataColumn("Ngày tổ chức sinh nhật"),
        //        new DataColumn(AppRes.Gender),
        //        new DataColumn(AppRes.Mobile),
        //        new DataColumn("Hộ khẩu"),
        //        new DataColumn("Nơi tạm tú/Chỗ ở hiện tại"),
        //        new DataColumn("CMND"),
        //        new DataColumn(AppRes.DateRange),
        //        new DataColumn(AppRes.IssuePlace),
        //        new DataColumn("Email cá nhân"),
        //        new DataColumn("Email công ty"),
        //        new DataColumn("Email cán bộ quản lý"),
        //        new DataColumn("Số tài khoản"),
        //        new DataColumn("Chủ tài khoản"),
        //        new DataColumn("Ngân hàng")

        //    });

        //    dt.Columns[13].DataType = typeof(double);
        //    //dt.Columns[28].DataType = typeof(double);
        //    //dt.Columns[29].DataType = typeof(double);
        //    var db = new EmployeeDAL();
        //    int? total = 0;
        //    var baseListParam = new BaseListParam()
        //    {
        //        FilterField = filterString,
        //        OrderByField = "",
        //        PageIndex = pageIndex,
        //        PageSize = int.MaxValue,
        //        UserType = Global.CurrentUser.RoleId,
        //        UserId = Global.CurrentUser.LoginUserId,
        //        DeptId = Global.CurrentUser.OrganizationUnitID,
        //        LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
        //    };
        //    var lstData = db.ExportExcelEmployee(baseListParam, out total);
        //    foreach (var item in lstData)
        //    {
        //        dt.Rows.Add(
        //            item.StaffCode,
        //            item.OrganizationUnit,
        //            null,
        //            item.Fullname,
        //            item.OfficeName,
        //            item.OfficePositionName,
        //            item.Rank,
        //            item.Status,
        //            item.StartWorkingDate == null ? null : item.StartWorkingDate,
        //            item.OfficialDate == null ? null : item.OfficialDate,
        //            item.EndWorkingDate == null ? null : item.EndWorkingDate,
        //            item.SeniorityAllowancesDate == null ? null : item.SeniorityAllowancesDate,
        //            null,
        //            item.SeniorityAllowances == null ? null : item.SeniorityAllowances,
        //            item.BirthDay == null ? null : item.BirthDay,
        //            null,
        //            item.Gender,
        //            item.Mobile,
        //            item.Address,
        //            item.ContactAddress,
        //            item.IdentityNumber,
        //            item.IDIssuedDate == null ? null : item.IDIssuedDate,
        //            item.IDIssuedBy,
        //            item.EmailPersonal,
        //            item.EmailCompany,
        //            item.CCEmail,
        //            item.BankNumber,
        //            item.AccountName,
        //            item.BankName
        //           );
        //    }
        //    var wb = new XLWorkbook();
        //    wb.Worksheets.Add(dt);
        //    byte[] data = null;
        //    using (var stream = new MemoryStream())
        //    {
        //        wb.SaveAs(stream);
        //        data = stream.ToArray();
        //    }
        //    return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee.xlsx");
        //}


    }
}