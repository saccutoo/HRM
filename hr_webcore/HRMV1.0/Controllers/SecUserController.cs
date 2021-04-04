using ClosedXML.Excel;
using HRM.DataAccess.DAL;
using HRM.Models;
using Newtonsoft.Json;
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
    public class SecUserController : Controller
    {
        // GET: SecUser

        [Permission(TableID = (int)ETable.Sec_User, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/SecUser/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Sec_User, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Add, LogStoreProcedure = "sec_User_List")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Sec_UserDal();
            int total = 0;
            var result = db.GetSecUser(pageIndex, pageSize, filter, out total);
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
    }
}