using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
   public class SectionWithIconRight
    {
        public string SectionName { get; set; }
        public string WidthSecitonName { get; set; }
        public string WidthSectionIconRight { get; set; }
        public List<IconRight> ListIcon { get; set; }
    }
    public class IconRight
    {
        public string Icon { get; set; }
        public string Action { get; set; }

    }

}
