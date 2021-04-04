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
using HRM.Logger;
using HRM.Security;
using static HRM.Constants.Constant;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class SecRoleMenuController : Controller
    {
        // GET: SecMenu

        [Permission(TableID = (int)ETable.Sec_Menu, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/SecRole/TableServerSideGetData";
            ViewBag.url1 = "/SecMenu/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Sec_Menu, TypeAction = (int)EAction.Add)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "sec_Role_Menu_Insert")]
        public ActionResult _AddSecRoleMenu(int id1, int id2)
        {
            var db = new Sec_RoleMenuDAL();
            Sec_Role_Menu secrolemenu = new Sec_Role_Menu();
            secrolemenu.RoleID = id1;
            secrolemenu.MenuID = id2;
            var result = db.AddSecRoleMenu(1, 1, secrolemenu);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Sec_Menu, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "sec_Role_Menu_GetByIDRole")]
        public ActionResult GetMenuByIDRole(int id, int idTable)
        {
            var db = new Sec_RoleMenuDAL();
            var result = db.GetIDMenuByIdRole(1, 11, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}