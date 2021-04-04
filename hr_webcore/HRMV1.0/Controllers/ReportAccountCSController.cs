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
using HRM.DataAccess.Entity.UserDefinedType;
using System.Globalization;

namespace HRM.Controllers
{
    public class ReportAccountCSController : Controller
    {
        // GET: ReportAccountCS
        public ActionResult Index()
        {
            ViewBag.url = "/ReportAccountCS/TableServerSideGetData";
            return View();
        }
        [Permission(TableID = (int)ETable.PolicyDetail, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "ReportAccountCS_GetAccountNumber")]
        public ActionResult TableServerSideGetData(DateTime? filter1, int pageIndex, int pageSize, string filter = "")
        {
            var db = new ReportAccountCS_DAL();
            int total = 0;
            int total1 = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            var result = db.ReportAccountCS_GetAccountNumber(pageIndex, pageSize, filter, filter1, out total, out total1);
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                TotalAccountActive = total1,
                lstTotal = lstTotal
            }));
        }

        public ActionResult ReportAccountCSExportExcel(string date)
        {
            DataTable dt = new DataTable("Grid");          
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(AppRes.ReceipientAccountNumber),
            });
            dt.Columns[0].DataType = typeof(string);

            var db = new ReportAccountCS_DAL();

            int total = 0;
            int total1 = 0;
            int LanguageCode = Global.CurrentUser.CurrentLanguageID;
            DateTime dateFormat = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var lstData = db.ReportAccountCS_GetAccountNumber(1, 100000, string.Empty, dateFormat, out total, out total1);
            if (lstData!=null && lstData.Count()>0)
            {
                foreach (var item in lstData)
                {
                    dt.Rows.Add(item.AccountNumber);
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", " AccountNumberCS.xlsx");
        }

    }
}