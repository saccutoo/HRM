using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Framework.Models;
namespace Hrm.Framework.Models
{
    public class SalaryPaySlipViewModel:BaseModel
    {
        public List<SalaryElementModel> SalaryElements { get; set; } = new List<SalaryElementModel>();
        public bool IsViewOrder { get; set; } = false;
        public bool IsSave{ get; set; } = false;
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
