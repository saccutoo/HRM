using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class HR_WorkingDayMachineSatList
    {
        public int AutoID { get; set; }
        public int WorkingDayMachineID { get; set; }
        public string Name { get; set; }
        public DateTime Day { get; set; }
        public int IsFullday { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Modifiedby { get; set; }
    }

}

