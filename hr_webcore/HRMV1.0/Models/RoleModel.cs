using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HRM.Models
{
    public class RoleMenuModel : BaseModel
    {
        public RoleMenuModel()
        {
            ListMenuSelected = new List<int>();
            AllMenu = new List<Sec_MenuModel>();
        }

        public int RoleId { get; set; }
        public List<int> ListMenuSelected { get; set; }
        public List<Sec_MenuModel> AllMenu { get; set; }
        public string MenuIdSelected { get; set; }

        public string GetHtml()
        {
            return GetHtml(AllMenu, 0);
        }

        private string GetHtml(List<Sec_MenuModel> lstMenu, int treeLevel)
        {
            var html = new StringBuilder();
            var htmlBonus = "";
            if (treeLevel > 0)
            {
                for (int i = 0; i < treeLevel; i++)
                {
                    htmlBonus = htmlBonus + "----";
                }
            }

            treeLevel = treeLevel + 1;
            foreach (var m in lstMenu)
            {
                var selected = ListMenuSelected.Contains(m.MenuID) ? "checked = 'checked'" : "";
                html.AppendLine("<tr>");
                html.AppendFormat("<td><input type='checkbox' class='chkSelectMenu' value='{0}' {1}/></td>", m.MenuID, selected);
                html.AppendFormat("<td>{0}</td>", string.Format("{0} {1}", htmlBonus, m.MenuNameByLanguage));
                html.AppendFormat("<td>{0}</td>", m.Url);
                html.AppendLine("</tr>");
                if (m.SubMenu.Count > 0)
                {
                    html.Append(GetHtml(m.SubMenu, treeLevel));
                }
            }

            return html.ToString();
        }
        
    }

    public class RoleListModel : BaseModel
    {
        public RoleListModel()
        {
            LstData = new List<RoleModel>();
        }

        public List<RoleModel> LstData;
    }
    public class RoleModel : BaseModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameEn { get; set; }
    }
}