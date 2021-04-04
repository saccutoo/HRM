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
using OfficeOpenXml;
using HRM.DataAccess.Entity.UserDefinedType;
namespace HRM.Controllers
{
    public class StaffPlanImplementationController : Controller
    {
        // GET: StaffPlanImplementation
        [Permission(TableID = (int)ETable.StaffPlanImplementation, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/StaffPlanImplementation/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.StaffPlanImplementation, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "StaffPlanImplementation_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new StaffPlanImplementation_DAL();
            ToTalMonth ToTalMonth = new ToTalMonth();
            int total = 0;
            int LanguageCode = Global.CurrentLanguage;
            var result = db.StaffPlanImplementation_GetList(pageIndex, pageSize, filter, LanguageCode, out total, out ToTalMonth);
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            lstTotal.Total4 = "45";
            lstTotal.Total5 = "55";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal,
                ToTalMonth = ToTalMonth
            }));
        }
        [Permission(TableID = (int)ETable.StaffPlanImplementation, TypeAction = (int)EAction.Get)]

        public ActionResult StaffPlanImplementationSave()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.StaffPlanImplementation, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "StaffPlanImplementation_Save")]

        public ActionResult StaffPlanImplementation_Save(List<StaffPlan> data)
        {

            var db = new StaffPlanImplementation_DAL();
            var result = new SystemMessage();
            foreach (var item in data)
            {
                item.Type = 0;
                item.CreatedBy = Global.CurrentUser.UserID;
                item.CreatedOn = DateTime.Now;
                item.CreatedOn = DateTime.Now;
                item.ModifiedBy = Global.CurrentUser.UserID;
                result = db.StaffPlanImplementation_Save(item);
            }

            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.StaffPlanImplementation, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "StaffPlanImplementation_Delete")]

        public ActionResult StaffPlanImplementation_Delete(int ID)
        {

            var db = new StaffPlanImplementation_DAL();
            var result = db.StaffPlanImplementation_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.StaffPlanImplementation, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "StaffPlanImplementation_GetList")]

        public ActionResult StaffPlanImplementationExportExcel(string filter = "")
        {
            DataTable dt = new DataTable("Grid");
            if (filter.Contains("!!") == true)
            {
                filter = filter.Replace("!!", "%");
            }
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(AppRes.StaffName),
                new DataColumn(AppRes.OrganizationUnit),
                new DataColumn(AppRes.Currency),
                new DataColumn(AppRes.Contract),
                new DataColumn(AppRes.Year),
                new DataColumn(AppRes.StatusName),
                new DataColumn(AppRes.L1),
                new DataColumn(AppRes.L2),
                new DataColumn(AppRes.L3),
                new DataColumn(AppRes.L4),
                new DataColumn(AppRes.L5),
                new DataColumn(AppRes.L6),
                new DataColumn(AppRes.L7),
                new DataColumn(AppRes.L8),
                new DataColumn(AppRes.L9),
                new DataColumn(AppRes.L10),
                new DataColumn(AppRes.L11),
                new DataColumn(AppRes.L12),
                new DataColumn(AppRes.R_Total),
                new DataColumn(AppRes.StatusInput),
            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(double);
            dt.Columns[8].DataType = typeof(double);
            dt.Columns[9].DataType = typeof(double);
            dt.Columns[10].DataType = typeof(double);
            dt.Columns[11].DataType = typeof(double);
            dt.Columns[12].DataType = typeof(double);
            dt.Columns[13].DataType = typeof(double);
            dt.Columns[14].DataType = typeof(double);
            dt.Columns[15].DataType = typeof(double);
            dt.Columns[16].DataType = typeof(double);
            dt.Columns[17].DataType = typeof(double);
            dt.Columns[18].DataType = typeof(double);
            dt.Columns[19].DataType = typeof(string);

            var db = new StaffPlanImplementation_DAL();
            ToTalMonth ToTalMonth = new ToTalMonth();

            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.StaffPlanImplementation_GetList(1, 100000, filter, LanguageCode, out total, out ToTalMonth);
            if (lstData != null && lstData.Count() > 0)
            {
                foreach (var item in lstData)
                {
                    dt.Rows.Add(item.StaffName, item.OrganizationUnitName, item.CurrencyName, item.ContractTypeName, item.Year, item.StatusName, item.M1, item.M2, item.M3, item.M4, item.M5, item.M6, item.M7, item.M8, item.M9, item.M10, item.M11, item.M12, item.SumValue, item.StatusInputName);
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", " StaffPlanImplementation.xlsx");
        }

        [Permission(TableID = (int)ETable.StaffPlanImplementation, TypeAction = (int)EAction.Excel)]
        public ActionResult DownloadTemplate()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(AppRes.CodeUser),
                new DataColumn(AppRes.StaffName),
                new DataColumn(AppRes.OrganazationId),
                new DataColumn(AppRes.Department),
                new DataColumn(AppRes.ContractCode),
                new DataColumn(AppRes.Year),
                new DataColumn(AppRes.StatusCode),
                new DataColumn(AppRes.L1),
                new DataColumn(AppRes.L2),
                new DataColumn(AppRes.L3),
                new DataColumn(AppRes.L4),
                new DataColumn(AppRes.L5),
                new DataColumn(AppRes.L6),
                new DataColumn(AppRes.L7),
                new DataColumn(AppRes.L8),
                new DataColumn(AppRes.L9),
                new DataColumn(AppRes.L10),
                new DataColumn(AppRes.L11),
                new DataColumn(AppRes.L12),
                new  DataColumn(""),
                new  DataColumn(""),
                new DataColumn(AppRes.ColumnName),
                new DataColumn(AppRes.RequestContent),
                new  DataColumn(""),
                 new DataColumn(AppRes.ContractCode + " "),
                new DataColumn(AppRes.ContactTypeID + " " + AppRes.Contract),
                new DataColumn(""),
                new DataColumn(AppRes.StatusCode + " "),
                new DataColumn(AppRes.StatusList),
            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(string);
            dt.Columns[7].DataType = typeof(string);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(string);
            dt.Columns[10].DataType = typeof(string);
            dt.Columns[11].DataType = typeof(string);
            dt.Columns[12].DataType = typeof(string);
            dt.Columns[13].DataType = typeof(string);
            dt.Columns[14].DataType = typeof(string);
            dt.Columns[15].DataType = typeof(string);
            dt.Columns[16].DataType = typeof(string);
            dt.Columns[17].DataType = typeof(string);
            dt.Columns[18].DataType = typeof(string);
            dt.Columns[19].DataType = typeof(string);
            dt.Columns[20].DataType = typeof(string);
            dt.Columns[21].DataType = typeof(string);
            dt.Columns[22].DataType = typeof(string);
            dt.Columns[23].DataType = typeof(string);
            dt.Columns[24].DataType = typeof(string);
            dt.Columns[25].DataType = typeof(string);
            dt.Columns[26].DataType = typeof(string);
            dt.Columns[27].DataType = typeof(string);
            dt.Columns[28].DataType = typeof(string);
            var db = new OrganizationUnitPlanImplementation_DAL();
            ToTalMonth ToTalMonth = new ToTalMonth();
            var commonDAL = new CommonDal();
            var listContratType = commonDAL.GetsWhereParentIDnotTree(1949, Global.CurrentLanguage);
            var listStatus = commonDAL.GetsWhereParentIDnotTree(88, Global.CurrentLanguage);

            List<string> ListColumn = new List<string>()
            { AppRes.CodeUser,AppRes.StaffName,AppRes.OrganazationId,AppRes.Department,AppRes.ContractCode,AppRes.Year,AppRes.StatusName,AppRes.L1,AppRes.L2
            ,AppRes.L3,AppRes.L4,AppRes.L5,AppRes.L6,AppRes.L7,AppRes.L8,AppRes.L9
            ,AppRes.L10,AppRes.L11,AppRes.L12
            };

            List<string> ListRequest = new List<string>()
            {
                AppRes.RequestStaffCode,AppRes.RequestStaffName,AppRes.RequestStaffCode,AppRes.RequestStaffName,AppRes.RequestContractCode,AppRes.RequestYear
                ,AppRes.RequestStaffCode,AppRes.RequestMonthInYear,AppRes.RequestMonthInYear
                ,AppRes.RequestMonthInYear,AppRes.RequestMonthInYear,AppRes.RequestMonthInYear,AppRes.RequestMonthInYear,AppRes.RequestMonthInYear,AppRes.RequestMonthInYear
                ,AppRes.RequestMonthInYear,AppRes.RequestMonthInYear,AppRes.RequestMonthInYear,AppRes.RequestMonthInYear
            };

            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var rowsNumber = 0;
            if (ListColumn.Count > listContratType.Count)
            {
                rowsNumber = ListColumn.Count;
            }
            else
            {
                rowsNumber = listContratType.Count;
            }
            for (int i = 0; i < rowsNumber; i++)
            {
                dt.Rows.Add(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ListColumn.Count() > i ? ListColumn[i] : " ", ListRequest.Count() > i ? ListRequest[i] : " ", " ", listContratType.Count() > i ? listContratType[i].GlobalListID.ToString() : " ", listContratType.Count > i ? (Global.CurrentLanguage == 5 ? listContratType[i].Name : listContratType[i].NameEN) : " ", " ", listStatus.Count() > i ? listStatus[i].Value.ToString() : " ", listStatus.Count() > i ? (Global.CurrentLanguage == 5 ? listStatus[i].Name : listStatus[i].NameEN) : " ");
            }
            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);

            wb.Worksheet("Grid").Column(20).Style.Fill.BackgroundColor = XLColor.White;
            wb.Worksheet("Grid").Column(21).Style.Fill.BackgroundColor = XLColor.White;
            wb.Worksheet("Grid").Column(24).Style.Fill.BackgroundColor = XLColor.White;
            wb.Worksheet("Grid").Column(27).Style.Fill.BackgroundColor = XLColor.White;

            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TemplateStaffPlanImplementation.xlsx");
        }
        [Permission(TableID = (int)ETable.StaffPlanImplementation, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "ImPortStaffPlanImplementation")]
        public ActionResult ImportExcel()
        {
            try
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
                        var List = new List<StaffPlan>();

                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 1].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 2].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 2].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 3].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 3].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 4].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 4].Value.ToString().Trim()) || workSheet.Cells[rowIterator, 5].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 5].Value.ToString().Trim()) || workSheet.Cells[rowIterator, 6].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 6].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 7].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 7].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 8].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 8].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 9].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 9].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 10].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 10].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 11].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 11].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 12].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 12].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 13].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 13].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 14].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 14].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 15].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 15].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 16].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 16].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 17].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 17].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 18].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 18].Value.ToString().Trim()) ||
                                    workSheet.Cells[rowIterator, 19].Value != null && !string.IsNullOrEmpty(workSheet.Cells[rowIterator, 19].Value.ToString().Trim()))
                                {
                                    var StaffPlan = new StaffPlan();
                                    StaffPlan.AutoID = 0;
                                    StaffPlan.StaffCode = workSheet.Cells[rowIterator, 1].Value == null ? null : workSheet.Cells[rowIterator, 1].Value.ToString();
                                    StaffPlan.StaffName = workSheet.Cells[rowIterator, 2].Value == null ? null : workSheet.Cells[rowIterator, 2].Value.ToString();
                                    StaffPlan.OrganizationUnitCode = workSheet.Cells[rowIterator, 3].Value == null ? null : workSheet.Cells[rowIterator, 3].Value.ToString();
                                    StaffPlan.OrganizationUnitName = workSheet.Cells[rowIterator, 4].Value == null ? null : workSheet.Cells[rowIterator, 4].Value.ToString();
                                    StaffPlan.ContractType = workSheet.Cells[rowIterator, 5].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 5].Value.ToString().Trim()) ? 0 : Convert.ToInt32(workSheet.Cells[rowIterator, 5].Value.ToString());
                                    StaffPlan.Year = workSheet.Cells[rowIterator, 6].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 6].Value.ToString().Trim()) ? 0 : Convert.ToInt32(workSheet.Cells[rowIterator, 6].Value.ToString());
                                    StaffPlan.Status = workSheet.Cells[rowIterator, 7].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 7].Value.ToString().Trim()) ? 10 : Convert.ToInt32(workSheet.Cells[rowIterator, 7].Value.ToString());
                                    StaffPlan.M1 = workSheet.Cells[rowIterator, 8].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 8].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 8].Value.ToString());
                                    StaffPlan.M2 = workSheet.Cells[rowIterator, 9].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 9].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 9].Value.ToString());
                                    StaffPlan.M3 = workSheet.Cells[rowIterator, 10].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 10].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 10].Value.ToString());
                                    StaffPlan.M4 = workSheet.Cells[rowIterator, 11].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 11].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 11].Value.ToString());
                                    StaffPlan.M5 = workSheet.Cells[rowIterator, 12].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 12].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 12].Value.ToString());
                                    StaffPlan.M6 = workSheet.Cells[rowIterator, 13].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 13].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 13].Value.ToString());
                                    StaffPlan.M7 = workSheet.Cells[rowIterator, 14].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 14].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 14].Value.ToString());
                                    StaffPlan.M8 = workSheet.Cells[rowIterator, 15].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 15].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 15].Value.ToString());
                                    StaffPlan.M9 = workSheet.Cells[rowIterator, 16].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 16].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 16].Value.ToString());
                                    StaffPlan.M10 = workSheet.Cells[rowIterator, 17].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 17].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 17].Value.ToString());
                                    StaffPlan.M11 = workSheet.Cells[rowIterator, 18].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 18].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 18].Value.ToString());
                                    StaffPlan.M12 = workSheet.Cells[rowIterator, 19].Value == null || string.IsNullOrEmpty(workSheet.Cells[rowIterator, 19].Value.ToString().Trim()) ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 19].Value.ToString());
                                    StaffPlan.StatusInput = 0;
                                    StaffPlan.CreatedBy = Global.CurrentUser.UserID;
                                    StaffPlan.ModifiedBy = Global.CurrentUser.UserID;
                                    StaffPlan.CreatedOn = DateTime.Now;
                                    StaffPlan.ModifiedOn = DateTime.Now;
                                    List.Add(StaffPlan);
                                }

                            }
                        }
                        var db = new StaffPlanImplementation_DAL();
                        SystemMessage SM = new SystemMessage();
                        bool IsSuccess = true;
                        if (List.Count() <= 0)
                        {
                            SM.IsSuccess = false;
                            SM.Message = AppRes.ErrorNoHasData;
                            return Content(JsonConvert.SerializeObject(new
                            {
                                SM
                            }));
                        }
                        var result = db.ImPortStaffPlanImplementation(List, out IsSuccess);
                        if (result.Count() > 0)
                        {
                            Session["data"] = result;
                        }
                        if (IsSuccess == true)
                        {
                            SM.IsSuccess = true;
                            SM.Message = AppRes.Success;
                        }
                        else
                        {
                            SM.IsSuccess = false;
                            SM.Message = AppRes.ErrorImportOrganizationUnitPlanImplementation;
                        }
                        return Content(JsonConvert.SerializeObject(new
                        {
                            result,
                            SM
                        }));
                    }
                }
            }
            catch (Exception)
            {
                var SM = new SystemMessage();
                SM.IsSuccess = false;
                SM.Message = AppRes.ErrorFileExcel;
                return Content(JsonConvert.SerializeObject(new
                {
                    SM,
                }));
            }

            return View();
        }

        public ActionResult ExportStaffPlanImplementationDataError()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[]
            {
                 new DataColumn(AppRes.CodeUser),
                new DataColumn(AppRes.StaffName),
                new DataColumn(AppRes.OrganazationId),
                new DataColumn(AppRes.Department),
                new DataColumn(AppRes.ContractCode),
                new DataColumn(AppRes.Year),
                new DataColumn(AppRes.StatusCode),
                new DataColumn(AppRes.L1),
                new DataColumn(AppRes.L2),
                new DataColumn(AppRes.L3),
                new DataColumn(AppRes.L4),
                new DataColumn(AppRes.L5),
                new DataColumn(AppRes.L6),
                new DataColumn(AppRes.L7),
                new DataColumn(AppRes.L8),
                new DataColumn(AppRes.L9),
                new DataColumn(AppRes.L10),
                new DataColumn(AppRes.L11),
                new DataColumn(AppRes.L12),
                new  DataColumn(AppRes.Note),
            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(int);
            dt.Columns[5].DataType = typeof(int);
            dt.Columns[6].DataType = typeof(int);
            dt.Columns[7].DataType = typeof(double);
            dt.Columns[8].DataType = typeof(double);
            dt.Columns[9].DataType = typeof(double);
            dt.Columns[10].DataType = typeof(double);
            dt.Columns[11].DataType = typeof(double);
            dt.Columns[12].DataType = typeof(double);
            dt.Columns[13].DataType = typeof(double);
            dt.Columns[14].DataType = typeof(double);
            dt.Columns[15].DataType = typeof(double);
            dt.Columns[16].DataType = typeof(double);
            dt.Columns[17].DataType = typeof(double);
            dt.Columns[18].DataType = typeof(double);
            dt.Columns[19].DataType = typeof(string);
            List<StaffPlan> Data = (List<StaffPlan>)Session["data"];
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            foreach (var item in Data)
            {
                dt.Rows.Add(item.StaffCode, item.StaffName, item.OrganizationUnitCode, item.OrganizationUnitName, item.ContractType, item.Year, item.Status, item.M1, item.M2, item.M3, item.M4, item.M5, item.M6,
                    item.M7, item.M8, item.M9, item.M10, item.M11, item.M12, item.Result);
            }
            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            Session["data"] = null;
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StaffPlanImplementationDataError.xlsx");
        }

    }

}