using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
   public class EmloyeeDemoView
    {
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public string StaffCode { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> BirthDay { get; set; }
        public string GenderID { get; set; }
        public int ProvinceID { get; set; }
        public int ContactCountryID { get; set; }
        public int ContactProvinceID { get; set; }
        public string OrganizationUnitName { get; set; }
        public string OfficePositionID { get; set; }
        public string Status { get; set; }

    }

}
