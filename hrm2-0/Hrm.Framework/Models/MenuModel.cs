using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class MenuModel
    {
        public long Id { get; set; }
        public string MenuName { get; set; }
        public string RouteUrl { get; set; }
        public string Icon { get; set; }
        public int MenuGroupType { get; set; }
        public long ParentId { get; set; }
    }
}
