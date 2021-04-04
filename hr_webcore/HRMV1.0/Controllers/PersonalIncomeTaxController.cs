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
    public class PersonalIncomeTaxController : Controller
    {
        // GET: PersonalIncomeTax
        [Permission(TableID = (int)ETable.PersonalIncomeTax, TypeAction = (int)EAction.Get)]
        public ActionResult Index()
        {
            ViewBag.url = "/PersonalIncomeTax/TableServerSideGetData";
            return PartialView();
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PersonalIncomeTax_GetList")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new PersonalIncomeTax_DAL();
            int total = 0;
            int languageCode = Global.CurrentLanguage;
            var result = db.PersonalIncomeTax_GetList(pageIndex, pageSize, filter, languageCode, out total);
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

        public ActionResult SavePersonalIncomeTax()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.PersonalIncomeTax, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "PersonalIncomeTax_Save")]
        public ActionResult PersonalIncomeTax_Save(PersonalIncomeTax data)
        {
            //return View();
            var db = new PersonalIncomeTax_DAL();
            var result = db.PersonalIncomeTax_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.PersonalIncomeTax, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "PersonalIncomeTax_Delete")]
        public ActionResult PersonalIncomeTax_Delete(int ID)
        {
            var db = new PersonalIncomeTax_DAL();
            var result = db.PersonalIncomeTax_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.PersonalIncomeTax, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "PersonalIncomeTax_GetList")]
        public ActionResult PersonalIncomeTaxExportExcel(string filter)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[11]
            {
                new DataColumn(AppRes.TaxNo),
                new DataColumn(AppRes.HR_FromDate),
                new DataColumn(AppRes.HR_ToDate),
                new DataColumn(AppRes.IncomeFrom),
                new DataColumn(AppRes.IncomeComes),
                new DataColumn(AppRes.ProgressiveLevel),
                new DataColumn(AppRes.C_CurrencyID),
                new DataColumn(AppRes.C_Vat),
                new DataColumn(AppRes.Promotion_Status),
                new DataColumn(AppRes.TitleCountry),
                new DataColumn(AppRes.CustNote),
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(DateTime);
            dt.Columns[2].DataType = typeof(DateTime);
            dt.Columns[3].DataType = typeof(double);
            dt.Columns[4].DataType = typeof(double);
            dt.Columns[5].DataType = typeof(double);
            dt.Columns[6].DataType = typeof(string);
            dt.Columns[7].DataType = typeof(double);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(string);
            dt.Columns[10].DataType = typeof(string);

            var db = new PersonalIncomeTax_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.PersonalIncomeTax_GetList(1, 5000, filter, LanguageCode, out total);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.TaxNo, item.StartDate, item.EndDate,
                    item.FromAmount, item.ToAmount, item.ProgressiveTax,item.CurrencyName,item.RateTax,item.StatusName,item.CountryName,item.Note);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PersonalIncomeTax.xlsx");
        }

    }
}