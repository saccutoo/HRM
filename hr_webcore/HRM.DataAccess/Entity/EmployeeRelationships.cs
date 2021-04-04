using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class EmployeeRelationships
    {
        public int AutoID { get; set; }
        public int StaffID { get; set; }
        public int RelationshipID { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
        public bool Deduction { get; set; }
        public string DeductionCode { get; set; }
        public DateTime? DeductionFrom { get; set; }
        public DateTime? DeductionTo { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
