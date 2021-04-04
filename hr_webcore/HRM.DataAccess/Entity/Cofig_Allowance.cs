using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Cofig_Allowance
    {
        public int AllowanceID { get; set; }
        public string Name { get; set; }
        public string NameEN { set; get; }
        public string Content { get; set; }
        public double? FromAmount { get; set; }
        public double? ToAmount { get; set; }
        public string sFormular { get; set; }
        public string Note { get; set; }
    }
}
