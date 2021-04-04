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

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class ShareRateForPerformanceReportController : Controller
    {
        // GET: ShareRateForPerformanceReport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Staff()
        {
            ViewBag.url = "/ShareRateForPerformanceReport/GetShareRateForPerformanceReportbyStaff";
            ViewBag.Title = AppRes.ShareRateForPerformanceReportbyStaff;
            return View();
        }
        public ActionResult Dept()
        {
            ViewBag.url = "/ShareRateForPerformanceReport/GetShareRateForPerformanceReportbyOrganizationUnit";
            ViewBag.Title = AppRes.ShareRateForPerformanceReportbyOrganizationUnit;
            return View();
        }
        [WriteLog(Action = Constant.EAction.Get, LogStoreProcedure = "GetShareRateForPerformanceReportbyStaff")]
        public ActionResult GetShareRateForPerformanceReportbyStaff(int pageIndex, string pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            var db = new ShareRateForPerformanceReportDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
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
            var result = db.GetShareRateForPerformanceReportbyStaff(baseListParam, list, out total);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                userid = baseListParam.UserId

            }));
        }

        [WriteLog(Action = Constant.EAction.Get, LogStoreProcedure = "GetShareRateForPerformanceReportbyOrganizationUnit")]
        public ActionResult GetShareRateForPerformanceReportbyOrganizationUnit(int pageIndex, string pageSize, ListFilterParam list, string filter = "")
        {

            filter = filter.Replace("!!", "%");
            var db = new ShareRateForPerformanceReportDAL();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
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
            var result = db.GetShareRateForPerformanceReportbyOrganizationUnit(baseListParam, list, out total);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                userid = baseListParam.UserId

            }));
        }
        public ActionResult getListPerformanceReport(string id)
        {
            var db = new ShareRateForPerformanceReportDAL();
            var languageID = Global.CurrentLanguage;
            int id1 = Convert.ToInt32(id);
            var result = db.getListPerformanceReport(languageID, id1, Global.CurrentUser.UserID, Global.CurrentUser.RoleId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [WriteLog(Action = Constant.EAction.Edit, LogStoreProcedure = "SaveShareRateForPerformanceReport")]
        public ActionResult Save(ShareRateForPerformanceReport obj)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new ShareRateForPerformanceReportDAL();
            var result = db.Save(baseListParam, obj);
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
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            var db = new ShareRateForPerformanceReportDAL();
            var result = db.ShareRateForPerformanceReportGetByID(baseListParam, id, CustomerID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}