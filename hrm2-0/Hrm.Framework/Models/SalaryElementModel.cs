using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class SalaryElementModel:BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public long TypeId { get; set; }
        public long DataTypeId { get; set; }
        public string Formula { get; set; }
        public long DataFormat { get; set; }
        public string Css { get; set; }
        public int OrderNo { get; set; }
        public bool IsEdit { get; set; }
        public long LanguageId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public bool IsShowSalary { get; set; }
        public bool IsShowPayslip { get; set; }
        public int Index { get; set; }
        public bool IsEditRow { get; set; } = false;
        public bool MergeCells { get; set; }

    }
}
