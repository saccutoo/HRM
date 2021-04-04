using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class ReportSourceLead
    {
        public int id { get; set; }
        public string StatusList { get; set; }
        public int Quantity_MKT { get; set; }
        public int Quantity_Web { get; set; }
        public int Quantity_Sale { get; set; }
        public int Total { get; set; }
        public double Rate_MKT { get; set; }
        public double Rate_Web { get; set; }
        public double Rate_Sale { get; set; }
    }
}
