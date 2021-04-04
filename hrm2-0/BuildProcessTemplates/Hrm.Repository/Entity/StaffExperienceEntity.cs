using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class StaffExperienceEntity : BaseEntity
    {
        public long StaffId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long OfficePositionID { get; set; }
        public long OfficeRoleID { get; set; }
        public decimal Salary { get; set; }
        public string CompanyName { get; set; }
        public string Note { get; set; }
        public string CurrencyId { get; set; }
    }
}
