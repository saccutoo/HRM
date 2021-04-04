
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
    public class HR_OpeningAdditionalWorkController : Controller
    {
        // GET: HR_OpeningAdditionalWork
        [Permission(TableID = (int)ETable.HR_OpeningAdditionalWork, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/HR_OpeningAdditionalWork/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.HR_OpeningAdditionalWork, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "HR_OpeningAdditionalWork_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new HR_OpeningAdditionalWorkDAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.HR_OpeningAdditionalWork_GetList(pageIndex, pageSize, filter, LanguageCode, out total);
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
        public ActionResult SaveOpeningAdditionalWork()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.HR_OpeningAdditionalWork, TypeAction = (int)EAction.Add)]
        [WriteLog(Action = EAction.Add, LogStoreProcedure = "HR_OpeningAdditionalWork_Save")]
        public ActionResult OpeningAdditionalWorkSave(List<HR_OpeningAdditionalWork> data)
        {
            var db = new HR_OpeningAdditionalWorkDAL();
            foreach (var item in data)
            {
                item.CreatedBy = Global.CurrentUser.UserID;
                item.ModifiedBy = Global.CurrentUser.UserID;
                item.CreatedDate = DateTime.Today;
                item.ModifiedDate = DateTime.Today;
            }
            var result = db.OpeningAdditionalWorkSave(data);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.HR_OpeningAdditionalWork, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "HR_OpeningAdditionalWork_Save")]
        public ActionResult OpeningAdditionalWorkEdit(HR_OpeningAdditionalWork data)
        {
            var db = new HR_OpeningAdditionalWorkDAL();
            data.CreatedBy = Global.CurrentUser.UserID;
            data.ModifiedBy = Global.CurrentUser.UserID;
            data.CreatedDate = DateTime.Today;
            data.ModifiedDate = DateTime.Today;
            int Status = 0;
            var result = db.OpeningAdditionalWorkEdit(data, out Status);
            if (result.IsSuccess==false)
            {
                result.Message = AppRes.errorAlreadyExists;
            }
            else
            {
                result.Message = AppRes.SuccessfulUpdate;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                Status,
                result
            }));
        }
        [Permission(TableID = (int)ETable.HR_OpeningAdditionalWork, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "OpeningAdditionalWorkDelete")]
        public ActionResult OpeningAdditionalWorkDelete(int id)
        {
            int Status = 0;
            var db = new HR_OpeningAdditionalWorkDAL();
            var result = db.OpeningAdditionalWorkDelete(id,out Status);
            if (result.IsSuccess==false && Status==1)
            {
                result.Message = AppRes.errorAlreadyExists;
                return Content(JsonConvert.SerializeObject(new
                {
                    Status,
                    result
                }));
            }
            return Content(JsonConvert.SerializeObject(new
            {
                Status,
                result
            }));
        }
        [Permission(TableID = (int)ETable.HR_OpeningAdditionalWork, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "HR_OpeningAdditionalWork_GetList")]
        public ActionResult ExportHR_OpeningAdditionalWork(string filter)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn(AppRes.Staff),
                new DataColumn(AppRes.OpenDay),
                new DataColumn(AppRes.Promotion_Status),
                new DataColumn(AppRes.CreatedBy),
                new DataColumn(AppRes.CreatedDate),
                new DataColumn(AppRes.ModifiedBy),
                new DataColumn(AppRes.ModifiedDate),
            });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(DateTime);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(DateTime);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(DateTime);

            var db = new HR_OpeningAdditionalWorkDAL();
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            int Total = 0;
            var result = db.HR_OpeningAdditionalWork_GetList(1,10000, filter, LanguageCode, out Total);
            foreach (var item in result)
            {
                dt.Rows.Add(item.StaffName, item.OpenDay, item.StatusName, item.CreatedName, item.CreatedDate, item.ModifiedName, item.ModifiedDate);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",Global.CurrentLanguage!=5 ? "HR_OpeningAdditionalWork.xlsx" : "DanhSachMoBoSungCong.xlsx");
        }
    }
}