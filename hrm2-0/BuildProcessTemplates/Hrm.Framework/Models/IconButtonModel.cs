using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
   public class IconButtonModel
    {
        public string Name { get; set; }
        public string IconName { get; set; }
        public string BtnClass { get; set; }
        public string OnClick { get; set; }
        public string NgClick { get; set; }
        public string Style { get; set; }
        public int ViewType { get; set; } = 1;
        public string Title { get; set; } = string.Empty;
    }
}
