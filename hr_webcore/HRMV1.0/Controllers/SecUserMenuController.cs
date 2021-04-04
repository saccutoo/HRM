using ClosedXML.Excel;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
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
    public class SecUserMenuController : Controller
    {
        // GET: SecUserMenu

        [Permission(TableID = (int)ETable.Sec_Role_Menu, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/SecUser/TableServerSideGetData";
            ViewBag.url1 = "/SecMenu/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Sec_Role_Menu, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "sec_User_Menu_GetByUserID")]
        public ActionResult GetMenuByUserID(int id, int idTable)
        {
            var db = new Sec_UserMenuDAL();
            var result = db.GetIDMenuByUserID(1, 12, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Sec_Role_Menu, TypeAction = (int)EAction.Add)]
        [WriteLog(Action = EAction.Add, LogStoreProcedure = "sec_User_Menu_Insert")]

        public ActionResult _AddSecUserMenu(int id1, int id2)
        {
            var db = new Sec_UserMenuDAL();
            Sec_User_Menu secusermenu = new Sec_User_Menu();
            secusermenu.UserID = id1;
            secusermenu.MenuID = id2;
            var result = db.AddSecUserMenu(1, 1, secusermenu);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}