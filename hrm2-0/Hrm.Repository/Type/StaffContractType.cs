using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class StaffContractType : IUserDefinedType
    {
        public long Id { get; set; }
        public long StaffId { get; set; }
        public long ContractTypeId { get; set; }
        public long ContractTime { get; set; }
        public string ContractCode { get; set; }
        public string Name { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string ContractNote { get; set; }
        public long ContractStatus { get; set; }
        public long CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
