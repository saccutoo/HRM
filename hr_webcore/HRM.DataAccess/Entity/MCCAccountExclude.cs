using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class MCCAccountExclude
    {
        public int AutoID { set; get; }
        public  long AccountNumber { set; get; }
        public DateTime? StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public  int CreatedBy { set; get;}
        public string CreatedByName { set; get; }
        public DateTime? CreatedOn { set; get; }
        public int ModifiedBy { set; get; }
        public string ModifiedByName { set; get; }
        public DateTime? ModifiedOn { set; get; }
        public string Note { set; get; }
    }
}
