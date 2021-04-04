using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class MenuEntity : BaseEntity
    {
        public string MenuName { get; set; }
        public string RouteUrl { get; set; }
        public string Icon { get; set; }
        public int MenuGroupType { get; set; }
        public long ParentId { get; set; }

    }
}
