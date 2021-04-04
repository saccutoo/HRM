using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class StaffContractModel : BaseModel
    {
        public long StaffId { get; set; }
        public long ContractTypeId { get; set; }
        public long ContractTime { get; set; }
        public string ContractCode { get; set; }
        public string Name { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string ContractNote { get; set; }
        public long ContractStatus { get; set; }
    }
}
