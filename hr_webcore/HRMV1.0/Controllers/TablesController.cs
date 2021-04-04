using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Security;

namespace AutoAds.MVC.Controllers
{
    [HRMAuthorize]
    public class TablesController : Controller
    {
        public ActionResult TableJQGrid()
        {
            return View();
        }
        public ActionResult TableDatatable()
        {
            return View();
        }
        public ActionResult TableExtended()
        {
            return View();
        }
        public ActionResult TableStandard()
        {
            return View();
        }

    }
}