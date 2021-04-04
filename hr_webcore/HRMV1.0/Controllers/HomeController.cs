using ERP.Framework.Singleton;
using HRM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Security;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DemoTable()
        {
            return PartialView();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public ActionResult EmployeeList()
        //{
        //    ViewBag.Message = "Danh sách nhân viên.";
        //    var list = SingletonIpl.GetInstance<EmployeeDAL>().GetAll();
        //    return View(list);
        //}

        //public JsonResult GetAllEmployeeList()
        //{
        //    try
        //    {
        //        return Json(SingletonIpl.GetInstance<EmployeeDAL>().GetAll(), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exp)
        //    {
        //        return Json("Error in getting record !", JsonRequestBehavior.AllowGet);
        //    }

        //}
    }
}