using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Config_Insurance_Position
    {
        public int AutoID { get; set; }
        public int PositionID { get; set; }
        public double Amount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ApplyDate { get; set; }
        public string Note { get; set; }
        public string PositionName { get; set; }
        public string StatusName { get; set; }
        public string CreatedName { get; set; }
    }
}
