using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Sec_StaffMarginLevel
    {
        public int AutoID { get; set; }
        public int? StaffLevelID { get; set; }
        public double? MinMargin { get; set; }
        public double? MaxMargin { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Fullname { get; set; }
        public string StatusName { get; set; }
        public string MinMarginMoney { get; set; }
        public string MaxMarginMoney { get; set; }
    }

}
