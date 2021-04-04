using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class EmployeeBonus_Discipline
    {
        public string GroupName { set; get; }
        public string ActionName { set; get; }
        public string CurrencyName { set; get; }

        public string StaffName { set; get; }
        public string OrganizationUnit { set; get; }
    }
}
