using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class PaymentProduct
    {
        public int AutoID { get; set; }
        public int CustomerID { get; set; }
        public int StaffID { get; set; }
        public int Status { get; set; }
        public int OrganizationUnitID { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public int ProductID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
