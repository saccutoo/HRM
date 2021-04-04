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
    public class TimekeepingController : Controller
    {
        // GET: Timekeeping
        [Permission(TableID = (int)ETable.Timekeeping, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.Timekeeping, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "HR_WorkingDaySummary_GetManager")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, int month, int year, int userid, int status, string filter = "")
        {
            var db = new TimekeepingDal();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
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
            var result = db.GetWorkingDaySupplementManager(baseListParam, month, year, out total, out totalColumns);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId
            }));
        }

        public ActionResult SaveTimeKeeping()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.Timekeeping, TypeAction = (int)EAction.Get)]
        public ActionResult LatchesWorkDay(string listID, bool isCheckAll, int month, int year)
        {
            var db = new TimekeepingDal();
            var result = db.LatchesWorkDay(listID, isCheckAll, month, year,Global.CurrentUser.UserID, Global.CurrentUser.RoleId);

            if (isCheckAll == true && result.IsSuccess == true)
                result.Message = AppRes.LatchesWorkDayAllSusscess;
            else if (isCheckAll == false && result.IsSuccess == true)
                result.Message = AppRes.LatchesWorkDaySussess;
            else if (result.checkExisted == true && result.IsSuccess == false)
                result.Message = AppRes.TheFollowingStaffHasLatched + result.ListStaffCodeExisted;

            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Timekeeping, TypeAction = (int)EAction.Get)]
        public ActionResult LatchesWorkDayBack(string listID, bool isCheckAll, int month, int year)
        {
            var db = new TimekeepingDal();
            var result = db.LatchesWorkDayBack(listID, isCheckAll, month, year, Global.CurrentUser.UserID, Global.CurrentUser.RoleId);
            if (result.existedResult < 0)
                result.Message = "Nhân viên đã chốt phiếu lương! Không được phép chốt lại công !";
            else if (isCheckAll == true)
            {
                result.Message = "Gỡ chốt công tất cả nhân viên chưa chốt phiếu lương thành công !";
            }
            else if (isCheckAll == false)
            {
                result.Message = "Gỡ chốt công nhân viên thành công !";
            }
            //if (isCheckAll == true && result.IsSuccess == true)
            //    result.Message = AppRes.LatchesWorkDayAllSusscess;
            //else if (isCheckAll == false && result.IsSuccess == true)
            //    result.Message = AppRes.LatchesWorkDaySussess;
            //else if (result.checkExisted == true && result.IsSuccess == false)
            //    result.Message = AppRes.TheFollowingStaffHasLatched + result.ListStaffCodeExisted;

            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.Timekeeping, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "TimeKeeping_Save")]

        public ActionResult _SaveTimeKeeping(SalaryWorkingday obj)
        {
            var db = new TimekeepingDal();
            var result = db.SaveTimeKeeping(obj);
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

        [Permission(TableID = (int)ETable.Timekeeping, TypeAction = (int)EAction.Get)]
        public ActionResult GetEditItemById(int id, int month, int year, int idTable)
        {
            var db = new TimekeepingDal();
            var result = db.GetHRWorkingDayById(Global.CurrentUser.RoleId, idTable, id, month, year);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Timekeeping, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "HR_WorkingDaySummary_GetManager")]

        public ActionResult TimeKeepingExportExcel(int pageIndex, int pageSize, int month, int year, string filter = "")
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[15]
            {
                  new DataColumn(AppRes.Month),
                    new DataColumn(AppRes.Year),
                new DataColumn(AppRes.Timekeeping_CreateName),
                 new DataColumn(AppRes.StaffCode),
                new DataColumn(AppRes.Timekeeping_DepartmentName),
               new DataColumn(AppRes.Standardworkingday),
               new DataColumn(AppRes.Timekeeping_TotalTitle),
                new DataColumn(AppRes.Timekeeping_MinutePenalty),
                new DataColumn(AppRes.Timekeeping_TotalTitle2),
               new DataColumn(AppRes.Timekeeping_Supplement),
               new DataColumn(AppRes.Timekeeping_Furlough),
                new DataColumn(AppRes.Timekeeping_WorkingMore),
                new DataColumn(AppRes.Timekeeping_WorkingDayLeave),
               new DataColumn(AppRes.WorkingAdjusted),
               new DataColumn(AppRes.NoteAdjusted)


            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(int);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(double);
            dt.Columns[6].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(string);
            dt.Columns[8].DataType = typeof(double);
            dt.Columns[9].DataType = typeof(double);
            dt.Columns[10].DataType = typeof(double);
            dt.Columns[11].DataType = typeof(double);
            dt.Columns[12].DataType = typeof(double);
            dt.Columns[13].DataType = typeof(double);
            dt.Columns[14].DataType = typeof(string);

            var db = new TimekeepingDal();
            int? total = 0;
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = int.MaxValue,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var lstData = db.ExportExcelTimeKeeping(baseListParam, month, year, out total);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                    month,
                    year,
                    item.Fullname == null ? "" : item.Fullname,
                    item.StaffCode == null ? "" : item.StaffCode,
                    item.OrganizationUnitName == "" ? null : item.OrganizationUnitName,
                    item.DaysOfMonth == null ? 0 : item.DaysOfMonth,
                    item.TotalWorkingDay == null ? 0 : item.TotalWorkingDay,
                    item.TotalHourDuration == null ? "" : (item.TotalHourDuration).Substring(0, 5),
                    item.WorkingDay == null ? 0 : item.WorkingDay,
                    item.WorkingDaySupplement == null ? 0 : item.WorkingDaySupplement,
                    item.Furlough == null ? 0 : item.Furlough,
                    item.Overtime == null ? 0 : item.Overtime,
                    item.WorkingDayLeave == null ? 0 : item.WorkingDayLeave,
                    item.WorkingAdjusted == null ? null : item.WorkingAdjusted,
                    item.NoteAdjusted == null ? "" : item.NoteAdjusted

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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TimeKeeping.xlsx");
        }

        [Permission(TableID = (int)ETable.Timekeeping, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "ImportExcelSalaryWorkingday")]

        public ActionResult Upload()
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
                    var salaryWorkingdayList = new List<SalaryWorkingday>();

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        if (currentSheet.Count() > 0)
                        {
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                var salaryWorkingday = new SalaryWorkingday();
                                salaryWorkingday.Month = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value.ToString()); //int
                                salaryWorkingday.Year = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value.ToString()); //int
                                salaryWorkingday.StaffCode = workSheet.Cells[rowIterator, 4].Value.ToString(); //string
                                salaryWorkingday.WorkingAdjusted = workSheet.Cells[rowIterator, 14].Value == null ? (double?)null : Convert.ToDouble(workSheet.Cells[rowIterator, 14].Value.ToString()); //double
                                salaryWorkingday.NoteAdjusted = workSheet.Cells[rowIterator, 15].Value == null ? null : workSheet.Cells[rowIterator, 15].Value.ToString(); //string
                                salaryWorkingdayList.Add(salaryWorkingday);
                            }
                        }
                    }
                    var db = new TimekeepingDal();
                    var result = db.ImportExcelSalaryWorkingday(salaryWorkingdayList);
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