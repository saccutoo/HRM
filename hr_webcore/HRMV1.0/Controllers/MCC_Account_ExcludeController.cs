using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using ERP.Framework.DataBusiness.Common;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.Constants;
using HRM.DataAccess.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Logger;
using Newtonsoft.Json;

namespace HRM.Controllers
{
    public class MCC_Account_ExcludeController : Controller
    {
        // GET: MCC_Account_Exclude
        public ActionResult Index()
        {
            ViewBag.url = "/MCC_Account_Exclude/TableServerSideGetData";
            return View();
        }
        [Permission(TableID = (int)Constant.ETable.Salary, TypeAction = (int)Constant.EAction.Get)]
        [WriteLog(Action = Constant.EAction.Get, LogStoreProcedure = "GetMCCAccountExclude")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new MCCAccountExcludeDAL();

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
            var result = db.GetMCCAccountExclude(baseListParam, out total, out totalColumns);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                staffID = baseListParam.UserId
            }));
        }

        public ActionResult Save_MCCAcountExclude()
        {
            return PartialView();
        }
        public ActionResult SaveMCCAcountExclude(MCCAccountExclude obj)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new MCCAccountExcludeDAL();
            var result = db.SaveMCCAcountExclude(baseListParam, obj);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult ExportExcelMCCAcountExclude(int pageIndex, int pageSize, ListFilterParam list, string filter = "")
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[9]
            {
                  new DataColumn(AppRes.AccountNumber),
                    new DataColumn(AppRes.StartDate),
                new DataColumn(AppRes.EndDate),
                 new DataColumn(AppRes.Status),
                new DataColumn(AppRes.CreatedBy),
               new DataColumn(AppRes.CreatedDate),
               new DataColumn(AppRes.ModifiedBy),
                new DataColumn(AppRes.ModifiedDate),
                new DataColumn(AppRes.Note)
            });
            dt.Columns[0].DataType = typeof(long);
            dt.Columns[1].DataType = typeof(DateTime);
            dt.Columns[2].DataType = typeof(DateTime);
            dt.Columns[3].DataType = typeof(int);
            dt.Columns[4].DataType = typeof(int);
            dt.Columns[5].DataType = typeof(DateTime);
            dt.Columns[6].DataType = typeof(int);
            dt.Columns[7].DataType = typeof(DateTime);

            var db = new MCCAccountExcludeDAL();
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
            var lstData = db.ExportExcelMCCAccountExclude(baseListParam, out total);
            foreach (var item in lstData)
            {

                dt.Rows.Add(
                    item.AccountNumber,
                    item.StartDate ,
                    item.EndDate ,
                    item.Status,
                    item.CreatedBy,
                    item.CreatedOn,
                    item.ModifiedBy,
                    item.ModifiedOn,
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MCCAccountExclude.xlsx");
        }


        public ActionResult GetEditItemById(int id)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new MCCAccountExcludeDAL();
            var result = db.GetMCCAccountExcludeByID(baseListParam, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}