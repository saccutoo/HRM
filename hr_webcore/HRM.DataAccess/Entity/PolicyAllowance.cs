using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class PolicyAllowance
    {
        public int AutoID { get; set; }
        public int PolicyID { get; set; }
        public int AllowanceID { get; set; }
        public string Name { get; set; }       

    }
}
