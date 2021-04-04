using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity.UserDefinedType
{
    public class HR_WorkingDaySupplementType: IUserDefinedType
    {
        public int AutoID { get; set; }
        public int Type { get; set; }
        public string Note { get; set; }
    }
}
