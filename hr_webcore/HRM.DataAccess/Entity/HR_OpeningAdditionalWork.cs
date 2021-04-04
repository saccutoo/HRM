using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity.UserDefinedType
{
    public class HR_OpeningAdditionalWork: IUserDefinedType
    {
        public int AutoID { get; set; }
        public int StaffID { get; set; }
        public DateTime OpenDay { get; set; }
        public int CreatedBy  { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Status { get; set; }
        public string StaffName { get; set; }
        public string StatusName { get; set; }
        public string CreatedName { get; set; }
        public string ModifiedName { get; set; }

    }
}
