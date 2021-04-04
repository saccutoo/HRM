using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class SalaryElementEntity :BaseEntity
    {
        public string Code { get; set; }
        public long TypeId { get; set; }
        public long DataTypeId { get; set; }
        public string Formula { get; set; }
        public long DataFormat { get; set; }
        public string Css { get; set; }
        public int OrderNo { get; set; }
        public bool IsEdit { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public bool IsShowSalary { get; set; }
        public bool IsShowPayslip { get; set; }
        public bool MergeCells { get; set; }
        
    }
}
