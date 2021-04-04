using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class StandardSpending
    {
        public int Id { get; set; }
        public int StaffLevelId { get; set; }
        public double? StandardSpendingAmount { get; set; }
        public int PolicyID { get; set; }
        public double? MinSpending { get; set; }
        public int? MinPerson { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }        
        public DateTime? ModifiedDate { get; set; }
        public string Name { get; set; }
        public string StaffLevelName { get; set; }
        public string StatusName { get; set; }


    }
}
