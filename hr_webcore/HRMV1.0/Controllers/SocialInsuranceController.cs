using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Security;
using static HRM.Constants.Constant;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class SocialInsuranceController : Controller
    {
        // GET: SocialInsurance
        [Permission(TableID = (int)ETable.SocialInsuranceDetail, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            return View();
        }
    }
}