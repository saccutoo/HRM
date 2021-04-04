using Hrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class StaffExperienceModel : BaseModel
    {
        public StaffExperienceModel()
        {
            CurrencyId = CurrentUser.CurrencyId;
        }
        public long StaffId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long OfficePositionID { get; set; }
        public long OfficeRoleID { get; set; }
        public decimal Salary { get; set; }
        public string CompanyName { get; set; }
        public string Note { get; set; }
        public long CurrencyId { get; set; }
    }
}
