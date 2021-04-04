using Hrm.Framework.Models;
using System.Collections.Generic;

namespace Hrm.Framework.ViewModels
{
    public class MenuViewModel
    {
        public List<MenuModel> Menus { get; set; }
        public List<MenuModel> MenuGroups { get; set; }
        public List<MenuModel> SubMenuGroups { get; set; }
    }
}
