using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class SalarytypeMapperModel : BaseModel
    {
       public long SalarytypeId { get; set; }
       public long TypeId { get; set; }
       public long DataId { get; set; }

    }
}
