using Hrm.Common;
using Hrm.Framework.Controllers;
using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Hrm.Admin.Controllers
{
    public class SettingController : BaseController
    {
        #region Fields
        private IMenuService _menuService;
        #endregion Fields
        #region Constructors

        public SettingController(IMenuService menuService)
        {
            _menuService = menuService;
        }


        #endregion
        public ActionResult Index()
        {
            MenuViewModel menuViewModel = new MenuViewModel();
            var result = this._menuService.GetSettingMenu(TableConfig.Setting);
            var menuObj = JsonConvert.DeserializeObject<HrmResultModel<MenuModel>>(result);
            if (!CheckPermission(menuObj))
            {

            }
            else
            {
                menuViewModel.Menus = menuObj.Results;
            }
            return View(menuViewModel);
        }

    }
}
