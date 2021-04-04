using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class ControlModel : BaseModel
    {
        public string Class { get; set; }
        public bool ShowFooter { get; set; } = true;
    }
}
