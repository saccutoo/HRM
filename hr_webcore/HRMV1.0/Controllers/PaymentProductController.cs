using ClosedXML.Excel;
using ERP.Framework.DataBusiness.Common;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.DataAccess.Common;
using static HRM.Constants.Constant;
using HRM.Logger;

namespace HRM.Controllers
{
    public class PaymentProductController : Controller
    {
        // GET: PaymentProduct
        [Permission(TableID = (int)ETable.PaymentProduct, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/PaymentProduct/TableServerSideGetData";
            return View();
        }

        [Permission(TableID = (int)ETable.PaymentProduct, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PaymentProduct_Gets")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter)
        {
            var db = new PaymentProductDAL();
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
            var result = db.GetPaymentProduct(baseListParam, out total,out totalColumns);
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                roleid = baseListParam.UserType.ToString()

            }));
        }

        [HttpGet]
        [Permission(TableID = (int)ETable.PaymentProduct, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Customer_Gets_ByUserID_and_key")]
        public ActionResult GetCustomerContractByUserAndKey(string key)
        {
            CustomerDAL contractDal = new CustomerDAL();
            var list = contractDal.GetCustomerContractByUserAndKey(Global.CurrentUser.UserID, key).ToList();
            var jsonResult = list.Select(results => new { Id = results.CustomerID, FullName = results.FullName, Website = results.Website ?? "", Fanpage = results.Fanpage ?? "" });
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lấy danh sách BD theo role=3 và role=29
        /// </summary>
        /// <returns></returns>
        [Permission(TableID = (int)ETable.PaymentProduct, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "StaffWhereRoleBD")]
        public ActionResult GetsBD()
        {
            var db = new PaymentProductDAL();
            var result = db.StaffWhereRoleBD();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult Save_PaymentProduct()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.PaymentProduct, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "PaymentProduct_Save")]
        public ActionResult SavePaymentProduct(PaymentProduct obj)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new PaymentProductDAL();
            var result = db.SavePaymentProduct(baseListParam,obj);
            //if (result.IsSuccess == true && obj.WPID == 0)
            //{
            //    result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            //}
            //else if (result.IsSuccess == true && obj.WPID != 0)
            //{
            //    result.Message = AppRes.MS_Update_success;
            //}
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.PaymentProduct, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PaymentProduct_GetInfo")]
        public ActionResult GetEditItemById(int id,int CustomerID)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new PaymentProductDAL();
            var result = db.GetPaymentProductById(baseListParam, id, CustomerID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.PaymentProduct, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "PaymentProductReportGets")]
        public ActionResult PaymentProductExportExcel(int pageIndex, int pageSize,ListFilterParam list,string filter = "")
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[9]
            {
                  new DataColumn(AppRes.CustomerCode),
                    new DataColumn(AppRes.Email),
                new DataColumn(AppRes.Website),
                 new DataColumn(AppRes.BD),
                new DataColumn(AppRes.OrganizationUnitName),
               new DataColumn(AppRes.Product),
               new DataColumn(AppRes.Margin),
                new DataColumn(AppRes.PaymentDate),
                new DataColumn(AppRes.StatusName)
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(DateTime);
            dt.Columns[8].DataType = typeof(string);

            var db = new PaymentProductDAL();
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
            var lstData = db.ExportExcelPaymentProduct(baseListParam, list, out total);
            foreach (var item in lstData)
            {

                dt.Rows.Add(            
                    item.CustomerID,
                    item.Email == null ? "" : item.Email,
                    item.Website == null ? "" : item.Website,
                    item.BDName == null ? "" : item.BDName,
                    item.OrganizationUnitName == null ? "": item.OrganizationUnitName,
                    item.ProductName == null ? "" : item.ProductName,
                    item.Amount,
                    item.PaymentDate,
                    item.StatusName == null ? "" : item.StatusName
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PaymentProduct.xlsx");
        }
    }
}