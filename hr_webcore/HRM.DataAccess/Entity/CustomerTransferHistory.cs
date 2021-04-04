using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class CustomerTransferHistory
    {
        public string TranferFrom { get; set; }
        public string TranferTo { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerCode { get; set; }
        public string Month { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int CustomerID { get; set; }
        public string Amount { get; set; }
        public string AmountAssign { get; set; }
        public string Status { get; set; }
        public int OrganizationUnitID { get; set; }
        public int ToStaffID { get; set; }
        public int StaffId { get; set; }
        public int ToOrganizationUnitID { get; set; }
        public string AmountAssignBefore_From { get; set; }
        public string AmountAssignAfter_From { get; set; }
        public string AmountAssignBefore_To { get; set; }
        public string AmountAssignAfter_To { get; set; }
        public string Email { get; set; }



    }
}
