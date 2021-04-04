using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System;
using System.Collections.Generic;
using Hrm.Framework.Models;
using Hrm.Service;
using Hrm.Common;
using Newtonsoft.Json;
using Hrm.Common.Helpers;
using Hrm.Repository.Entity;
using Hrm.Core.Infrastructure;
using System.Linq;
using Hrm.Framework.Helper;

namespace Hrm.Admin.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index(  )
        {
            return View();
        }
        #region Staff Checklist
        public ActionResult Checklist()
        {
            return View();
        }
        #endregion
    }
}