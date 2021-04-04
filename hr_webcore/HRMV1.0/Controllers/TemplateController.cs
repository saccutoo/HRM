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
    [HRMAuthorize]
    public class TemplateController : Controller
    {
        // GET: Template
        [Permission(TableID = (int)ETable.TemplateSalary, TypeAction = (int)EAction.Get)]
        public ActionResult Index()
        {
            ViewBag.url = "/Template/TableServerSideGetData";
            return PartialView();
        }
        [Permission(TableID = (int)ETable.TemplateSalary, TypeAction = (int)EAction.Get)]

        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Template_GetList")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Template_DAL();
            int total = 0;
            int languageCode = Global.CurrentLanguage;
            var result = db.Template_GetList(pageIndex, pageSize, filter, languageCode, out total);
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
        public ActionResult SaveTemplate()
        {
            return PartialView();
        }
        [Permission(TableID = (int)ETable.TemplateSalary, TypeAction = (int)EAction.Get)]
        public ActionResult Sys_Table_Column_GetALL()
        {
            var db = new Template_DAL();
            var result = db.Sys_Table_Column_GetALL();
            return Content(JsonConvert.SerializeObject(new
            {
                result,
            }));
        }
        [Permission(TableID = (int)ETable.TemplateSalary, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "Teamplate_Save")]

        public ActionResult Teamplate_Save(Template2 data)
        {
            var db = new Template_DAL();
            var result = db.Teamplate_Save(data);           
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
            //return View();
    }
        [Permission(TableID = (int)ETable.TemplateSalary, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "Template_Delete")]

        public ActionResult Template_Delete(int ID)
        {
            var db = new Template_DAL();
            var result = db.Template_Delete(ID);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}