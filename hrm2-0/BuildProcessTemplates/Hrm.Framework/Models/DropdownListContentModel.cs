using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class DropdownListContentModel:BaseModel
    {
        public string Name { get; set; }
        public string  Value { get; set; }
        public bool IsSelected { get; set; }
    }
}
