using Hrm.Framework.Controllers;
using Hrm.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Hrm.Admin.Controllers
{
    public partial class HomeController : BaseController
    {
        #region Fields
        private IAuthenticationService _authenticationService;
        private ILocalizationService _localizationService;

        #endregion Fields
        #region Constructors
        public HomeController(IAuthenticationService authenticationService, ILocalizationService localizationService)
        {
            this._authenticationService = authenticationService;
            this._localizationService = localizationService;
        }
        #endregion
        public ActionResult Index()
        {
            return View();
        }

    }
}
