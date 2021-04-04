using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class HealthInsuranceModel : BaseModel
    {
        public long StaffId { get; set; }
        public string InsuranceCode { get; set; }
        public string InsuranceNumber { get; set; }
        public long TypeId { get; set; }
        public long Status { get; set; }
        public string PlaceHealthCare { get; set; }
        public string Note { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
