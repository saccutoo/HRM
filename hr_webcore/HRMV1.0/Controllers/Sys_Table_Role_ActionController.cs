using ClosedXML.Excel;
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
using HRM.Security;
using static HRM.Constants.Constant;
using HRM.Logger;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class Sys_Table_Role_ActionController : Controller
    {
        // GET: Sys_Table_Role_Action
        [Permission(TableID = (int)ETable.Sys_Table_Role_Action, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/SecRole/TableServerSideGetData";
            ViewBag.url1 = "/Sys_Table_Role_Action/TableServerSideGetData";
            return PartialView();
        }


        [Permission(TableID = (int)ETable.Sys_Table_Role_Action, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Sys_Table_List")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Sys_Table_Role_ActionDal();
            int total = 0;
            var result = db.GetSys_Table(pageIndex, pageSize, filter, out total);
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

        [Permission(TableID = (int)ETable.Sys_Table_Role_Action, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "Add_Sys_Table_Role_Action")]

        public ActionResult _AddSys_Table_Role_Action(int id1, int id2, Sys_Table_Role_Action sys)
        {
            var db = new Sys_Table_Role_ActionDal();
            Sys_Table_Role_Action sys1 = new Sys_Table_Role_Action();
            sys1.RoleId = id1;
            sys1.TableId = id2;
            sys1.isAdd = sys.isAdd;
            sys1.isEdit = sys.isEdit;
            sys1.isDelete = sys.isDelete;
            sys1.isActive = sys.isActive;
            sys1.isFilterButton = sys.isFilterButton;
            sys1.isExcel = sys.isExcel;
            sys1.isSubmit = sys.isSubmit;
            sys1.isApproval = sys.isApproval;
            sys1.isDisApproval = sys.isDisApproval;
            sys1.isCopy = sys.isCopy;
            sys1.isIndex = sys.isIndex;
            sys1.isGet = sys.isGet;
            var result = db.Add_Sys_Table_Role_Action(1, 28, sys1);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


      
        public ActionResult Sys_Table_Role_ActionByIDRole(int id)
        {
            var db = new Sys_Table_Role_ActionDal();
            var result = db.Sys_Table_Role_Action_GetByIDRole(id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}