using ClosedXML.Excel;
using ERP.Framework.DataBusiness.Common;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Logger;
using HRM.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HRM.Constants.Constant;

namespace HRM.Controllers
{
    public class CampaignController : Controller
    {
        // GET: Campaign
        [HRMAuthorize]
        public ActionResult ListReopened()
        {
            ViewBag.url = "/Campaign/GetListReopenCampaign";
            return View();
        }

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "CampaignReopened_GetAll")]

        public ActionResult GetListReopenCampaign(int pageIndex, string pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            var db = new CampaignDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
            if (list.filter7 == "1" && filter.Split('=').Count() > 1)
            {
                filter = filter.Replace(filter.Split('=')[filter.Split('=').Count() - 1], "1");
            }
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = Int32.Parse(pageSize),
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var result = db.GetListReopenCampaign(baseListParam, list, out total);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                userid = baseListParam.UserId

            }));
        }
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "CampaignReopened_GetAll")]

        public ActionResult ExportListReopenCampaign(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
               {
                    new DataColumn(AppRes.ErpCampaignID),
                    new DataColumn(AppRes.ReopenBy),
                    new DataColumn(AppRes.OrganizationUnit),
                    new DataColumn(AppRes.ReasonReopenCampaign), 
                    new DataColumn(AppRes.MediaAccount),
                    new DataColumn(AppRes.TimeReopenCampaign)

               });
            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(DateTime);


            var db = new CampaignDAL();
            int? total = 0;
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };

            list.FromDate = Convert.ToDateTime(DateTime.ParseExact(list.StringFromDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            list.ToDate = Convert.ToDateTime(DateTime.ParseExact(list.StringToDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            var lstData = db.ExportListReopenCampaign(baseListParam, list, out total);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                        item.CampaignId == null ? "" : item.CampaignId,
                        item.ReopenBy == null ? "" : item.ReopenBy,
                        item.OrginizationUnit == null ? "" : item.OrginizationUnit,
                        item.Reason == null ? "" : item.Reason,
                        item.Requester == null ? "" : item.Requester,
                        item.ReopenDate == null ? new DateTime(1, 1, 1) : item.ReopenDate
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
            excelName = "ListCampaignReopen.xlsx";
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}