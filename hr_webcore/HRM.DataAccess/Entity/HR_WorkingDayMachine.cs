using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class HR_WorkingDayMachine
    {
        public int WorkingDayMachineID { get; set; }
        public string Name { get; set; }
        public string NameEN { set; get; }
        public string DatabaseName { get; set; }
        public bool? DayOfWeekFomat { get; set; }
        public int StatusSat { get; set; }
    }

}
