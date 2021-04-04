using ClosedXML.Excel;
using HRM.App_LocalResources;
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

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class Sec_Role_OrganizationunitController : Controller
    {
        // GET: Sec_Role_Organizationunit
        public ActionResult Index()
        {
            ViewBag.urlSecRole = "/SecRole/TableServerSideGetData";
            ViewBag.urlOrganizationUnit = "/OrganizationUnit/TableServerSideGetData";
            return PartialView();
        }

        public ActionResult GetOrganizationUnitByIdRole(int id, int idTable)
        {
            var db = new Sec_Role_OrganizationunitDAL();
            var result = db.GetOrganizationUnitByIdRole(1, 11, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        public ActionResult UpdateSec_Role_Organizationunit(int id1, int id2)
        {
            var db = new Sec_Role_OrganizationunitDAL();
            Sec_Role_Organizationunit secrolemenu = new Sec_Role_Organizationunit();
            secrolemenu.RoleID = id1;
            secrolemenu.OrganizationunitID = id2;
            var result = db.UpdateSec_Role_Organizationunit(1, 1, secrolemenu);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}