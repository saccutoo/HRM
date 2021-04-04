using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{

   public class TableConfigModel:BaseModel
    {
        public long TableId { get; set; }
        public string ConfigData { get; set; }
        public string FilterData { get; set; }
    }
}
