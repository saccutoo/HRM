using Hrm.Framework.Models;
using Newtonsoft.Json;
using System.Web.Mvc;
using Hrm.Service;
using Hrm.Framework.Helper;
using System.Collections.Generic;
using Hrm.Core.Infrastructure;
using Hrm.Framework.ViewModels;

namespace Hrm.Framework.Controllers
{
    public class MenuController : BaseController
    {
        public ActionResult LeftMenu()
        {
            var _menuService = EngineContext.Current.Resolve<IMenuService>();
            var menu_vm = new MenuViewModel();
            menu_vm.Menus = new List<MenuModel>();
            var response = _menuService.GetMenu();
            var result = JsonConvert.DeserializeObject<HrmResultModel<MenuModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                menu_vm.Menus = result.Results;
            }
            return PartialView(UrlHelpers.View("~/Views/Shared/_Menu.cshtml"), menu_vm);
        }
    }
}