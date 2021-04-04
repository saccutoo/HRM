
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
using HRM.DataAccess.Entity.UserDefinedType;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class SalaryKPIController : Controller
    {
        // GET: SalaryKPI
        [Permission(TableID = (int)ETable.SalaryKPI, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/SalaryKPI/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.SalaryKPI, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "SalaryKPI_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new SalaryKPIDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.SalaryKPI_GetList(pageIndex, pageSize, filter, LanguageCode, out total);
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
        [Permission(TableID = (int)ETable.SalaryKPI, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "SalaryKPI_Delete")]
        public ActionResult SalaryKPI_Delete(int ID)
        {
            var db = new SalaryKPIDAL();
            var result = db.SalaryKPI_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        public ActionResult SaveSalaryKPI()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.SalaryKPI, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "SalaryKPI_Save")]
        public ActionResult SalaryKPI_Save(List<SalaryKPI> data)
        {
            int Type = 0;
            var db = new SalaryKPIDAL();
            foreach (var item in data)
            {
                item.CreatedBy = Global.CurrentUser.UserID;
                item.ModifiedBy = Global.CurrentUser.UserID;
                if (item.AutoID==0)
                {
                    item.CreatedDate = DateTime.Now;
                    item.ModifiedDate = DateTime.Now;
                }
                if (item.Month==null)
                {
                    item.Month = 0;
                }
                if (item.Year == null)
                {
                    item.Year = 0;
                }
            }      
            var result = db.SalaryKPI_Save(data, Type);       
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }

        public ActionResult SaveListSalaryKPI()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.SalaryKPI, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "SalaryKPI_GetList")]
        public ActionResult ExportSalaryKPI(string filter)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[]
            {
               new DataColumn(AppRes.TitleMonth),
                new DataColumn(AppRes.TitleQuarter),
                new DataColumn(AppRes.Year),
                new DataColumn(AppRes.CodeUser),
                new DataColumn(AppRes.Staff),
                new DataColumn(AppRes.OrganazationId),
                new DataColumn(AppRes.Timekeeping_DepartmentName),
                new DataColumn(AppRes.KPI),
                new DataColumn(AppRes.KpiCode),
                new DataColumn(AppRes.KpiValue),
                new DataColumn(AppRes.ExchangeValue),
                new DataColumn(AppRes.Rewards),
                new DataColumn(AppRes.TypeUse),
                new DataColumn(AppRes.Note),
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(int);
            dt.Columns[2].DataType = typeof(int);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(string);
            dt.Columns[7].DataType = typeof(string);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(string);
            dt.Columns[10].DataType = typeof(Decimal);
            dt.Columns[11].DataType = typeof(string);
            dt.Columns[12].DataType = typeof(string);
            dt.Columns[13].DataType = typeof(string);

            var db = new SalaryKPIDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.SalaryKPI_GetList(1, 5000, filter, LanguageCode, out total);
            if (lstData!=null && lstData.Count()>0)
            {
                foreach (var item in lstData)
                {
                    dt.Rows.Add(item.Month, item.Quarter, item.Year, item.StaffCode, item.StaffName, item.OrganizationUnitCode, item.OrganizationUnitName, item.KPIName, item.KpiCode, item.KpiValue, item.KpiAmount, item.PolicyBonusName, item.StatusOfUseName, item.Description);
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalaryKPI.xlsx");
        }

        [Permission(TableID = (int)ETable.SalaryKPI, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "SalaryKPI_Save")]
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
                        var ListSalaryKPI = new List<SalaryKPI>();

                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 1].Value == null &&  workSheet.Cells[rowIterator, 2].Value==null && workSheet.Cells[rowIterator, 3].Value == null  &&  workSheet.Cells[rowIterator, 9].Value == null)
                                {
                                    
                                }
                                else
                                {
                                    var SalaryKPI = new SalaryKPI();
                                    SalaryKPI.AutoID = 0;
                                    SalaryKPI.Month = workSheet.Cells[rowIterator, 1].Value == null ? Convert.ToInt32(null) : Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value.ToString());
                                    SalaryKPI.Quarter = workSheet.Cells[rowIterator, 2].Value == null ? Convert.ToInt32(null) : Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value.ToString());
                                    SalaryKPI.Year = workSheet.Cells[rowIterator, 3].Value == null ? Convert.ToInt32(null) : Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value.ToString());
                                    SalaryKPI.StaffCode = workSheet.Cells[rowIterator, 4].Value == null ? null : workSheet.Cells[rowIterator, 4].Value.ToString();
                                    SalaryKPI.OrganizationUnitCode = workSheet.Cells[rowIterator, 6].Value == null ? null : workSheet.Cells[rowIterator, 6].Value.ToString();
                                    SalaryKPI.KpiCode = workSheet.Cells[rowIterator, 9].Value == null ? null : workSheet.Cells[rowIterator, 9].Value.ToString();
                                    SalaryKPI.KpiValue = workSheet.Cells[rowIterator, 10].Value == null ? null : workSheet.Cells[rowIterator, 10].Value.ToString();
                                    SalaryKPI.KpiAmount = workSheet.Cells[rowIterator, 11].Value == null ? Convert.ToDecimal(null) : Convert.ToDecimal(workSheet.Cells[rowIterator, 11].Value.ToString());
                                    SalaryKPI.PolicyBonusID = workSheet.Cells[rowIterator, 12].Value == null ? Convert.ToInt32(null) : Convert.ToInt32(workSheet.Cells[rowIterator, 12].Value.ToString());
                                    SalaryKPI.Description = workSheet.Cells[rowIterator, 13].Value == null ? null : workSheet.Cells[rowIterator, 13].Value.ToString();
                                    SalaryKPI.StatusOfUse = workSheet.Cells[rowIterator, 14].Value == null ? Convert.ToInt32(null) : Convert.ToInt32(workSheet.Cells[rowIterator, 14].Value);
                                    SalaryKPI.CreatedBy = Global.CurrentUser.UserID;
                                    SalaryKPI.CreatedDate = DateTime.Today;
                                    SalaryKPI.ModifiedBy = Global.CurrentUser.UserID;
                                    SalaryKPI.ModifiedDate = DateTime.Today;
                                    SalaryKPI.StatusInput = 0;
                                    ListSalaryKPI.Add(SalaryKPI);
                                }
                            }
                        }
                        var db = new SalaryKPIDAL();
                        SystemMessage SM = new SystemMessage();
                        bool IsSuccess = true;
                        if (ListSalaryKPI.Count()<=0)
                        {
                            SM.IsSuccess = false;
                            SM.Message =AppRes.ErrorNoHasData;
                            return Content(JsonConvert.SerializeObject(new
                            {
                                SM
                            }));
                        }
                        var result = db.ImPortSalaryKPI(ListSalaryKPI, out IsSuccess);
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
                            SM.Message = AppRes.ErrorImportExcelSalaryKPI;
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

        public ActionResult ExportSalaryKPIDataError()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[]
            {
               new DataColumn(AppRes.TitleMonth),
                new DataColumn(AppRes.TitleQuarter),
                new DataColumn(AppRes.Year),
                new DataColumn(AppRes.CodeUser),
                new DataColumn(AppRes.Staff),
                new DataColumn(AppRes.OrganazationId),
                new DataColumn(AppRes.Timekeeping_DepartmentName),
                new DataColumn(AppRes.KPI),
                new DataColumn(AppRes.KpiCode),
                new DataColumn(AppRes.KpiValue),
                new DataColumn(AppRes.ExchangeValue),
                new DataColumn(AppRes.Rewards),
                new DataColumn(AppRes.Note),
                new DataColumn(AppRes.TypeUse),
                new DataColumn(AppRes.Notification),

            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(int);
            dt.Columns[2].DataType = typeof(int);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(string);
            dt.Columns[7].DataType = typeof(string);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(string);
            dt.Columns[10].DataType = typeof(Decimal);
            dt.Columns[11].DataType = typeof(int);
            dt.Columns[12].DataType = typeof(string);
            dt.Columns[13].DataType = typeof(int);
            dt.Columns[14].DataType = typeof(string);

            List<SalaryKPI> Data = (List<SalaryKPI>)Session["data"];
            var db = new SalaryKPIDAL();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            foreach (var item in Data)
            {
                dt.Rows.Add(item.Month, item.Quarter, item.Year, item.StaffCode, item.StaffName, item.OrganizationUnitCode, item.OrganizationUnitName,"", item.KpiCode, item.KpiValue, item.KpiAmount, item.PolicyBonusID, item.Description,item.StatusOfUse, item.KPIName);
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalaryKPIDataError.xlsx");
        }

        [Permission(TableID = (int)ETable.SalaryKPI, TypeAction = (int)EAction.Excel)]
        public ActionResult DownLoadTemplate()
        {
            DataTable dt = new DataTable("Grid");

            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(AppRes.TitleMonth),
                new DataColumn(AppRes.TitleQuarter),
                new DataColumn(AppRes.Year),
                new DataColumn(AppRes.CodeUser),
                new DataColumn(AppRes.Staff),
                new DataColumn(AppRes.OrganazationId),
                new DataColumn(AppRes.Timekeeping_DepartmentName),
                new DataColumn(AppRes.KPI),
                new DataColumn(AppRes.KpiCode),
                new DataColumn(AppRes.KpiValue),
                new DataColumn(AppRes.ExchangeValue),
                new DataColumn(AppRes.CodeReward),
                new DataColumn(AppRes.Note),
                new DataColumn(AppRes.TypeUse),
                new DataColumn(""),
                new DataColumn(""),
                new DataColumn(AppRes.ColumnName),
                new DataColumn(AppRes.RequestContent),
                new DataColumn(""),
                new DataColumn(AppRes.KpiCode + " "),
                new DataColumn(AppRes.KPI + " "),
                new DataColumn(""),
                new DataColumn(AppRes.CodeReward + " "),
                new DataColumn(AppRes.TypeReward + " "),
                new DataColumn(""),
                new DataColumn(AppRes.CodeTypeUse),
                new DataColumn(AppRes.TypeUse + " "),

            });
            
            var commonDAL = new CommonDal();
            var listKPI = commonDAL.GetsWhereParentIDnotTree(3494,Global.CurrentLanguage);
            var listReward = commonDAL.GetsWhereParentIDnotTree(3541, Global.CurrentLanguage);
            var listTypeUse = commonDAL.GetsWhereParentIDnotTree(3606, Global.CurrentLanguage);

            List<string> ListColumn = new List<string>()
            { AppRes.TitleMonth, AppRes.TitleQuarter, AppRes.Year,
              AppRes.CodeUser, AppRes.Staff , AppRes.OrganazationId
              ,AppRes.Timekeeping_DepartmentName,AppRes.KPI,AppRes.KpiCode,
              AppRes.KpiValue,AppRes.ExchangeValue,AppRes.TypeReward,AppRes.Note,AppRes.TypeUse
            };

            List<string> ListRequest = new List<string>()
            {
                AppRes.RequestMonth,AppRes.RequestQuater,AppRes.RequestYear,
                AppRes.RequestStaffCode,AppRes.RequestStaffName, AppRes.RequestStaffCode,AppRes.RequestStaffName,
                AppRes.RequestStaffName,AppRes.RequestKPICode,AppRes.RequestValue,AppRes.RequestExchangeValue,
                AppRes.RequsetReward,AppRes.RequestNote,AppRes.NoteTypeUse
            };
            var rowsNumber = 0;
            if (listKPI.Count > listReward.Count)
            {
                rowsNumber = listKPI.Count;
            }
            else
            {
                rowsNumber = listReward.Count;
            }

            for (int i = 0; i < rowsNumber; i++)
            {
                dt.Rows.Add(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                    string.Empty, string.Empty, ListColumn.Count()>i? ListColumn[i] : "", ListRequest.Count()>i? ListRequest[i]:"", string.Empty,
                    listKPI.Count > i ? listKPI[i].ValueEN : "", listKPI.Count > i ? (Global.CurrentLanguage==5 ? listKPI[i].Name : listKPI[i].NameEN) : ""
                    , string.Empty
                    , listReward.Count > i ? listReward[i].GlobalListID.ToString() : "", listReward.Count > i ? (Global.CurrentLanguage == 5 ? listReward[i].Name : listReward[i].NameEN) : ""
                    ,"", listTypeUse.Count>i ? listTypeUse[i].Value.ToString():"", listTypeUse.Count > i ? (Global.CurrentLanguage == 5 ? listTypeUse[i].Name : listTypeUse[i].NameEN) : "");
            }
            var wb = new XLWorkbook();    
            wb.Worksheets.Add(dt);

            wb.Worksheet("Grid").Column(15).Style.Fill.BackgroundColor = XLColor.White;
            wb.Worksheet("Grid").Column(16).Style.Fill.BackgroundColor = XLColor.White;
            wb.Worksheet("Grid").Column(19).Style.Fill.BackgroundColor = XLColor.White;
            wb.Worksheet("Grid").Column(22).Style.Fill.BackgroundColor = XLColor.White;
            wb.Worksheet("Grid").Column(25).Style.Fill.BackgroundColor = XLColor.White;
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            Session["data"] = null;
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ImportKpiTemplate.xlsx");
        }
    }

}