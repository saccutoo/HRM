using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{

    public class CustomerContact
    {
        public int AutoID { get; set; }
        public int CustomerID { get; set; }
        public int ContactTypeID { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string FbID { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Note { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string Address { get; set; }
        public string BankNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
