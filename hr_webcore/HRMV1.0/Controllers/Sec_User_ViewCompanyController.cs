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

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class Sec_User_ViewCompanyController : Controller
    {
        // GET: Sec_User_ViewCompany
        [Permission(TableID = (int)ETable.Sec_User_ViewCompany, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.urlUser = "/SecUser/TableServerSideGetData";
            ViewBag.urlCompany = "/OrganizationUnit/GetCompany";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.Sec_User_ViewCompany, TypeAction = (int)EAction.Get)]
        public ActionResult GetUserByIdCompany(int id, int idTable)
        {
            var db = new Sec_User_ViewCompanyDAL();
            var result = db.GetUserByIdCompany(1, 46, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        [Permission(TableID = (int)ETable.Sec_User_ViewCompany, TypeAction = (int)EAction.Add)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "SaveSec_User_ViewCompany")]

        public ActionResult SaveSec_User_ViewCompany(int id1, int id2)
        {
            var db = new Sec_User_ViewCompanyDAL();
            Sec_User_ViewCompany Sec_User_ViewCompany = new Sec_User_ViewCompany();
            Sec_User_ViewCompany.CompanyID = id1;
            Sec_User_ViewCompany.UserID = id2;
            var result = db.SaveSec_User_ViewCompany(1, 1, Sec_User_ViewCompany);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}