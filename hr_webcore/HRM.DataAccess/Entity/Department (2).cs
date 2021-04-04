using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Model
{
    public class DepartmentViewModel
    {
        public int id { get; set; }
        public string label { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public int ParentID { get; set; }
        public bool? collapsed { get; set; }
        public List<DepartmentViewModel> children { get; set; }
    }
}
