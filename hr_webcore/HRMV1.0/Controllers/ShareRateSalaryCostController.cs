using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Framework.DataBusiness.Common;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.Constants;
using HRM.DataAccess.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Logger;
using HRM.Security;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;
using HRM.DataAccess.Entity.UserDefinedType;
using OfficeOpenXml;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class ShareRateSalaryCostController : Controller
    {
        // GET: ShareRateForPerformanceReport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Staff()
        {
            ViewBag.url = "/ShareRateSalaryCost/GetShareRateSalaryCostByStaff";
            ViewBag.Title = AppRes.ShareRateForPerformanceReportbyStaff;
            return View();
        }
        public ActionResult Dept()
        {
            ViewBag.url = "/ShareRateSalaryCost/GetShareRateSalaryCostByDept";
            ViewBag.Title = AppRes.ShareRateForPerformanceReportbyOrganizationUnit;
            return View();
        }
        [WriteLog(Action = Constant.EAction.Get, LogStoreProcedure = "GetShareRateSalaryCostByStaff")]
        public ActionResult GetShareRateSalaryCostByStaff(int pageIndex, string pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            var db = new ShareRateSalaryCostDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.UserID,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = Int32.Parse(pageSize),
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetShareRateSalaryCostByStaff(baseListParam,0, list, out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId

            }));
        }

        [WriteLog(Action = Constant.EAction.Get, LogStoreProcedure = "GetShareRateSalaryCostByDept")]
        public ActionResult GetShareRateSalaryCostByDept(int pageIndex, string pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            var db = new ShareRateSalaryCostDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.UserID,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = Int32.Parse(pageSize),
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetShareRateSalaryCostByDept(baseListParam,0, list, out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId

            }));
        }
        public ActionResult GetListCompany(string id)
        {
            var db = new OrganizationUnitDAL();
            var result = db.GetCompany();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [WriteLog(Action = Constant.EAction.Edit, LogStoreProcedure = "SaveShareRateSalaryCost")]
        public ActionResult Save(ShareRateSalaryCost obj)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.UserID,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new ShareRateSalaryCostDAL();
            var result = db.Save(baseListParam, obj);
            var msg = result.Message = result.IsSuccess == true ? AppRes.Success : AppRes.ValidateDateIsExist;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [WriteLog(Action = Constant.EAction.Edit, LogStoreProcedure = "SaveShareRateSalaryCostBySatff")]
        public ActionResult SaveByStaff(ShareRateSalaryCost obj)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.UserID,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new ShareRateSalaryCostDAL();
            var result = db.SaveByStaff(baseListParam, obj);
            var msg = result.Message = result.IsSuccess == true ? AppRes.Success : AppRes.ValidateDateIsExist;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [WriteLog(Action = Constant.EAction.Edit, LogStoreProcedure = "SaveShareRateSalaryCostByDept")]
        public ActionResult SaveByDept(ShareRateSalaryCost obj)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.UserID,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new ShareRateSalaryCostDAL();
            var result = db.SaveByDept(baseListParam, obj);
            var msg = result.Message = result.IsSuccess == true ? AppRes.Success : AppRes.ValidateDateIsExist;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [WriteLog(Action = Constant.EAction.Edit, LogStoreProcedure = "DeleteShareRateSalaryCost")]
        public ActionResult Delete(int id, int idTable)
        {
            var obj = new ShareRateSalaryCost()
            {
                ModifiedBy = Global.CurrentUser.UserID,
                Id = id,
                Type = idTable
            };

            obj.ModifiedBy = Global.CurrentUser.UserID;
            var db = new ShareRateSalaryCostDAL();
            var result = db.Delete(obj);
            var msg = result.Message = result.IsSuccess == true ? AppRes.Success : AppRes.ValidateDateIsExist;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [WriteLog(Action = Constant.EAction.Edit, LogStoreProcedure = "DeleteShareRateSalaryCostByStaff")]
        public ActionResult DeleteShareRateSalaryCostByStaff(int id, int idTable)
        {
            var obj = new ShareRateSalaryCost()
            {
                ModifiedBy = Global.CurrentUser.UserID,
                Id = id,
                Type = idTable
            };

            obj.ModifiedBy = Global.CurrentUser.UserID;
            var db = new ShareRateSalaryCostDAL();
            var result = db.DeleteShareRateSalaryCostByStaff(obj);
            var msg = result.Message = result.IsSuccess == true ? AppRes.Success : AppRes.ValidateDateIsExist;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [WriteLog(Action = Constant.EAction.Edit, LogStoreProcedure = "DeleteShareRateSalaryCostByDept")]
        public ActionResult DeleteShareRateSalaryCostByDept(int id, int idTable)
        {
            var obj = new ShareRateSalaryCost()
            {
                ModifiedBy = Global.CurrentUser.UserID,
                Id = id,
                Type = idTable
            };

            obj.ModifiedBy = Global.CurrentUser.UserID;
            var db = new ShareRateSalaryCostDAL();
            var result = db.DeleteShareRateSalaryCostByDept(obj);
            var msg = result.Message = result.IsSuccess == true ? AppRes.Success : AppRes.ValidateDateIsExist;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult GetItemById(int id, int CustomerID) //CustomerID la type(type = 1 la bang ShareRateForPerformanceReportBystaff), vi su dung lai ham cu nen de la CustomerID
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.UserID,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new ShareRateSalaryCostDAL();
            var result = db.ShareRateSalaryCostGetByID(baseListParam, id, CustomerID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult ShareRateSalaryCostDeptGetByID(int id) //CustomerID la type(type = 1 la bang ShareRateForPerformanceReportBystaff), vi su dung lai ham cu nen de la CustomerID
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.UserID,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new ShareRateSalaryCostDAL();
            var result = db.ShareRateSalaryCostDeptGetByID(baseListParam,id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult ShareRateSalaryCostStaffGetByID(int id) //CustomerID la type(type = 1 la bang ShareRateForPerformanceReportBystaff), vi su dung lai ham cu nen de la CustomerID
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.UserID,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new ShareRateSalaryCostDAL();
            var result = db.ShareRateSalaryCostStaffGetByID(baseListParam, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        public ActionResult ExportExcel(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            DataTable dt = new DataTable("Grid");
            if (list.filter7 == "1")
            {
                dt.Columns.AddRange(new DataColumn[7]
                {
                    new DataColumn(AppRes.Staff),
                    new DataColumn(AppRes.OrganizationUnit),
                    new DataColumn(AppRes.ShareRate),
                    new DataColumn(AppRes.StartDate),
                    new DataColumn(AppRes.EndDate),
                    new DataColumn(AppRes.Note),
                    new DataColumn(AppRes.Company)

                });
                dt.Columns[0].DataType = typeof(string);
                dt.Columns[1].DataType = typeof(string);
                dt.Columns[2].DataType = typeof(double);
                dt.Columns[3].DataType = typeof(DateTime);
                dt.Columns[4].DataType = typeof(DateTime);
                dt.Columns[5].DataType = typeof(string);
                dt.Columns[6].DataType = typeof(string);
            }
            else
            {
                dt.Columns.AddRange(new DataColumn[6]
                 {
                    new DataColumn(AppRes.OrganizationUnit),
                    new DataColumn(AppRes.ShareRate),
                    new DataColumn(AppRes.StartDate),
                    new DataColumn(AppRes.EndDate),
                    new DataColumn(AppRes.Note),
                    new DataColumn(AppRes.Company)

                 });
                dt.Columns[0].DataType = typeof(string);
                dt.Columns[1].DataType = typeof(double);
                dt.Columns[2].DataType = typeof(DateTime);
                dt.Columns[3].DataType = typeof(DateTime);
                dt.Columns[4].DataType = typeof(string);
                dt.Columns[5].DataType = typeof(string);
            }
            var db = new ShareRateSalaryCostDAL();
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.UserID,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };

            list.FromDate = Convert.ToDateTime(DateTime.ParseExact(list.StringFromDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            list.ToDate = Convert.ToDateTime(DateTime.ParseExact(list.StringToDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));

            var lstData = new List<ShareRateSalaryCost>();
            if (list.filter7 == "1")
            {
                lstData = db.GetShareRateSalaryCostByStaff(baseListParam, 1, list, out total, out totalColumns);
            }
            else
            {
                lstData = db.GetShareRateSalaryCostByDept(baseListParam, 1, list, out total, out totalColumns);
            }
            
            foreach (var item in lstData)
            {

                if (list.filter7 == "1")
                {
                    dt.Rows.Add(
                    item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                    item.StaffName == null ? "" : item.StaffName,
                    Double.Parse(String.Format("{0:###0.##}", item.ShareRate)),
                    item.StartDate,
                    item.EndDate,
                    item.Note == null ? "" : item.Note,
                    item.CompanyName == null ? "" : item.CompanyName);
                }
                else
                {
                    dt.Rows.Add(
                    item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                    Double.Parse(String.Format("{0:###0.##}", item.ShareRate)),
                    item.StartDate,
                    item.EndDate,
                    item.Note == null ? "" : item.Note,
                    item.CompanyName == null ? "" : item.CompanyName);
                }

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
            switch (list.filter7)
            {
                case "1":
                    // code block
                    excelName = "Ty le share bao cao chi phi luong theo nhan vien.xlsx";
                    break;
                default:
                    // code block
                    excelName = "Ty le share bao cao chi phi luong theo phong ban.xlsx";
                    break;
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }


        public virtual ActionResult DownloadTemplate(string formatFile, int? param)
        {
            if (String.IsNullOrEmpty(formatFile))
            {
                formatFile = ".xlsx";
            }
            //var globalListDal = new GlobalListDal();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[16]
            {
                new DataColumn(AppRes.StaffIDD),
                new DataColumn(AppRes.OrganazationId),
                new DataColumn(AppRes.ShareRate),
                new DataColumn(AppRes.StartDate),
                new DataColumn(AppRes.EndDate),
                new DataColumn(AppRes.CompanyId),
                new DataColumn(AppRes.Note),
                new DataColumn(""),
                new DataColumn(""),
                new DataColumn(AppRes.CompanyId+" "),
                new DataColumn(AppRes.Company+" "),
                new DataColumn(""),
                new DataColumn(AppRes.StaffIDD+" "),
                new DataColumn(AppRes.Staff+"  "),
                new DataColumn(AppRes.OrganazationId+" "),
                new DataColumn(AppRes.OrganizationUnit+" ")

            });
            var db = new OrganizationUnitDAL();
            var listCompany = db.GetCompany();
            var dborg = new OrganizationUnitDAL();
            var listStaff = dborg.GetEmployeeByStatusAndDept(Global.CurrentLanguage, 0, Constant.numStatusStaff.Activity.GetHashCode(), Global.CurrentUser.UserID, Global.CurrentUser.RoleId);
            var listOrganizationUnit = dborg.OrganizationUnitAll(1, Global.CurrentUser.RoleId, Global.CurrentUser.UserID);
            var rowsNumber = 0;
            rowsNumber = Math.Max(listCompany.Count, Math.Max(listStaff.Count, listOrganizationUnit.Count));

            dt.Rows.Add(
                    1,
                    1230,
                    50,
                    DateTime.Now,
                    DateTime.Now,
                    1221,
                    Global.CurrentLanguage == 4 ? "Note" : "Ghi chú"
                );

            for (int i = 0; i < rowsNumber; i++)
            {
                dt.Rows.Add(
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    listCompany.Count > i ? listCompany[i].OrganizationUnitID.ToString() : "", listCompany.Count > i ? listCompany[i].Name.ToString() : "",
                    "",
                    listStaff.Count > i ? listStaff[i].StaffID.ToString() : "", listStaff.Count > i ? listStaff[i].FullName.ToString() : "",
                    listOrganizationUnit.Count > i ? listOrganizationUnit[i].OrganizationUnitID.ToString() : "", listOrganizationUnit.Count > i ? listOrganizationUnit[i].Name.ToString() : ""

                );
            }
            
            if (param == 2)
            {
                dt.Columns.Remove(AppRes.StaffIDD + " ");
                dt.Columns.Remove(AppRes.Staff + "  ");
                dt.Columns.Remove(AppRes.StaffIDD);
            }
            else
            {
                dt.Columns.Remove(AppRes.OrganazationId + " "); 
                dt.Columns.Remove(AppRes.OrganizationUnit + " ");
                dt.Columns.Remove(AppRes.OrganazationId);
            }
           
            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            wb.Worksheet("Grid").Cell(1, 7).Style.Fill.BackgroundColor = XLColor.White;
            wb.Worksheet("Grid").Cell(1, 8).Style.Fill.BackgroundColor = XLColor.White;
            wb.Worksheet("Grid").Cell(1, 11).Style.Fill.BackgroundColor = XLColor.White;

            

            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ImportFileTemplate"+ formatFile);
        }

        public ActionResult ImportExcel()
        {
            if (Global.CurrentUser.RoleId != 1 && Global.CurrentUser.RoleId != 28 && Global.CurrentUser.RoleId != 29 &&
                Global.CurrentUser.RoleId != 3)
            {
                HttpContext.Response.Redirect("/AccessDenied/Index");
            }
            //var listDataChecked = new List<BdImportCustomerCheckExistedResult>();
            var type = 1;
            var result = new List<ShareRateSalaryCostType>();

            if (Request != null)
            {
                var db = new ShareRateSalaryCostDAL();
                var listRow = new List<ShareRateSalaryCostType>();
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        type = Int32.Parse(Request.Form["Type"]);
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();

                            var noOfRow = workSheet.Dimension.End.Row;
                            var roleid = Global.CurrentUser.RoleId;
                            var officePositionId = Global.CurrentUser.OfficePositionID;
                            var staffId = Global.CurrentUser.UserID;
                            for (int i = 2; i <= noOfRow; i++)
                            {

                                var row = new ShareRateSalaryCostType()
                                {
                                    Id = i,
                                    OrganizationUnitID = type == 1 ? 0 : Int32.Parse((workSheet.Cells[i, 1].Value ?? 0).ToString()),
                                    StaffId = type == 2 ? 0 : Int32.Parse((workSheet.Cells[i, 1].Value ?? 0).ToString()),
                                    ShareRate = float.Parse((workSheet.Cells[i, 2].Value ?? 0).ToString()),
                                    StartDate = workSheet.Cells[i, 3].Value == null ? DateTime.MinValue : DateTime.Parse((workSheet.Cells[i, 3].Value ?? 0).ToString()),
                                    EndDate = workSheet.Cells[i, 4].Value == null ? DateTime.MinValue : DateTime.Parse((workSheet.Cells[i, 4].Value ?? 0).ToString()),
                                    CompanyId = Int32.Parse((workSheet.Cells[i, 5].Value ?? 0).ToString()),
                                    CreatedBy = Global.CurrentUser.UserID,
                                    ModifiedBy = Global.CurrentUser.UserID,
                                    Status = 1,
                                    Note = workSheet.Cells[i, 6].Value == null ? "" : workSheet.Cells[i, 6].Value.ToString(),
                                    Type = type,
                                    Result = "",

                                };
                                row.StartDate = row.StartDate == DateTime.MinValue ? null : row.StartDate;
                                row.EndDate = row.EndDate == DateTime.MinValue ? null : row.EndDate;
                                listRow.Add(row);


                            }
                            package.Dispose();
                        }
                    }

                    if (type == 2)
                    {
                        result = db.ImportShareRateSalaryCostByDept(listRow, Global.CurrentUser.UserID, Global.CurrentLanguage, Global.CurrentUser.RoleId);
                    }
                    else
                    {
                        result = db.ImportShareRateSalaryCostByStaff(listRow, Global.CurrentUser.UserID, Global.CurrentLanguage, Global.CurrentUser.RoleId);
                    }
                }
            }
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8]
            {
                new DataColumn(AppRes.StaffIDD),
                new DataColumn(AppRes.OrganazationId),
                new DataColumn(AppRes.ShareRate),
                new DataColumn(AppRes.StartDate),
                new DataColumn(AppRes.EndDate),
                new DataColumn(AppRes.CompanyId),
                new DataColumn(AppRes.Note),
                new DataColumn(AppRes.Result),
            });
            
            foreach (var item in result)
            {
                dt.Rows.Add(
                    item.StaffId,
                    item.OrganizationUnitID,
                    item.ShareRate,
                    item.StartDate,
                    item.EndDate,
                    item.CompanyId,
                    item.Note,
                    item.Result
                );
            }
            if (type == 2)
            {
                dt.Columns.Remove(AppRes.StaffIDD);
            }
            else
            {
                dt.Columns.Remove(AppRes.OrganazationId);
            }
            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            
            string handle = Guid.NewGuid().ToString();
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                stream.Position = 0;
                //data = stream.ToArray();
                TempData[handle] = stream.ToArray();
            }

            return new JsonResult()
            {
                Data = new { FileGuid = handle, FileName = Global.CurrentLanguage == 4? "Result.xlsx" : "Ketqua.xlsx" }
            };
        }

        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.ms-excel", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }
    }
}