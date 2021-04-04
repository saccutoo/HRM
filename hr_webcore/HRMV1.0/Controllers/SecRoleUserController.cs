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
    public class SecRoleUserController : Controller
    {
        // GET: SecRoleUser

        [Permission(TableID = (int)ETable.Sec_Role_User, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/SecUser/TableServerSideGetData";
            ViewBag.url2 = "/SecRole/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Sec_Role_User, TypeAction = (int)EAction.Add)]
        [WriteLog(Action = EAction.Add, LogStoreProcedure = "sec_Role_User_Insert")]
        public ActionResult _AddSecRoleUser(int id1, int id2)
        {
            var db = new Sec_Role_UserDal();
            Sec_Role_User secrolemenu = new Sec_Role_User();
            secrolemenu.RoleID = id1;
            secrolemenu.UserID = id2;
            var result = db.AddSecRoleUser(1, 1, secrolemenu);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Sec_Role_User, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "sec_Role_User_GetByIDUser")]
        public ActionResult GetRoleByIDUser(int id, int idTable)
        {
            var db = new Sec_Role_UserDal();
            var result = db.GetIDMenuByIdUser(1, 27, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
      
    }
}