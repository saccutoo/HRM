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

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class Config_PersonalIncomeTaxController : Controller
    {
        // GET: Config_PersonalIncomeTax
        [Permission(TableID = (int)ETable.Config_PersonalIncomeTax, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/Config_PersonalIncomeTax/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Config_PersonalIncomeTax, TypeAction = (int)EAction.Get)]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Config_PersonalIncomeTax_DAL();
            int total = 0;
            var result = db.Config_PersonalIncomeTax_GetList(pageIndex, pageSize, filter, out total);
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
        public ActionResult SaveConfig_PersonalIncomeTax()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Config_PersonalIncomeTax, TypeAction = (int)EAction.Submit)]
        public ActionResult Config_PersonalIncomeTax_Save(Config_PersonalIncomeTax data)
        {
            var db = new Config_PersonalIncomeTax_DAL();
            var result = db.Config_PersonalIncomeTax_Save(data);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.Config_PersonalIncomeTax, TypeAction = (int)EAction.Delete)]
        public ActionResult Config_PersonalIncomeTax_Delete(int ID)
        {
            var db = new Config_PersonalIncomeTax_DAL();
            var result = db.Config_PersonalIncomeTax_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Config_PersonalIncomeTax, TypeAction = (int)EAction.Excel)]
        public ActionResult Config_PersonalIncomeTaxExportExcel(string filter)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5]
            {
                new DataColumn(AppRes.PersonalinCome),
                new DataColumn(AppRes.Tax),
                new DataColumn(AppRes.SubtractAmount),
                new DataColumn(AppRes.FullAmount),
                new DataColumn(AppRes.ProgressiveLevel),

            });
            dt.Columns[0].DataType = typeof(double);
            dt.Columns[1].DataType = typeof(double);
            dt.Columns[2].DataType = typeof(double);
            dt.Columns[3].DataType = typeof(double);
            dt.Columns[4].DataType = typeof(double);

            var db = new Config_PersonalIncomeTax_DAL();
            int total = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var lstData = db.Config_PersonalIncomeTax_GetList(1, 5000, filter, out total);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.FromIncome, item.Tax, item.ProgressiveAmount, item.FullAmount, item.SubtractAmount);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Config_PersonalIncomeTax.xlsx");
        }

    }

}