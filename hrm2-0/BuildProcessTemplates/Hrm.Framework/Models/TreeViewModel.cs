using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class TreeViewModel
    {     
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayField { get; set; }
        public string ValueField { get; set; }
        public string OnClick { get; set; }
        public string NgClick { get; set; }

        public long ParentId { get; set; }
        public long CurrentId { get; set; }
        public List<OrganizationModel> TreeData { get; set; }

        public int MinParent { get; set; } = 0;
        public bool IsFilter { get; set; } = false;
    }
}