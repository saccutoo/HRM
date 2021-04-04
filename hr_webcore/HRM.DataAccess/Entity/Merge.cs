using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Merge
    {
        public DateTime CHECKTIME { get; set; }
        public int USERID { get; set; }
        public int StaffID { get; set; }
        public string WorkingDayMachineName { get; set; }
        public string WorkingDayMachineID { get; set; }
        public string Fullname { get; set; }
        public string FullnameEN { get; set; }
    }
}
