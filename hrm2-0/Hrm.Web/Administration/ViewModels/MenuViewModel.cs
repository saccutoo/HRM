using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Admin.ViewModels
{
    public class MenuViewModel
    {
        public List<MenuModel> listMenu { get; set; } = new List<MenuModel>();
        public MenuModel menu { get; set; } = new MenuModel();
    }
}