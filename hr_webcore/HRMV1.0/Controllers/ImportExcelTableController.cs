
using ClosedXML.Excel;
using ERP.Framework.DataBusiness.Common;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
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
using HRM.DataAccess.Entity;
using static HRM.Constants.Constant;
using HRM.Logger;
using Core.Web.Enums;

namespace HRM.Controllers
{
    public class ImportExcelTableController : Controller
    {
        // GET: ImportExcel
        public ActionResult Index()
        {
            ViewBag.url = "/ImportExcelTable/TableServerSideGetData";
            ViewBag.url1 = "/ImportExcelTable/TableServerSideGetData1";
            ViewBag.url2 = "/ImportExcelTable/TableServerSideGetData2";
            ViewBag.url3 = "/ImportExcelTable/TableServerSideGetData3";
            ViewBag.url4 = "/ImportExcelTable/TableServerSideGetData4";
            return PartialView();
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeBonus_Discipline_Gets")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
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
            int Type = 0; // khen thuong
            var result = db.GetEmployeeBonus_Discipline(baseListParam, out total, staffID, Type);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total
            }));
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeBonus_Discipline_Gets")]
        public ActionResult TableServerSideGetData1(int pageIndex, int pageSize, string filter = "")
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
            int Type = 1; // Ky luat
            var result = db.GetEmployeeBonus_Discipline(baseListParam, out total, staffID, Type);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total
            }));
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeRelationships_Gets")]
        public ActionResult TableServerSideGetData2(int pageIndex, int pageSize, string filter = "")
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
            var result = db.GetEmployeeRelationships(baseListParam, out total, staffID);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total
            }));
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "WorkingProcess_GetAllStaff")]
        public ActionResult TableServerSideGetData3(int pageIndex, int pageSize, string filter = "")
        {
            var db = new ImportExcelDal();
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
            var result = db.GetWorkingProcess(baseListParam, out total);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total
            }));
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "EmployeeAllowance_GetAllStaff")]
        public ActionResult TableServerSideGetData4(int pageIndex, int pageSize, string filter = "")
        {
            var db = new ImportExcelDal();
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
            var result = db.GetEmployeeAllowance(baseListParam, out total);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total
            }));
        }

        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "EmployeeBonus_Discipline_Gets")]
        public ActionResult EmployeeBonusDisciplineExportExcel(string filterString, int pageIndex, int pageSize, int Type)
        {
            DataTable dt = new DataTable("Grid");
            if (Type == 1)
            {
                dt.Columns.AddRange(new DataColumn[12]
                {
                    new DataColumn(AppRes.AutoID),
                    new DataColumn(AppRes.OrganizationUnit),
                    new DataColumn(AppRes.Staff),
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
                dt.Columns.AddRange(new DataColumn[12]
                {
                    new DataColumn(AppRes.AutoID),
                    new DataColumn(AppRes.OrganizationUnit),
                    new DataColumn(AppRes.Staff),
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
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[6].DataType = typeof(double);
            var db = new EmployeeBonus_DisciplineDAL();
            int? total = 0;
            int staffID = 0;
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
            var lstData = db.ExportExcelEmployeeBonusDiscipline(baseListParam, out total, staffID, Type);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                    item.AutoID,
                    item.OrganizationUnit,
                    item.StaffName,
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

        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "EmployeeRelationships_Gets")]
        public ActionResult EmployeeRelationshipsExportExcel(string filterString, int pageIndex, int pageSize, int Type)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[13]
            {
                new DataColumn(AppRes.AutoID),
                new DataColumn(AppRes.OrganizationUnit),
                new DataColumn(AppRes.Staff),
                new DataColumn(AppRes.RelationShip),
                new DataColumn(AppRes.FullName),
                new DataColumn(AppRes.Date_Birth),
                new DataColumn(AppRes.Mobile),
                new DataColumn(AppRes.Deduction),
                new DataColumn(AppRes.DeductionCode),
                new DataColumn(AppRes.DeductionTimeFrom),
                new DataColumn(AppRes.DeductionTimeTo),
                new DataColumn(AppRes.Status),
                new DataColumn(AppRes.Note)

            });
            dt.Columns[0].DataType = typeof(double);
            var db = new EmployeeRelationshipsDAL();
            int? total = 0;
            int staffID = 0;
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
            var lstData = db.ExportExcelEmployeeRelationship(baseListParam, out total, staffID);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                    item.AutoID,
                    item.OrganizationUnit,
                    item.StaffName,
                    item.RelationshipName,
                    item.Name,
                    item.BirthDay,
                    item.Phone,
                    item.Deduction,
                    item.DeductionCode,
                    item.DeductionFrom,
                    item.DeductionTo,
                    item.StatusName,
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
            
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeRelationships.xlsx");
        }

        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "WorkingProcess_GetAllStaff")]
        public ActionResult WorkingProcessFullExportExcel(string filterString, int pageIndex, int pageSize, int Type)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[23]
            {
                new DataColumn(AppRes.FullName),
                new DataColumn("Số quyết định"),
                new DataColumn(AppRes.Company),
                new DataColumn(AppRes.OrganizationUnit),
                new DataColumn(AppRes.Office),
                new DataColumn("Trạng thái QTCT"),
                new DataColumn("Trạng thái làm việc"),
                new DataColumn("Ngày bắt đầu QTCT"),
                new DataColumn("Ngày kết thúc QTCT"),
                new DataColumn("Trạng thái duyệt"),
                new DataColumn("Người quản lý"),
                new DataColumn("HR quản lý"),
                new DataColumn(AppRes.Position),
                new DataColumn("Vị trí"),
                new DataColumn("Cấp bậc"),
                new DataColumn("Loại hợp đồng"),
                new DataColumn("Ngày bắt đầu HĐ"),
                new DataColumn("Ngày kết thúc HĐ"),
                new DataColumn("Số hợp đồng"),
                new DataColumn("Chính sách"),
                new DataColumn("Kiểu tiền tệ"),
                new DataColumn("Lương cơ bản"),
                new DataColumn("Thưởng HTCV")
            });
 
            var db = new ImportExcelDal();
            int? total = 0;
            int staffID = 0;
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
            dt.Columns[21].DataType = typeof(double);
            dt.Columns[22].DataType = typeof(double);
            var lstData = db.GetWorkingProcess(baseListParam, out total);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                    item.FullName, //họ và tên
                    item.DecisionNo,// số quyết định
                    item.CompanyName,//tên công ty
                    item.OrganizationUnitName,//tên phòng ban
                    item.OfficeName,//văn phòng
                    item.WPTypeName,//trạng thái qtct
                    item.WorkingStatusName,//trạng thái làm việc
                    String.Format("{0:dd/MM/yyyy}", item.WPStartDate), //ngày bắt đầu qtct
                    String.Format("{0:dd/MM/yyyy}", item.WPEndDate), //ngày kết thúc qtct
                    item.StatusName, //trạng thái duyệt
                    item.ManagerName,//ng quản lý
                    item.HRNames,//hr quản lý           
                    item.OfficePositionName,//chức vụ
                    item.OfficeRoleName,// vị trí
                    item.StaffLevelName,//cấp bậc
                    item.ContractTypeName,//loại hợ đồng
                    String.Format("{0:dd/MM/yyyy}", item.StartDateContract),
                    String.Format("{0:dd/MM/yyyy}", item.EndDateContract),
                    item.ContractNo,
                    item.PolicyName,
                    item.CurrencyName,
                    item.BasicPay,
                    item.EfficiencyBonus
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

            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "WorkingProcessFull.xlsx");
        }

        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "EmployeeAllowance_GetAllStaff")]
        public ActionResult EmployeeAllowanceFullExportExcel(string filterString, int pageIndex, int pageSize, int Type)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn(AppRes.FullName),
                new DataColumn("Loại phụ cấp"),
                new DataColumn(AppRes.Amount),
                new DataColumn(AppRes.Note),
                new DataColumn(AppRes.StartDate),
                new DataColumn(AppRes.EndDate),
                new DataColumn(AppRes.Company)
            });
            dt.Columns[2].DataType = typeof(double);
            var db = new ImportExcelDal();
            int? total = 0;
            int staffID = 0;
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
            var lstData = db.GetEmployeeAllowance(baseListParam, out total);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                    item.FullName,
                    item.AllowanceName,
                    item.Amount,
                    item.Note,
                    String.Format("{0:dd/MM/yyyy}", item.StartDate),
                    String.Format("{0:dd/MM/yyyy}", item.EndDate),
                    item.CompanyName
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

            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeAllowanceFull.xlsx");
        }

        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "ImportEmployeeRelationship")]
        public ActionResult UploadRelationships()
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["file-0"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var relationshipsList = new List<EmployeeRelationships>();

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var relationships = new EmployeeRelationships();
                            relationships.AutoID = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value.ToString());
                            relationships.StaffName = workSheet.Cells[rowIterator, 3].Value == null ? null : workSheet.Cells[rowIterator, 3].Value.ToString();
                            relationships.OrganizationUnit = workSheet.Cells[rowIterator, 2].Value == null ? null : workSheet.Cells[rowIterator, 2].Value.ToString();
                            relationships.Name = workSheet.Cells[rowIterator, 5].Value == null ? null : workSheet.Cells[rowIterator, 5].Value.ToString();
                            relationships.RelationshipName = workSheet.Cells[rowIterator, 4].Value == null ? null : workSheet.Cells[rowIterator, 4].Value.ToString();
                            relationships.BirthDay = workSheet.Cells[rowIterator, 6].Value==null? Convert.ToDateTime(null): Convert.ToDateTime(workSheet.Cells[rowIterator, 6].Value.ToString());
                            relationships.Phone = workSheet.Cells[rowIterator, 7].Value == null ? null : workSheet.Cells[rowIterator, 7].Value.ToString();
                            relationships.Deduction =Convert.ToBoolean(workSheet.Cells[rowIterator,8].Value.ToString());
                            relationships.DeductionCode = workSheet.Cells[rowIterator, 9].Value == null ? null : workSheet.Cells[rowIterator, 9].Value.ToString();
                            relationships.DeductionFrom = workSheet.Cells[rowIterator, 10].Value==null? Convert.ToDateTime(null) : Convert.ToDateTime(workSheet.Cells[rowIterator, 10].Value.ToString());
                            relationships.DeductionTo = workSheet.Cells[rowIterator, 11].Value == null ? Convert.ToDateTime(null) : Convert.ToDateTime(workSheet.Cells[rowIterator, 11].Value.ToString());
                            relationships.StatusName = workSheet.Cells[rowIterator, 12].Value == null ? null : workSheet.Cells[rowIterator, 12].Value.ToString();
                            relationships.Note = workSheet.Cells[rowIterator, 13].Value == null ? null : workSheet.Cells[rowIterator, 13].Value.ToString();
                            if (relationships.BirthDay == Convert.ToDateTime("01/01/0001 12:00:00 AM")){relationships.BirthDay = null; }
                            if (relationships.DeductionFrom == Convert.ToDateTime("01/01/0001 12:00:00 AM")) { relationships.DeductionFrom = null; }
                            if (relationships.DeductionTo == Convert.ToDateTime("01/01/0001 12:00:00 AM")) { relationships.DeductionTo = null; }
                            relationshipsList.Add(relationships);
                        }
                    }
                    var db = new EmployeeRelationshipsDAL();
                    var result = db.ImportExcelRelationships(relationshipsList);
                    if (result.IsSuccess == true)
                    {
                        result.Message = AppRes.ImportExcelSuccess;
                    }
                    else
                    {
                        result.Message = AppRes.ImportExcelFailed;
                    }
                    return Content(JsonConvert.SerializeObject(new
                    {
                        result
                    }));
                }
            }
            return View("Index");
        }

        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "ImportEmployeeBonus_Discipline")]
        public ActionResult Upload(int type)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["file-0"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var Bonus_DisciplineList = new List<EmployeeBonus_Discipline>();

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var bonus_Discipline = new EmployeeBonus_Discipline();
                            bonus_Discipline.AutoID = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value.ToString());

                            bonus_Discipline.StaffName = workSheet.Cells[rowIterator, 2].Value == null ? null : workSheet.Cells[rowIterator, 2].Value.ToString();
                            //bonus_Discipline.OrganizationUnit = workSheet.Cells[rowIterator, 2].Value == null ? null : workSheet.Cells[rowIterator, 2].Value.ToString();
                            bonus_Discipline.GroupName = workSheet.Cells[rowIterator, 3].Value == null ? null : workSheet.Cells[rowIterator, 3].Value.ToString();
                            bonus_Discipline.Content = workSheet.Cells[rowIterator, 4].Value == null ? null : workSheet.Cells[rowIterator, 4].Value.ToString();
                            bonus_Discipline.ActionName = workSheet.Cells[rowIterator, 5].Value == null ? null : workSheet.Cells[rowIterator, 5].Value.ToString();
                            bonus_Discipline.Amount = Convert.ToDouble(workSheet.Cells[rowIterator, 6].Value.ToString());
                            bonus_Discipline.CurrencyName = workSheet.Cells[rowIterator, 7].Value == null ? null : workSheet.Cells[rowIterator, 7].Value.ToString();
                            bonus_Discipline.DecisionNo = workSheet.Cells[rowIterator, 8].Value == null ? null : workSheet.Cells[rowIterator, 8].Value.ToString();
                            bonus_Discipline.SignDate = workSheet.Cells[rowIterator, 9].Value == null ? Convert.ToDateTime(null) : new DateTime(1899, 12, 30).AddDays(Double.Parse(workSheet.Cells[rowIterator, 9].Value.ToString()));;
                            bonus_Discipline.ApplyDate = workSheet.Cells[rowIterator, 10].Value == null ? Convert.ToDateTime(null) : new DateTime(1899, 12, 30).AddDays(Double.Parse(workSheet.Cells[rowIterator, 10].Value.ToString()));
                            bonus_Discipline.Note = workSheet.Cells[rowIterator, 11].Value == null ? null : workSheet.Cells[rowIterator, 11].Value.ToString();
                            bonus_Discipline.StaffCode = workSheet.Cells[rowIterator, 12].Value == null ? null : workSheet.Cells[rowIterator, 12].Value.ToString();
                            if (bonus_Discipline.SignDate == Convert.ToDateTime("01/01/0001 12:00:00 AM")) { bonus_Discipline.SignDate = null; }
                            if (bonus_Discipline.ApplyDate == Convert.ToDateTime("01/01/0001 12:00:00 AM")) { bonus_Discipline.ApplyDate = null; }
                            Bonus_DisciplineList.Add(bonus_Discipline);
                        }
                    }
                    var db = new EmployeeBonus_DisciplineDAL();
                    var result = db.ImportExcelBonus_Discipline(Bonus_DisciplineList, type);
                    if (result.IsSuccess == true)
                    {
                        result.Message = AppRes.ImportExcelSuccess;
                    }
                    else
                    {
                        result.Message = AppRes.ImportExcelFailed;
                    }
                    return Content(JsonConvert.SerializeObject(new
                    {
                        result
                    }));
                }
            }
            return View("Index");
        }
    }
}