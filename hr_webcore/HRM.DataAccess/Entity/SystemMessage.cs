using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
   public class SystemMessage
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public bool checkExisted { set; get; }
        public int existedResult { set; get; }
        public string ListStaffCodeExisted { set; get; }
        public string ExistedDate { set; get; }

    }
}
