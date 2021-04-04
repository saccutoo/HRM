using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Common;
using HRM.DataAccess.DAL;
using Newtonsoft.Json;
using HRM.Security;
using HRM.DataAccess.Entity.UserDefinedType;
using HRM.App_LocalResources;

namespace HRM.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        [HRMAuthorize]
        public ActionResult PolicyAllowanceServerSide()
        {
            return PartialView();

        }

        public ActionResult CheckSalaryErp()
        {
            ViewBag.LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString();
            return PartialView();
        }
        public ActionResult Sec_User_ViewCompanyServerSide()
        {
            return PartialView();

        }
        public ActionResult SecOrganizationunitMenuServerSide()
        {
            return PartialView();

        }
     
        public ActionResult PerformanceReportServerSide()
        {
            return PartialView();
        }


        public ActionResult SecRoleServerSide()
        {
            return PartialView();

        }
        public ActionResult SecRoleServerSide1()
        {
            return PartialView();

        }


        public ActionResult Sys_Table_Role_ActionServerSide()
        {
            return PartialView();
        }

        public ActionResult TableServerSide()
        {
            ViewBag.LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString() ?? "1";
            return PartialView();
        }



        public ActionResult SecRoleMenuServerSide1()
        {
            return PartialView();
        }
        public ActionResult SecRoleMenuServerSide()
        {
            return PartialView();
        }
        public ActionResult SecUserMenuServerSide()
        {
            return PartialView();
        }
        public ActionResult SecRoleUserServerSide()
        {
            return PartialView();
        }

        public ActionResult EmployeeAllowanceServerSide()
        {
            return PartialView();
        }

        public ActionResult SalaryServerSide()
        {
            return PartialView();
        }
        public ActionResult SalaryServerSide2()
        {
            return PartialView();
        }
        public ActionResult TimeKeepingServerSide()
        {
            return PartialView();
        }

        public ActionResult BuildTable()
        {
            ViewBag.LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString();
            return PartialView();
        }
        public ActionResult BuildFixedTable()
        {
            ViewBag.LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()??"1";
            return PartialView();
        }
        public ActionResult BuildTable1()
        {
            ViewBag.LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString() ?? "1";
            return PartialView();
        }
        public ActionResult GetTableInfo(string url)
        {
            var db = new CommonDal();
            var result = db.GetTableInfo(url);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }
        public ActionResult GetTableColumns(string url)
        {
            
            var db = new CommonDal();
            var result = db.GetTableColumns(Global.CurrentUser.RoleId, url);
            if((result != null && result.Count <= 0) || result == null)
            {
                result = db.GetTableColumnsByUser(url, Global.CurrentUser.UserID);
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }
        public ActionResult GetTableFilterColumns(int idTable)
        {
            var db = new CommonDal();
            var result = db.GetTableFilterColumns(Global.CurrentUser.RoleId, idTable);
            if ((result != null && result.Count <= 0) || result == null)
            {
                result = db.GetTableFilterColumnsByUser(Global.CurrentUser.UserID, idTable);
            }
            result.Where(T => T.isQuickFilter = true);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        public ActionResult GetTableAction(int idTable = 0)
        {
            var db = new CommonDal();
            var result = db.GetTableAction(Global.CurrentUser.RoleId, idTable);
            if (result == null)
            {
                result = db.GetTableActionByUser(Global.CurrentUser.UserID, Global.CurrentUser.RoleId, idTable);
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }
        public ActionResult GetStatus88()
        {
            var db = new CommonDal();
            var result = db.GetsWhereParentID(88,Global.CurrentUser.CurrentLanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


       

        //Lấy thông tin globallist theo parentid
        public ActionResult GetDataByGloballist(int parentid)
        {
            var db = new CommonDal();
            var LanguageID = Global.CurrentLanguage;
            var result = db.GetsWhereParentID(parentid, LanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        //Lấy thông tin globallist theo parentid không phân case cha con
        public ActionResult GetDataByGloballistnotTree(int parentid)
        {
            var db = new CommonDal();
            var LanguageID = Global.CurrentLanguage;
            var result = db.GetsWhereParentIDnotTree(parentid, LanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult GetColumnDataById(int columnId)
        {
            var db = new CommonDal();
            var LanguageID = Global.CurrentLanguage;
            var result = db.GetColumnDataById(columnId, LanguageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult GetColumnDataByRoleID()
        {
            var db = new CommonDal();
            int RoleId = Global.CurrentUser.RoleId;
            var LanguageID = Global.CurrentLanguage;
            var result = db.GetColumnDataByRoleID(RoleId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        public ActionResult GetTableDataByRoleID()
        {
            int RoleId = Global.CurrentUser.RoleId;
            var db = new CommonDal();
            var LanguageID = Global.CurrentLanguage;
            var result = db.GetTableDataByRoleID(RoleId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        public ActionResult GetListPolicy()
        {
            var db = new CommonDal();
            var result = db.GetListPolicy();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        public ActionResult GetWorkingDayTimePeriod(int staffId)
        {
            var db = new CommonDal();
            var result = db.GetWorkingDayTimePeriod(staffId);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult GetListRole()
        {
            var db = new CommonDal();
            var result = db.GetListRole();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult ColumnFormat()
        {
            return PartialView();
        }
        public ActionResult SaveColumn(List<Sys_Table_Column_Type> data)
        {
            var db = new CommonDal();
            var result = db.ColumnSave(data, Global.CurrentUser.RoleId);
            if (result.IsSuccess==true)
            {
                result.Message = AppRes.SuccessfulUpdate;
            }
            else
            {
                result.Message = AppRes.UpdateFailed;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult getAllColumns(string url)
        {
            var db = new CommonDal();
            var result = db.GetALLColumns(Global.CurrentUser.RoleId, url);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));

        }
    }
}