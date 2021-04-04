using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class SalaryElementType : IUserDefinedType
    {
        public long Id { get; set; }
        public long SalaryTypeId { get; set; }
        public long SalaryElementId { get; set; }
        public bool IsShowSalary { get; set; }
        public bool IsShowPayslip { get; set; }
        public bool IsEdit { get; set; }
        public int OrderNo { get; set; }

    }
}
